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
    public class CT_PhieuKhamBenhController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: CT_PhieuKhamBenh
        public ActionResult Index()
        {
            var ct_phieukhambenh = from e in db.CT_PhieuKhamBenh
                                   select e;
            int iD = Convert.ToInt32(Session["ID_PhieuKham"]);
            if (!String.IsNullOrEmpty(iD.ToString())) {
                ct_phieukhambenh = ct_phieukhambenh.Where(s => s.ID_PhieuKham.ToString().Contains(iD.ToString()));
            }
            return View(ct_phieukhambenh.ToList());
            //var cT_PhieuKhamBenh = db.CT_PhieuKhamBenh.Include(c => c.CachDung).Include(c => c.PhieuKhamBenh).Include(c => c.Thuoc);
            //return View(cT_PhieuKhamBenh.ToList());
        }

        // GET: CT_PhieuKhamBenh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PhieuKhamBenh cT_PhieuKhamBenh = db.CT_PhieuKhamBenh.Find(id);
            if (cT_PhieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            return View(cT_PhieuKhamBenh);
        }

        // GET: CT_PhieuKhamBenh/Create
        public ActionResult Create()
        {
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung");
            //ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung");
            ViewBag.ID_PhieuKham = Session["ID_PhieuKham"];
            ViewBag.ID_Thuoc = new SelectList(db.Thuocs, "ID_Thuoc", "TenThuoc");
            //ViewBag.ID_DonVi = new SelectList(db.Thuocs, "ID_Thuoc", "ID_DonVi");
            return View();
        }

        // POST: CT_PhieuKhamBenh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PhieuKham,ID_Thuoc,ID_CachDung,SoLuongThuocLay")] CT_PhieuKhamBenh cT_PhieuKhamBenh)
        {
            if (ModelState.IsValid)
            {
                db.CT_PhieuKhamBenh.Add(cT_PhieuKhamBenh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", cT_PhieuKhamBenh.ID_CachDung);
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", cT_PhieuKhamBenh.ID_PhieuKham);
            ViewBag.ID_Thuoc = new SelectList(db.Thuocs, "ID_Thuoc", "TenThuoc", cT_PhieuKhamBenh.ID_Thuoc);
            return View(cT_PhieuKhamBenh);
        }

        // GET: CT_PhieuKhamBenh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PhieuKhamBenh cT_PhieuKhamBenh = db.CT_PhieuKhamBenh.Find(id);
            if (cT_PhieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", cT_PhieuKhamBenh.ID_CachDung);
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", cT_PhieuKhamBenh.ID_PhieuKham);
            ViewBag.ID_Thuoc = new SelectList(db.Thuocs, "ID_Thuoc", "TenThuoc", cT_PhieuKhamBenh.ID_Thuoc);
            return View(cT_PhieuKhamBenh);
        }

        // POST: CT_PhieuKhamBenh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PhieuKham,ID_Thuoc,ID_CachDung,SoLuongThuocLay")] CT_PhieuKhamBenh cT_PhieuKhamBenh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_PhieuKhamBenh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", cT_PhieuKhamBenh.ID_CachDung);
            ViewBag.ID_PhieuKham = new SelectList(db.PhieuKhamBenhs, "ID_PhieuKham", "TrieuChung", cT_PhieuKhamBenh.ID_PhieuKham);
            ViewBag.ID_Thuoc = new SelectList(db.Thuocs, "ID_Thuoc", "TenThuoc", cT_PhieuKhamBenh.ID_Thuoc);
            return View(cT_PhieuKhamBenh);
        }

        // GET: CT_PhieuKhamBenh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PhieuKhamBenh cT_PhieuKhamBenh = db.CT_PhieuKhamBenh.Find(id);
            if (cT_PhieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            return View(cT_PhieuKhamBenh);
        }

        // POST: CT_PhieuKhamBenh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PhieuKhamBenh cT_PhieuKhamBenh = db.CT_PhieuKhamBenh.Find(id);
            db.CT_PhieuKhamBenh.Remove(cT_PhieuKhamBenh);
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
