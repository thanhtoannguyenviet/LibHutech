using LibHutech_DoAnLapTrinhWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class MonController : Controller
    {
        private LibHutechEntities content = new LibHutechEntities();

        // GET: Mon
        public ActionResult Index()
        {
            List<Mon> dsMon = content.Mons.ToList();
            return View(dsMon);
        }
        public ActionResult Details(int id)
        {
            Mon mo = content.Mons
                .FirstOrDefault(x=>x.MaMon==id);
            return View(mo);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                Mon mo = new Mon();
                mo.TenMon = Request.Form["TenMon"];
                mo.Color = Request.Form["Color"];
                HttpPostedFileBase file = Request.Files["ImgMon"];
                if (file != null)
                {
                    string serverPath = HttpContext.Server.MapPath("~/Content");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    mo.ImgMon = file.FileName;
                }
                content.Mons.Add(mo);
                content.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var m = content.Mons.FirstOrDefault(x => x.MaMon == id);
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mon mon)
        {
            Mon mo = content.Mons.FirstOrDefault(x => x.MaMon == mon.MaMon);
            mo.TenMon = mon.TenMon;
            mo.Color = mon.Color;
            mo.BgColor= mon.BgColor;
            HttpPostedFileBase fileImg = Request.Files["ImgMon"];
            if (fileImg != null && mon.ImgMon != null)
            {
                var fileName = Path.GetFileName(fileImg.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Images"), fileName);
                fileImg.SaveAs(path);
                mo.ImgMon = fileImg.FileName;
            }
            content.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Mon mo = content.Mons.FirstOrDefault(x => x.MaMon == id);
            if (mo != null)
            {
                content.Mons.Remove(mo);
                content.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}