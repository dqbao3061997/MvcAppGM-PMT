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
    public class HoaDonController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: HoaDon
        public ActionResult Index()
        {
            var hoadon = from e in db.HoaDons
                                   select e;
            int iD = Convert.ToInt32(Session["ID_PhieuKham"]);
            if (!String.IsNullOrEmpty(iD.ToString()))
            {
                hoadon = hoadon.Where(s => s.ID_PhieuKham.ToString().Contains(iD.ToString()));
            }
            return View(hoadon.ToList());
            //var hoaDons = db.HoaDons.Include(h => h.PhieuKhamBenh);
            //return View(hoaDons.ToList());
        }

        // GET: HoaDon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_HoaDon,ID_PhieuKham,TienKham,TienThuoc,DoanhThu,GhiChu")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", hoaDon.ID_PhieuKham);
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", hoaDon.ID_PhieuKham);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HoaDon,ID_PhieuKham,TienKham,TienThuoc,DoanhThu,GhiChu")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", hoaDon.ID_PhieuKham);
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
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
