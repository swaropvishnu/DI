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
        IndustrialAreaMasterModal dd = new IndustrialAreaMasterModal();
        AddMYSY ad = new AddMYSY();
        CommonBL bl = new CommonBL();


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
        [SessionExpireFilterAttribute]
        public ActionResult DistrictIndrustiesInformation()
        {
            ViewBag.DisLst = DllDistrict();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DistrictIndrustiesInformation(FormModal Objform)
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




        public ActionResult MYSYEntry()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            ad.DistrictNames = distNames;
            return View(ad);

        }
        public ActionResult MYSY_Entry()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            ad.DistrictNames = distNames;
            return View(ad);

        }

        [HttpPost]
        public ActionResult GetTehsil(string DistID)
        {
            List<SelectListItem> tehsilNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", tehsilNames, "TEHSIL", DistID.ToString(), "");
            return Json(tehsilNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetBlock(string TehID)
        {
            List<SelectListItem> blockNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", blockNames, "block", TehID.ToString(), "");
            return Json(blockNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetVillage(string BlockID)
        {
            List<SelectListItem> villageNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", villageNames, "village", BlockID.ToString(), "");
            return Json(villageNames, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult InsertUpdateMYSY(AddMYSY Objform)
        //{
        //    try
        //    {
        //        Objform.Mode = "Insert";
        //        string str =""// new DAL.CommonDA().InsertUpdateMYSY(Objform);
        //        return Json(str, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult GetMYSY()
        {
            return View();

        }

        public ActionResult InsertUpdateYojona_Master()
        {
            return View();
        }

        public JsonResult InsertUpdateYojona_Master_(Yojona_Master Objform, string sptype)
        {
            try
            {
                Objform.Mode = sptype;
                string str = new DAL.CommonDA().InsertYojona_Master(Objform);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Get_Data_MYSY()
        {
            return View();
        }

        public ActionResult Entry_plant_machinery()
        {
            return View();
        }
    }
}