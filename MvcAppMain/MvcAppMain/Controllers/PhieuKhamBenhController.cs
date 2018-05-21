using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Filters;
using MvcAppMain.Models;

namespace MvcAppMain.Controllers
{
    
    public class PhieuKhamBenhController : Controller
    {
        QLPMContext db = new QLPMContext();

        // GET: PhieuKhamBenh
        
        public ActionResult Index()
        {
            var phieuKhamBenhs = db.PhieuKhamBenhs.Include(p => p.Benh).Include(p => p.HoSoBenhNhan);
            return View(phieuKhamBenhs.ToList());
        }
        
        public ActionResult DanhSachKhamBenh(DateTime? date) {
            if (!date.HasValue) date = date.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);

            ViewBag.date = date;
            var phieuKhamBenhs = db.PhieuKhamBenhs.Include(p => p.Benh).Include(p => p.HoSoBenhNhan).Where(x => x.NgayKham == date);
            return View(phieuKhamBenhs.ToList());
        }
        // GET: PhieuKhamBenh/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuKhamBenh phieuKhamBenh = db.PhieuKhamBenhs.Find(id);
            if (phieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            return View(phieuKhamBenh);
        }

        // GET: PhieuKhamBenh/PhieuKhamBenh
        
        
        public ActionResult PhieuKhamBenh()
        {
            
            ViewBag.ID_Benh = new SelectList(db.Benhs, "ID_Benh", "TenBenh");
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "HoTen");
            //ViewBag.NgayKham = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "NgayKham");
            //ViewBag.ID_NgayKham = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "NgayKham");
            //ViewBag.NgayKham = from e in db.HoSoBenhNhans where db.HoSoBenhNhans.ID_ = GetIDBenhNhan(ID_NgayKham)
                               
            return View();
        }

        //public int GetIDBenhNhan(int ID_NgayKham) {

        //}
        
        public ActionResult DanhSachBenhNhan(string searchString)
        {
            //var phieuKhamBenhs = db.PhieuKhamBenhs.Include(p => p.Benh).Include(p => p.HoSoBenhNhan);
            var phieuKhamBenhs = from e in db.PhieuKhamBenhs.Include(p => p.Benh).Include(p => p.HoSoBenhNhan)
                                 select e;
            if (!String.IsNullOrEmpty(searchString)) {
                phieuKhamBenhs = phieuKhamBenhs.Where(s => s.HoSoBenhNhan.HoTen.Contains(searchString));
            }
            return View(phieuKhamBenhs.ToList());
        }
        // POST: PhieuKhamBenh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhieuKhamBenh([Bind(Include = "ID_PhieuKham,ID_BenhNhan,ID_Benh,NgayKham,TrieuChung")] PhieuKhamBenh phieuKhamBenh)
        {
            if (ModelState.IsValid)
            {
                db.PhieuKhamBenhs.Add(phieuKhamBenh);
                db.SaveChanges();
                Session["ID_PhieuKham"] = phieuKhamBenh.ID_PhieuKham;
                return RedirectToAction("Create","CT_PhieuKhamBenh");
            }

            
                    //ViewBag.ID_Benh = new SelectList(db.CT_PhieuKhamBenh, "ID_Thuoc", phieuKhamBenh.CT_PhieuKhamBenh.ID_);
            ViewBag.ID_Benh = new SelectList(db.Benhs, "ID_Benh", "TenBenh", phieuKhamBenh.ID_Benh);
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "HoTen", phieuKhamBenh.ID_BenhNhan);
            return View(phieuKhamBenh);
        }
       
        // GET: PhieuKhamBenh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuKhamBenh phieuKhamBenh = db.PhieuKhamBenhs.Find(id);
            if (phieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Benh = new SelectList(db.Benhs, "ID_Benh", "TenBenh", phieuKhamBenh.ID_Benh);
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "HoTen", phieuKhamBenh.ID_BenhNhan);
            return View(phieuKhamBenh);
        }

        // POST: PhieuKhamBenh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PhieuKham,ID_BenhNhan,ID_Benh,TrieuChung")] PhieuKhamBenh phieuKhamBenh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuKhamBenh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Benh = new SelectList(db.Benhs, "ID_Benh", "TenBenh", phieuKhamBenh.ID_Benh);
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans, "ID_BenhNhan", "HoTen", phieuKhamBenh.ID_BenhNhan);
            return View(phieuKhamBenh);
        }

        // GET: PhieuKhamBenh/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuKhamBenh phieuKhamBenh = db.PhieuKhamBenhs.Find(id);
            if (phieuKhamBenh == null)
            {
                return HttpNotFound();
            }
            return View(phieuKhamBenh);
        }

        // POST: PhieuKhamBenh/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuKhamBenh phieuKhamBenh = db.PhieuKhamBenhs.Find(id);
            db.PhieuKhamBenhs.Remove(phieuKhamBenh);
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
