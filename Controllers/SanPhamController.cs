using CloudComputing.Conditions;
using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace CloudComputing.Controllers
{
   
    public class SanPhamController : Controller
    {

        private readonly DbbhContext _db;

        public SanPhamController(DbbhContext db)
        {
            _db = db;
        }
        [NonAction]
        public static SanPhamViewModel<T> DetailSP<T>(DbbhContext _db, SanPham sanpham, DanhMuc dm, ref List<SanPham> spthem ) where T :class, IEntitySPWithId
        {

            var spchuot = _db.Set<T>().FirstOrDefault(x => (x as IEntitySPWithId).Id.Trim().Equals(sanpham.IdSp.Trim())) ?? Activator.CreateInstance<T>();
            spthem = ChucNangChung.SPtheoDM2(_db, dm.TenBang.Trim()).Take(6).ToList();
            SanPhamViewModel<T> chuot = new SanPhamViewModel<T>()
            {
                ChiTiet = spchuot,
                SanPham = sanpham
            };
            return chuot;


        }
        public IActionResult Details(string id)
        {
         
            var danhmuc = _db.DanhMucs.ToList();
            List<SanPham> spthem = new List<SanPham>();
            var sanpham = _db.SanPhams.FirstOrDefault(x => x.TrangThai == true && x.IdSp.Trim().Equals(id.Trim())) ?? new SanPham();
            var dm = danhmuc.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(sanpham.IdDm.Trim()), new DanhMuc());
            List<HinhAnhSp> hinhanhsp = _db.HinhAnhSps.Where(x => x.IdSp.Trim().Equals(sanpham.IdSp.Trim())).ToList();
            switch (dm.TenBang.Trim())
            {
                case "CHUOT": ViewBag.SP = DetailSP<ChuotMayTinh>(_db, sanpham, dm, ref spthem); break;
                case "BANPHIM": ViewBag.SP = DetailSP<BanPhim>(_db, sanpham, dm, ref spthem); break;
                case "PCMANHINH": ViewBag.SP = DetailSP<PcManHinh>(_db, sanpham, dm, ref spthem); break;
                case "PCMAYTINHBO": ViewBag.SP = DetailSP<PcMayTinhBo>(_db, sanpham, dm, ref spthem); break;
                case "LAPTOP": ViewBag.SP = DetailSP<Laptop>(_db, sanpham, dm, ref spthem); break;
                case "TAINGHE": ViewBag.SP = DetailSP<TaiNghe>(_db, sanpham, dm, ref spthem); break;
                default: return NotFound();
            }

            ViewBag.HA = hinhanhsp;
            ViewBag.DM = dm;            
            return View(spthem);
        }

        public IActionResult MuaNgaySP(string idsanpham)
        {
            string? idnguoidung = HttpContext.Session.GetString("id") ?? "";
           if(!HttpContext.Session.TryGetValue("username",out byte[]? value))
            {
                               
                if(!HttpContext.Session.TryGetValue("Cart",out byte[]? k))
                {
                    Dictionary<string, DetailCart> cart = new Dictionary<string, DetailCart>();
                    cart.Add(idsanpham, new DetailCart(idsanpham, idnguoidung, 1, true));
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                }
                else
                {
                    var cartsession = JsonSerializer.Deserialize<Dictionary<string, DetailCart>>(HttpContext.Session.GetString("Cart") ?? "") ?? new Dictionary<string, DetailCart>();
                    if (cartsession.ContainsKey(idsanpham))
                    {
                        cartsession[idsanpham].State = true;
                    }
                    else
                    {
                        DetailCart detailCart = new DetailCart(idsanpham, idnguoidung, 1, true);
                        cartsession.Add(idsanpham, detailCart);
                        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartsession));

                    }
                }
            }
            else
            {
                var cartdb = _db.DetailCarts.Where(x => x.IdSp == idsanpham).FirstOrDefault();
                if (cartdb == null) {
                    DetailCart detailCart = new DetailCart(idsanpham, idnguoidung, 1, true);
                    _db.DetailCarts.Add(detailCart);
                    _db.SaveChanges();
                   
                }
                else
                {
                    cartdb.State = true;
                    _db.DetailCarts.Update(cartdb);
                    _db.SaveChanges();
                  
                }
                HttpContext.Session.SetInt32("giohang", ((int)HttpContext.Session.GetInt32("giohang")) + 1);
            }

            return RedirectToAction("Cart", "GioHang");
        }
        public IActionResult ToanBoSP(string id, string filter, string ThuongHieu,int pagesize = 16, int page = 1 )
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<SanPham> sp = new List<SanPham>();
            if(id == null)
            {
                return NotFound();
            }
            if(id == "TB")
            {
                var sanPhams = ChucNangChung.ToanBoSP(_db) ?? new List<SanPham>();
                sp = sanPhams;
                if(ThuongHieu == null) sp = sanPhams;
                else sp = sanPhams.Where(x => x.ThuongHieu.Trim() == ThuongHieu.Trim()).ToList();
                var thuonghieutong = sanPhams.Select(x => x.ThuongHieu).Distinct();
                ViewBag.TH = thuonghieutong;
            }
            else
            {
                var dm = _db.DanhMucs.FirstOrDefault(x => x.State == true && x.Id.Trim().Equals(id.Trim())) ?? new DanhMuc();
                if (dm == null)
                {
                    return NotFound();
                }

                var thuonghieutong = ChucNangChung.SPtheoDM2(_db, dm.TenBang.Trim()).Select(x => x.ThuongHieu).Distinct();
               
                if(ThuongHieu == null) sp = ChucNangChung.SPtheoDM2(_db, dm.TenBang.Trim()).ToList();
                else sp = ChucNangChung.SPtheoDM2(_db, dm.TenBang.Trim()).Where(x => x.ThuongHieu.Trim().Equals(ThuongHieu.Trim())).ToList();
                ViewBag.TH = thuonghieutong;
            

        }

            if (filter != null)
            {
                switch (filter)
                {
                    case "giamdan": sp = sp.OrderByDescending(x => x.Gia).ToList(); break;
                    case "tangdan": sp = sp.OrderBy(x => x.Gia).ToList(); break;
                }
            }
           
            int totalCount = sp.Count;
            int skip = (page - 1) * pagesize;
            var pagedItems = sp.Skip(skip).Take(pagesize).ToList();


            var viewModel = new PagedViewModel<SanPham>
            {
                Items = pagedItems,
                PageIndex = page,
                PageSize = pagesize,
                TotalCount = totalCount
            };
            
            ViewBag.thuonghieu = ThuongHieu;
            ViewBag.thutugia = filter;

            stopwatch.Stop();
            Console.WriteLine("thoi gian " + stopwatch.ElapsedMilliseconds);
            return View(viewModel);
        }
    }
    

}
