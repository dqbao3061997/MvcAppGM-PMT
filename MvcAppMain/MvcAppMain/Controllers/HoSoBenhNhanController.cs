using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Models;

namespace MvcAppMain.Controllers
{
    public class HoSoBenhNhanController : Controller
    {
        QLPMContext db = new QLPMContext();

        // GET: HoSoBenhNhan
        //public ActionResult DanhSachKhamBenh(DateTime? date)
        //{
        //    if (!date.HasValue) date = date.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);

        //    ViewBag.date = date;
        //    //var hosobenhnhans = db.HoSoBenhNhans.Where(c => c.NgayKham == date).ToList();
        //    //var hosobenhnhans = db.HoSoBenhNhans.Include(c => c.PhieuKhamBenh);
        //    return View(hosobenhnhans);
        //}
        // GET: HoSoBenhNhan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            if (hoSoBenhNhan == null)
            {
                return HttpNotFound();
            }
            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoSoBenhNhan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BenhNhan,HoTen,GioiTinh,NamSinh,DiaChi,NgayKham")] HoSoBenhNhan hoSoBenhNhan)
        {
            if (ModelState.IsValid)
            {

                if (hoSoBenhNhan.GioiTinh == true)
                    Session["GioiTinh"] = "Nam";
                else
                    Session["GioiTinh"] = "Nữ";
                db.HoSoBenhNhans.Add(hoSoBenhNhan);
                db.SaveChanges();
                //Session["NgayKham"] = hoSoBenhNhan.NgayKham;
                return RedirectToAction("DanhSachKhamBenh","HoSoBenhNhan");
            }

            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            if (hoSoBenhNhan == null)
            {
                return HttpNotFound();
            }
            return View(hoSoBenhNhan);
        }

        // POST: HoSoBenhNhan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BenhNhan,HoTen,GioiTinh,NamSinh,DiaChi,NgayKham")] HoSoBenhNhan hoSoBenhNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoSoBenhNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachKhamBenh","HoSoBenhNhan");
            }
            return View(hoSoBenhNhan);
        }

        // GET: HoSoBenhNhan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            if (hoSoBenhNhan == null)
            {
                return HttpNotFound();
            }
            return View(hoSoBenhNhan);
        }

        // POST: HoSoBenhNhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoSoBenhNhan hoSoBenhNhan = db.HoSoBenhNhans.Find(id);
            db.HoSoBenhNhans.Remove(hoSoBenhNhan);
            db.SaveChanges();
            return RedirectToAction("DanhSachKhamBenh","HoSoBenhNhan");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
