using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppMain.Controllers
{
    public class ThayDoiQuyDinhController : Controller
    {
        // GET: ThayDoiQuyDinh
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThayDoiSoLuongBenhNhan()
        {
            return File(@"F:\CNPM nâng cao\Lap 4\SQL++\TriggerSoLuongBenhNhanToiDa.sql", "application/sql");


        }
    }
}