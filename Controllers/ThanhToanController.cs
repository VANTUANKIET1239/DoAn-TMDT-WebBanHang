using CloudComputing.Models;
using CloudComputing.Others;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace CloudComputing.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly DbbhContext _db;

        public ThanhToanController(DbbhContext db)
        {
            _db = db;
        }
        public IActionResult TruocThanhToan(ThanhToanViewModel thanh)
        {
            ModelState.Remove("diaChi.IdDiachi");
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("emailthanhtoan", thanh.IDNguoiDung);
                if(thanh.HoaDon.Payment.Trim() == "MOMO")
                {
                    _db.Add(thanh.HoaDon);
                    
                    _db.DetailHoaDons.AddRange(thanh.detailHoaDons);
                    _db.SaveChanges();                   
                    return RedirectToAction("Payment", new { orderidhehe = thanh.HoaDon.Id, tongtien = thanh.HoaDon.Total });
                }
                else
                {
                    _db.Add(thanh.HoaDon);
                    _db.DetailHoaDons.AddRange(thanh.detailHoaDons);
                    _db.SaveChanges();                
                    return RedirectToAction("ConfirmPaymentClient", new { orderidhehe = thanh.HoaDon.Id});
                }
                
            }
            return View();
        }
        public IActionResult Index()
        {
            if (!HttpContext.Session.TryGetValue("username", out byte[]? value))
            {
               return RedirectToAction("DangNhap", "User", new { checkthanhtoan = "ok" });              
            }
            var giohang = _db.DetailCarts.Where(x => x.State == true).ToList();
            if(giohang.Count  == 0)
            {
                TempData["error"] = "Không có sản phẩm trong giỏ";
                RedirectToAction("Cart", "GioHang");
            }
            var hoadon = _db.HoaDons.ToList();
            var sanpham = ChucNangChung.ToanBoSP(_db);
            string idnguoidung = HttpContext.Session.GetString("id") ?? "";
            var nguoidung = _db.NguoiDungs.FirstOrDefault(x => x.Id.Trim() == idnguoidung.Trim()) ?? new NguoiDung();

            var diachitong = _db.DiaChis.ToList();
            var diachi = diachitong.Where(x => x.IdNguoiDung.Trim() == idnguoidung.Trim()).ToList();
           


            List<byte[]?> hinhanhsp = new List<byte[]>();
            List<DetailHoaDon> detailHoaDons = sanpham.Join(giohang, sp => sp.IdSp, gh => gh.IdSp, (sp, gh) => new DetailHoaDon()
            {
                IdHoaDon = "HD" + (hoadon.Count + 1).ToString("000"),
                IdSp = sp.IdSp,
                TenSp = sp.TenSanPham,
                SoLuong = (byte)gh.SoLuong,
                DonGia = (int)sp.Gia,
                State = true
                
            }).ToList();
            hinhanhsp.AddRange(sanpham.Join(giohang, sp => sp.IdSp, gh => gh.IdSp, (sp, gh) => sp).Select(x => x.HinhAnh).ToList());
            HoaDon hoaDont = new HoaDon()
            {
                Id = "HD" + (hoadon.Count + 1).ToString("000"),
                IdUser = idnguoidung,
                ShipFee = 0,
                Total = detailHoaDons.Sum(x => x.DonGia * x.SoLuong),
                State = true
            };
            
            ThanhToanViewModel thanhToanViewModel = new ThanhToanViewModel(detailHoaDons, hoaDont, new DiaChi(), nguoidung.Email);
            string iddiachi = "DC" + (diachitong.Count + 1).ToString("000");
            ViewBag.iddiachi = iddiachi;
            ViewBag.HA = hinhanhsp;
            ViewBag.DC = diachi;
        
            return View(thanhToanViewModel);
        }
        public ActionResult Payment(int tongtien, string orderidhehe )
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "test";
            string returnUrl = "https://thuongmaidientu1.azurewebsites.net/ThanhToan/ConfirmPaymentClient";
            string notifyurl = "https://thuongmaidientu1.azurewebsites.net/ThanhToan/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = tongtien.ToString();
            string orderid = orderidhehe; //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            if(rErrorCode == "0")
            {
                ViewBag.ME = rOrderId;
                var donhang = _db.HoaDons.FirstOrDefault(x => x.Id == rOrderId);
                var chitiet = _db.DetailHoaDons.Where(x => x.IdHoaDon == rOrderId).ToList();
                string ten = HttpContext.Session.GetString("username") ?? "";
                string email = HttpContext.Session.GetString("emailthanhtoan") ?? "";
                ChucNangChung.SendEmail(email,"[Xác Nhận Đơn Hàng]",ChucNangChung.vietmail(chitiet,donhang,ten));
                var giohang = _db.DetailCarts.Where(x => x.State == true).ToList();
                _db.DetailCarts.RemoveRange(giohang);
                _db.SaveChanges();
                int? slthanhtoan = HttpContext.Session.GetInt32("giohang") ?? 0;
                HttpContext.Session.SetInt32("giohang",(int)slthanhtoan - giohang.Count);
            }
           
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
           // String a = "";
        }
    }
}
