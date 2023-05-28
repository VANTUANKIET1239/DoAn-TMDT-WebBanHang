using CloudComputing.Models;
using CloudComputing.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputing.Controllers
{
    public class DiaChiController : Controller
    {
        private readonly DbbhContext _db;

        public DiaChiController(DbbhContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult CatNhatDiaChiMD(DiaChi diachi)
        {
            if (ModelState.IsValid)
            {
                _db.DiaChis.Update(diachi);
                _db.SaveChanges();
                TempData["success"] = "Cật Nhật Thành Công";
                return RedirectToAction("Details", "User", new { idnguoidung = diachi.IdNguoiDung });
            }
            TempData["error"] = "Cật Nhật Thất Bại";
            return RedirectToAction("Details", "User", new { idnguoidung = diachi.IdNguoiDung });
        }
        public IActionResult CatNhatDiaChiDC(DiaChi diachi)
        {
            if (ModelState.IsValid)
            {
                if (diachi.MacDinh)
                {
                    var dc = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim() == diachi.IdNguoiDung.Trim() && x.MacDinh == true);
                    dc.MacDinh = false;
                    _db.DiaChis.Update(dc);
                    _db.SaveChanges();
                    diachi.MacDinh = true;
                }
                _db.DiaChis.Update(diachi);
                _db.SaveChanges();
                TempData["success"] = "Cật Nhật Thành Công";
                return RedirectToAction("DiaChi", "User", new { idnguoidung = diachi.IdNguoiDung });
            }
            TempData["error"] = "Cật Nhật Thất Bại";
            return RedirectToAction("DiaChi", "User", new { idnguoidung = diachi.IdNguoiDung });
        }

        [HttpPost]
        public IActionResult ThemDiaChi(DiaChi diachi, string? thanhtoan)
        {

            
            if (ModelState.IsValid)
            {
                if (diachi.MacDinh) {
                    var dc = _db.DiaChis.FirstOrDefault(x => x.IdNguoiDung.Trim() == diachi.IdNguoiDung.Trim() && x.MacDinh == true);
                    dc.MacDinh = false;
                    _db.DiaChis.Update(dc);
                    _db.SaveChanges();
                    diachi.MacDinh = true;
                } 
                else diachi.MacDinh = false;
                _db.DiaChis.Add(diachi);
                _db.SaveChanges();
                TempData["success"] = "Cật Nhật Thành Công";
                if (thanhtoan == "OK")
                {
                    return RedirectToAction("Index", "ThanhToan");
                }
                return RedirectToAction("DiaChi", "User", new { idnguoidung = diachi.IdNguoiDung });
            }
            TempData["error"] = "Cật Nhật Thất Bại";
            return RedirectToAction("DiaChi", "User", new { idnguoidung = diachi.IdNguoiDung });
        }

        public IActionResult ChinhSuaDiaChi(string iddiachi)
        {
            var diachi = _db.DiaChis.FirstOrDefault(x => x.IdDiachi.Trim() == iddiachi.Trim());
            if (diachi == null)
            {
                return NotFound();
            }           
            return RedirectToAction("DiaChi", "User", new { idnguoidung =diachi.IdNguoiDung, iddiachi =diachi.IdDiachi});
        }
    }
}
