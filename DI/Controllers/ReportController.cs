using DI.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class ReportController : Controller
    {
        public ActionResult SummaryReportDIC()
        {
            return View();
        }
        public ActionResult StateAdmin_summary_report()
        {
            return View();
        }
        public ActionResult stateadmin_bankReport()
        {
            return View();
        }
        public ActionResult MYSY_Applicant_status_report()
        {
            return View();
        }

    }
}