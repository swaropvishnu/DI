using DI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    public class UserController : Controller
    {
        //CMYSS_Applicant ad = new CMYSS_Applicant();
        public ActionResult CMYS_SchemeEntryForm()
        {
            CMYSS_Applicant CM = new CMYSS_Applicant();
            if (@UserSession.LoggedInUserName!=null)
            {
                //CM.adhar_no= 
            }
            return View();
        }
        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Doc> objdoc, List<CMYSS_Applicant_Family> objFamily, bool @sptype)
        {
            try
            {

                string str = new DAL.CommonDA().InsertUpdateCMYSS_Applicant(Objform, objdoc, objFamily, @sptype);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}