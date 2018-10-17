using DI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using Jmelosegui.Mvc.GoogleMap;
using DI.Models;
using System.Text;
using System.Data;

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
        public ActionResult IndustryCluster()
        {
            return View();
        }
        public ActionResult MYSY_Dashboard()
        {
            MYSY_Dashboard MYSY = new MYSY_Dashboard();
            StringBuilder str = new StringBuilder();
            DataTable dt = new DAL.CommonDA().GetMYSYDashboard(-1);
            if (dt!=null)
            {
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                         if (((i+1)%2)==1)
                        {
                            str.Append("<div class='row'>");
                        }
                        str.Append("<div class='col-lg-6 col-xs-12 col-sm-12'>");
                        str.Append("<div class='portlet light '>");
                        str.Append("<div class='portlet-title'>");
                        str.Append("<div class='caption'>");
                        str.Append("<i class='icon-cursor font-dark hide'></i>");
                        str.Append("<span class='caption-subject font-dark bold uppercase'>"+dt.Rows[i]["dist_name"].ToString().Trim() +" Stats</span>");
                        str.Append("</div>");
                        str.Append("<div class='actions'>");
                        str.Append("<a href='javascript:;' class='btn btn-sm btn-circle red easy-pie-chart-reload'>");
                        str.Append("<i class='fa fa-repeat'></i> Reload");
                        str.Append("</a>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("<div class='portlet-body'>");
                        str.Append("<div class='row'>");
                        str.Append("<div class='col-md-4'>");
                        str.Append("<div class='easy-pie-chart'>");
                        str.Append("<div class='number transactions' data-percent='55'>");
                        str.Append("<span>+55</span>%");
                        str.Append("</div>");
                        str.Append("<a class='title_small' href='javascript:;'>");
                        str.Append("Approved By DIC  ");
                        str.Append("<i class='icon-arrow-right'></i>");
                        str.Append("</a>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("<div class='margin-bottom-10 visible-sm'> </div>");
                        str.Append("<div class='col-md-4'>");
                        str.Append("<div class='easy-pie-chart'>");
                        str.Append("<div class='number visits' data-percent='85'>");
                        str.Append("<span>+85</span>%");
                        str.Append("</div>");
                        str.Append("<a class='title_small' href='javascript:;'>");
                        str.Append("Approved By DLTFC  ");
                        str.Append("<i class='icon-arrow-right'></i>");
                        str.Append("</a>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("<div class='margin-bottom-10 visible-sm'> </div>");
                        str.Append("<div class='col-md-4'>");
                        str.Append("<div class='easy-pie-chart'>");
                        str.Append("<div class='number bounce' data-percent='46'>");
                        str.Append("<span>-46</span>%");
                        str.Append("</div>");
                        str.Append("<a class='title_small' href='javascript:;'>");
                        str.Append("Approved By Bank  ");
                        str.Append("<i class='icon-arrow-right'></i>");
                        str.Append("</a>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("</div>");
                        str.Append("</div>");
                         if (((i+1) % 2) == 0)
                        {
                            str.Append("</div>");
                        }
                    }
                }
            }
            MYSY.Dashboard = str.ToString().Trim();
            return View(MYSY);
        }
    }
}