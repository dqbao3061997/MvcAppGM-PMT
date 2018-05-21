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
    public class CachDungController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: CachDung
        public ActionResult Index()
        {
            return View(db.CachDungs.ToList());
        }

        // GET: CachDung/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CachDung cachDung = db.CachDungs.Find(id);
            if (cachDung == null)
            {
                return HttpNotFound();
            }
            return View(cachDung);
        }

        // GET: CachDung/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CachDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CachDung,TenCachDung,SoLanDung")] CachDung cachDung)
        {
            if (ModelState.IsValid)
            {
                db.CachDungs.Add(cachDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cachDung);
        }

        // GET: CachDung/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CachDung cachDung = db.CachDungs.Find(id);
            if (cachDung == null)
            {
                return HttpNotFound();
            }
            return View(cachDung);
        }

        // POST: CachDung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CachDung,TenCachDung,SoLanDung")] CachDung cachDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cachDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cachDung);
        }

        // GET: CachDung/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CachDung cachDung = db.CachDungs.Find(id);
            if (cachDung == null)
            {
                return HttpNotFound();
            }
            return View(cachDung);
        }

        // POST: CachDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CachDung cachDung = db.CachDungs.Find(id);
            db.CachDungs.Remove(cachDung);
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
