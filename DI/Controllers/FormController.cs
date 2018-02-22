using DI.BLL;
using DI.Filters;
using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (result == "Success")
            {
                ViewBag.Alert = "Application Insert Successfully";

            }
            else
            {
                ViewBag.Alert = "Some Error In Insert Application";
            }

            return View();

            //return RedirectToAction("EntryForm", "Form");
        }
        [SessionExpireFilterAttribute]
        public ActionResult EntryFormReort()
        {
            FormModal Objform = new FormModal();
            Objform.Mode = "Select";

            List<FormModal> Obj = CommonBL.ReportApplicationFormDetail(Objform).ToList();
            return View(Obj);

        }
        public ActionResult DistrictIndrustiesInformation()
        {
            ViewBag.DisLst = DllDistrict();
            return View();
        }
        #region Dropdown
        //For DllDist
        public List<SelectListItem> DllDistrict()
        {
            

                DataSet objDt = new DataSet();
                objDt=CommonBL.bindDropDownHn("proc_Detail", "DS", "","");
                List<SelectListItem> objLst = new List<SelectListItem>();
                SelectListItem Items;
                Items = new SelectListItem();
                Items.Text = "--SELECT--";
                Items.Value = "-1";

                objLst.Add(Items);
            if (objDt !=null && objDt.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < objDt.Tables[0].Rows.Count; i++)
                {
                    Items = new SelectListItem();
                    Items.Text = objDt.Tables[0].Rows[i]["ValueText"].ToString();
                    Items.Value = objDt.Tables[0].Rows[i]["ValueId"].ToString();

                    objLst.Add(Items);
                }
            }
                
                return objLst;
            
        }
        #endregion
    }
}