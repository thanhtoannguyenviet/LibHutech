using LibHutech_DoAnLapTrinhWeb.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class SachController : Controller
    {
       private LibHutechEntities context = new LibHutechEntities();
        // GET: Sach
        public ActionResult Index()
        {
            List<Sach> dsSach = context.Saches.ToList();
            return View(dsSach);
        }

        public ActionResult Details(int id)
        {
            Sach s = context.Saches.FirstOrDefault(x => x.MaSach == id);
            return View(s);


        }
        
        public ActionResult Create()
        {
            if(Request.Form.Count>0){
                Sach s = new Sach();
                s.TenSach = Request.Form["TenSach"];
                s.LuotXem = 0;
                HttpPostedFileBase fileImg = Request.Files["ImgSach"];
                if (fileImg != null)
                {
                    var fileName = Path.GetFileName(fileImg.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Images"),fileName);
                    fileImg.SaveAs(path);
                    s.ImgSach = fileImg.FileName;
                }
                HttpPostedFileBase fileBook = Request.Files["DuongDan"];
                if (fileBook != null)
                {
                    var fileName = Path.GetFileName(fileBook.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/StorePDF"), fileName);
                    fileBook.SaveAs(path);
                    s.DuongDan = fileBook.FileName;
                }

                context.Saches.Add(s);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sach s = context.Saches.FirstOrDefault(x => x.MaSach == id);
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sach sach)
        {
            Sach s = context.Saches.FirstOrDefault(x => x.MaSach == sach.MaSach);
            if (Request.Form.Count == 0)
            {
                return View(s);
            } 
            s.TenSach = sach.TenSach;
            //s.DuongDan = Request.Form["DuongDan"];
            HttpPostedFileBase fileImg = Request.Files["ImgSach"];
            if (fileImg != null&& sach.ImgSach!=null)
            {
                var fileName = Path.GetFileName(fileImg.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Images"), fileName);
                fileImg.SaveAs(path);
                s.ImgSach = fileImg.FileName;
            }
            HttpPostedFileBase fileBook = Request.Files["DuongDan"];
            if (fileBook != null)
            {
                var fileName = Path.GetFileName(fileBook.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/StorePDF"), fileName);
                fileBook.SaveAs(path);
                s.DuongDan = fileBook.FileName;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Sach s = context.Saches.FirstOrDefault(x => x.MaSach == id);
            if (s != null)
            {
                context.Saches.Remove(s);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}