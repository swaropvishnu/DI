using DI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

namespace DI.Controllers
{
    [SessionExpireFilterAttribute]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            //this.ViewData["height"] = height ?? 0;
            //this.ViewData["width"] = width ?? 0;
            return View();
        }
    }
}