using DI.BLL;
using DI.Filters;
using DI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    public class FormController : Controller
    {
        // GET: Form
         [SessionExpireFilterAttribute]
        public ActionResult EntryForm()
        {
            return View();
        }
          [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult EntryForm(FormModal Objform)
         {
            Objform.UserId = UserSession.LoggedInUserId.ToString();
            Objform.Mode = "Insert";
            string result = CommonBL.InsertUpdateApplicationFormDetail(Objform);
            if (result=="Success")
            {
                ViewBag.Alert = "Application Insert Successfully";  
            }
            else
            {
                ViewBag.Alert = "Some Error In Insert Application";  
            }

            return RedirectToAction("EntryForm", "Form");
         }

    }
}