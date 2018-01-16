using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DI.Models;

namespace DI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // If the browser session or authentication session has expired...
            //if (ctx.Session["tbl_Session"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
            if (ctx.Session["tbl_Session"] == null )
            {
                

                filterContext.Result =
              new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Login" }));

                    return;
            }

            //base.OnActionExecuting(filterContext);
        }
    }
}