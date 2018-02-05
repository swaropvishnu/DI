using DI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using Jmelosegui.Mvc.GoogleMap;

namespace DI.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(int? height, int? width)
        {
            this.ViewData["height"] = height ?? 0;
            this.ViewData["width"] = width ?? 0;
            return View();
        }

        public ActionResult IndustryLocation(MarkerClusteringOptions options, string customStyles)
        {
            this.ViewData["MaxZoom"] = options.MaxZoom;
            this.ViewData["GridSize"] = options.GridSize;
            this.ViewData["AverageCenter"] = options.AverageCenter;
            this.ViewData["ZoomOnClick"] = options.ZoomOnClick;
            this.ViewData["HideSingleGroupMarker"] = options.HideSingleGroupMarker;
            this.ViewData["CustomStyles"] = customStyles;

            return this.View(App_Data.DataContext.GetHugeAmountOfMarkers());
        }
    }
}