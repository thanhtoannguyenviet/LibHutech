using LibHutech_DoAnLapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibHutech_DoAnLapTrinhWeb.Controllers
{
    public class AccountController : Controller
    {

        private LibHutechEntities context = new LibHutechEntities();
        public ActionResult Index()
        {
            List<SinhVien> dsSinhVien = context.SinhViens.ToList();
            return View(dsSinhVien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SinhVien sv)
        {
          
            sv.Active = true;
            sv.Role = 100; //100 la admin
            HttpPostedFileBase file = Request.Files["ImgSv"];
            if (file != null)
            {
                string severPath = HttpContext.Server.MapPath("~/Uploads/Images");
                string filePath = severPath + "/" + file.FileName;
                file.SaveAs(filePath);
                sv.ImgSv = file.FileName;
            }
            context.SinhViens.Add(sv);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            SinhVien s = context.SinhViens.FirstOrDefault(x => x.MaSV == id);
            if (s != null)
            {
                context.SinhViens.Remove(s);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}