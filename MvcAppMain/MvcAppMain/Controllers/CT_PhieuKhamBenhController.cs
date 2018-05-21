using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using MvcAppMain.Models;
using MvcAppMain.Report;
using MvcAppMain.Report.DSBaoCaoSuDungThuocTableAdapters;

namespace MvcAppMain.Controllers
{
    public class CT_PhieuKhamBenhController : Controller
    {
        QLPMContext db = new QLPMContext();

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

        public ActionResult BaoCaoSuDungThuoc() {
            var baocaosudungthuoc = db.CT_PhieuKhamBenh.Include(p => p.PhieuKhamBenh).Include(p => p.Thuoc).Include(p => p.CachDung);
            return View(baocaosudungthuoc.ToList());
        }

        //public ActionResult Export()
        //{


        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report/CRBaoCaoSuDungThuoc.rpt")));
        //    DSBaoCaoSuDungThuoc dataset = new DSBaoCaoSuDungThuoc();
        //    ThuocTableAdapter thuocAdapter = new ThuocTableAdapter();
        //    //HoaDonTableAdapter hoadonAdapter = new HoaDonTableAdapter();
        //    //hoadonAdapter.Fill(dataset.HoaDon);
        //    thuocAdapter.Fill(dataset.Thuoc);

        //    rd.SetDataSource(dataset);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();

        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "BaoCaoSuDungThuocTheoNgay.pdf");

        //}

        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Report(FormCollection collection)
        {
            try
            {
                int date = Convert.ToInt32(collection["month"].ToString());

                return RedirectToAction("ReportThuoc", new { dt = date });
            }
            catch (FormatException)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng tháng");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng tháng");
                return View();
            }
        }
        public ActionResult CreateDefaultFolder()
        {
            string rootDirectory = "C:\\ReportThuoc";
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
                ViewBag.Success = "Thành công";
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Thư mục đã tồn tại!!");
                return View("Report");
            }
            return View("Report");

        }

        public ActionResult ReportThuoc(int dt)
        {
            SetLocalReport(dt);

            return View();
        }
        private void SetLocalReport(int dt)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(500);
            reportViewer.Height = Unit.Percentage(500);

            DSBaoCaoSuDungThuoc dataset = new DSBaoCaoSuDungThuoc();
            ThuocTableAdapter thuocTableAdapter = new ThuocTableAdapter();
            thuocTableAdapter.Fill(dataset.Thuoc, dt);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report\ReportBaoCaoSuDungThuoc.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBaoCaoSuDungThuoc", dataset.Tables[0]));

            ViewBag.ReportViewer = reportViewer;
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
