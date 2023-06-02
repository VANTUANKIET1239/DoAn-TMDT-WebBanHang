using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json;

namespace CloudComputing.Controllers
{
    public class GioHangController : Controller
    {
        private readonly DbbhContext _db;

        public GioHangController(DbbhContext db)
        {
            _db = db;
        }
        public IActionResult XoaAllCart()
        {
            if (HttpContext.Session.TryGetValue("username", out byte[]? value))
            {
                var giohangall = _db.DetailCarts;
                _db.DetailCarts.RemoveRange(giohangall);
                _db.SaveChanges();
                HttpContext.Session.SetInt32("giohang", 0);
            }
            else
            {
                HttpContext.Session.Remove("Cart");
            }
            return RedirectToAction("Cart");

        }

        public IActionResult XoaCart(string idsanpham)
        {
            if (HttpContext.Session.TryGetValue("username", out byte[] value))
            {
                var giohang = _db.DetailCarts.FirstOrDefault(x => x.IdSp.Trim() == idsanpham.Trim());
                if (giohang.IdSp != "")
                {
                    _db.DetailCarts.Remove(giohang);
                    _db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                int cartsl = (int)(HttpContext.Session.GetInt32("giohang") ?? 0);
                HttpContext.Session.SetInt32("giohang", cartsl - 1);
            }
            else
            {
                var giohang = JsonSerializer.Deserialize<Dictionary<string, DetailCart>>(HttpContext.Session.GetString("Cart") ?? "");
                giohang.Remove(idsanpham);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(giohang));
            }
            return RedirectToAction("Cart");
        }
        public IActionResult Cart(bool? checkmua, string idsanpham, int soluongmua)
        {
            List<DetailCart> giohang = new List<DetailCart>();
            List<SP_GioHangViewModel> spgiohang = new List<SP_GioHangViewModel>();
            string? idnguoidung = HttpContext.Session.GetString("id") ?? "";
            var sp = ChucNangChung.ToanBoSP(_db).ToList();
            if (HttpContext.Session.TryGetValue("username", out byte[] value))
            {

                giohang = _db.DetailCarts.Where(x => x.IdUser.Trim() == idnguoidung.Trim()).ToList();

                spgiohang = sp.Join(giohang, spham => spham.IdSp.Trim(), gh => gh.IdSp.Trim(), (spham, gh) => new SP_GioHangViewModel()
                {
                    sanPham = spham,
                    soluong = idsanpham == null ? gh.SoLuong : (spham.IdSp.Trim() == idsanpham.Trim() ? (byte)soluongmua : gh.SoLuong),
                    idus = idnguoidung,
                    check = gh.State
                }).ToList();
                // nếu idsanpham null thì nghĩa là không có nhấn nút cộng trừ số lượng, mỗi lần nhấn tích hoặc nút cộng trừ đều sẽ có số lượng trong 
                // soluongmua
                if(idsanpham != null)
                {
                    var giohangdb = _db.DetailCarts.FirstOrDefault(x => x.IdSp == idsanpham);
                    giohangdb.SoLuong = (byte)soluongmua;
                    _db.DetailCarts.Update(giohangdb);
                    _db.SaveChanges();

                }
                if (checkmua != null)
                {
                    foreach (SP_GioHangViewModel s in spgiohang)
                    {
                        if (s.sanPham.IdSp.Trim() == idsanpham.Trim())
                        {
                            s.check = checkmua;
                            var so = _db.DetailCarts.FirstOrDefault(x => x.IdSp.Trim() == s.sanPham.IdSp.Trim());
                            so.State = checkmua;
                            _db.DetailCarts.Update(so);
                            _db.SaveChanges();
                            break;
                        }
                    }

                }
            }
            else
            {
                if (HttpContext.Session.TryGetValue("Cart", out byte[] vava))
                {
                    string? jsongiohang = HttpContext.Session.GetString("Cart") ?? "";
                    var cartsession = JsonSerializer.Deserialize<Dictionary<string, DetailCart>>(jsongiohang) ?? new Dictionary<string,DetailCart>();
                    giohang = cartsession.Select(x => new DetailCart(x.Key, x.Value.IdUser, x.Value.SoLuong, x.Value.State)).ToList();
                    spgiohang = sp.Join(giohang, spham => spham.IdSp.Trim(), gh => gh.IdSp.Trim(), (spham, gh) => new SP_GioHangViewModel()
                    {
                        sanPham = spham,
                        soluong = idsanpham == null ? gh.SoLuong : (spham.IdSp.Trim() == idsanpham.Trim() ? (byte)soluongmua : gh.SoLuong),
                        idus = idnguoidung,
                        check = gh.State
                    }).ToList();
                    if (checkmua != null)
                    {
                        foreach (SP_GioHangViewModel s in spgiohang)
                        {
                            if (s.sanPham.IdSp.Trim() == idsanpham.Trim())
                            {
                                s.check = checkmua;
                                cartsession[s.sanPham.IdSp.Trim()] = new DetailCart(s.sanPham.IdSp.Trim(), s.idus, s.soluong, s.check);
                                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartsession));
                                break;
                            }
                        }

                    }
                }
            }
            ViewBag.TongTien = spgiohang.Where(x => x.check == true).Sum(x => x.sanPham.Gia * x.soluong);

            return View(spgiohang);
        }
        [HttpGet]
        public IActionResult ThemCart(string idsp, byte soluong)
        {
            string? idnguoidung = HttpContext.Session.GetString("id");
            // nếu soluong == 0 thì nghĩa là người dùng không phải đang add cart ở trang chi tiết sản phẩm
            // checkindex == false thì trang sẽ là chi tiết sản phẩm
            // checkindex == true thì trang sẽ là trang chủ
            bool checkindex = false;
            if (soluong == 0) { soluong = 1; checkindex = true; }

            if (HttpContext.Session.TryGetValue("username", out byte[]? value))
            {
                var giohang = _db.DetailCarts.FirstOrDefault(x => x.IdSp.Trim() == idsp.Trim());
                if (giohang != null)
                {
                    giohang.SoLuong = Convert.ToByte(Convert.ToInt32(giohang.SoLuong) + Convert.ToInt32(soluong));
                    _db.DetailCarts.Update(giohang);
                    _db.SaveChanges();
                }
                else
                {
                    DetailCart card = new DetailCart(idsp, idnguoidung, soluong, false);
                    _db.DetailCarts.Add(card);
                    _db.SaveChanges();
                    int cartsl = (HttpContext.Session.GetInt32("giohang") ?? 0);
                    HttpContext.Session.SetInt32("giohang", cartsl + 1);

                }
            }
            else
            {
                if (!HttpContext.Session.TryGetValue("Cart", out byte[]? vv))
                {
                    Dictionary<string, DetailCart> cart = new Dictionary<string, DetailCart>();
                    cart.Add(idsp, new DetailCart(idsp, idnguoidung, soluong, false));
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                }
                else
                {
                    var cart = JsonSerializer.Deserialize<Dictionary<string, DetailCart>>(vv);
                    if (cart.ContainsKey(idsp))
                    {

                        cart[idsp].SoLuong = Convert.ToByte(Convert.ToInt32(cart[idsp].SoLuong) + Convert.ToInt32(soluong));

                    }
                    else
                    {

                        cart.Add(idsp, new DetailCart(idsp, idnguoidung, soluong, false));

                    }
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                }
            }

            TempData["success"] = "Đã thêm vào giỏ";
            if (checkindex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Details", "SanPham", new { id = idsp });

        }
      
    }
}
