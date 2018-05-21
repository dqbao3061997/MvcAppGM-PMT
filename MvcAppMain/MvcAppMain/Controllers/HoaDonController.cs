using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using MvcAppMain.Models;
using MvcAppMain.Report;
using MvcAppMain.Report.DSBaoCaoDoanhThuTableAdapters;

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
        public ActionResult Success() {
            return View();
        }
        public ActionResult Backup() {
            
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["QLPMContext"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_BKHoaDon";
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            return RedirectToAction("Success");
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

        public ActionResult BaoCaoDoanhThu() {

            var baocaodoanhthu = db.HoaDons.Include(p => p.PhieuKhamBenh);
            return View(baocaodoanhthu.ToList());
        }

        //public ActionResult Export() {


        //ReportDocument rd = new ReportDocument();
        //rd.Load(Path.Combine(Server.MapPath("~/Report/CRBaoCaoDoanhThu.rpt")));
        //DSBaoCaoDoanhThu dataset = new DSBaoCaoDoanhThu();
        //PhieuKhamBenhTableAdapter phieukhamAdapter = new PhieuKhamBenhTableAdapter();
        ////HoaDonTableAdapter hoadonAdapter = new HoaDonTableAdapter();
        ////hoadonAdapter.Fill(dataset.HoaDon);
        //phieukhamAdapter.Fill(dataset.PhieuKhamBenh);

        //rd.SetDataSource(dataset);
        //Response.Buffer = false;
        //Response.ClearContent();
        //Response.ClearHeaders();

        //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //stream.Seek(0, SeekOrigin.Begin);
        //return File(stream, "application/pdf", "BaoCaoDoanhThuTheoNgay.pdf");


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
                
                return RedirectToAction("ReportDoanhThu", new { dt = date});
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
            string rootDirectory = "C:\\ReportHoaDon";
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

        public ActionResult ReportDoanhThu(int dt)
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

            DSBaoCaoDoanhThu dataset = new DSBaoCaoDoanhThu();
            PhieuKhamBenhTableAdapter phieukhambenhTableAdapter = new PhieuKhamBenhTableAdapter();
            phieukhambenhTableAdapter.Fill(dataset.PhieuKhamBenh, dt);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report\ReportBaoCaoDoanhThu.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBaoCaoDoanhThu", dataset.Tables[0]));

            ViewBag.ReportViewer = reportViewer;
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
