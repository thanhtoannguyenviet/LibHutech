using LibHutech_DoAnLapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibHutech_DoAnLapTrinhWeb.Controllers
{
    public class HomeController : Controller
    {
        private LibHutechEntities content = new LibHutechEntities();
        public ActionResult Index()
        {
            ViewBag.Sach = content.Saches.ToList();
            ViewBag.Mon = content.Mons.ToList();
            return View();
        }
        public ActionResult Filter(int id)
        {
            var ls = content.Saches.Where(s=>s.MaMon==id).ToList();
            return PartialView("Filter",ls);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Viewer(string id)
        {
            
            int MaSach = int.Parse(id);
           
            var sv = Session["Account"] as SinhVien; 
            if (sv != null) { 
           var q = from LichSus in content.LichSus
                   where
                     LichSus.Sach.MaSach == MaSach &&
                     LichSus.SinhVien.MaSV == sv.MaSV
                   select new
                   {   Trang = LichSus.SoTrang,
                       MaSach = LichSus.Sach.MaSach,
                       TenSach = LichSus.Sach.TenSach,
                       ImgSach = LichSus.Sach.ImgSach,
                       DuongDan = LichSus.Sach.DuongDan,
                       LuotXem = LichSus.Sach.LuotXem,
                       MaMon = LichSus.Sach.MaMon,
                       NoiDung = LichSus.Sach.NoiDung
                   };
                   Sach sach = new Sach();
                sach.MaSach=q.First().MaSach;
                sach.MaMon = q.First().MaMon;
                sach.TenSach= q.First().TenSach;
                sach.ImgSach= q.First().ImgSach;
                sach.DuongDan = q.First().DuongDan;
                sach.LuotXem = q.First().LuotXem;
                sach.NoiDung = q.First().NoiDung;
            ViewBag.Page = q.First().Trang;
                return View(sach);
            }
          
                var s = content.Saches.Where(sa=>sa.MaSach==MaSach).FirstOrDefault();
           
            return View(s);
        }
        [HttpPost]
        public ActionResult Login()
        {
            var username = Request.Form["Username"];
            var password = Request.Form["Password"];
            var result = content.SinhViens.Where(a => a.Email == username && a.Password == password).FirstOrDefault();
            if (result != null && ModelState.IsValid)
            {
                Session["Account"] = result;
                if (result.Role==100)
                return RedirectToAction("Index", "Admin");
                else return RedirectToAction("Index","Home");

            }
            ModelState.AddModelError("", "Tên đăng nhập hoặc mất khẩu không đúng");
            return RedirectToAction("Index", "Home");
        }

    
        [HttpPost]
        public ActionResult Create()
        {
            SinhVien s = new SinhVien();
            s.HoTen = Request.Form["HoTen"];
            s.Nganh = Request.Form["Nganh"];
            s.Email = Request.Form["Email"];
            s.Password = Request.Form["Password"];
            s.Active = true;
            s.Role = 1; //100 la admin
            HttpPostedFileBase file = Request.Files["ImgSv"];
            if (file != null)
            {
                string severPath = HttpContext.Server.MapPath("~/Uploads/Images/");
                string filePath = severPath + "/" + file.FileName;
                file.SaveAs(filePath);
                s.ImgSv = file.FileName;
            }
            content.SinhViens.Add(s);
            content.SaveChanges();
            return Redirect("Index");
        }
        public ActionResult LogOut()
        {
            if (Request.Cookies["Account"] != null)
            {
                HttpCookie c = new HttpCookie("Account");
                c.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(c);
            }
            Request.Cookies.Clear();
            return RedirectToAction("Index");
        }
    }
}
