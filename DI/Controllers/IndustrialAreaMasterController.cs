using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DI.Models;
using DI.BLL;

namespace DI.Controllers
{
    public class IndustrialAreaMasterController : Controller
    {
        // GET: IndustrialAreaMaster

        IndustrialAreaMasterModal dd = new IndustrialAreaMasterModal();
        AddPlot AP = new AddPlot();
        AddShed As = new AddShed();
        CommonBL bl = new CommonBL();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddAreaView()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            dd.DistrictNames = distNames;
            return View(dd);
        }
        public ActionResult IndustrialEstateEntryForm()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            dd.DistrictNames = distNames;
            return View(dd);
        }
        public ActionResult addAreaView2()
        {
            return View();
        }
        public JsonResult InsertLoc(IndustrialAreaMasterModal M01,bool Isdel)
        {
            try
            {
                string str = new DAL.CommonDA().InsertLoc(M01, Isdel);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult InsertUpdatePlot(AddPlot M01,bool sptype)
        {
            try
            {
                string str = new DAL.CommonDA().InsertUpdatePlot(M01, sptype);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult InsertUpdateShed(AddShed M01, bool sptype)
        {
            try
            {
                if (M01.ShedName=="")
                {
                    return Json("Please Enter Shed Name", JsonRequestBehavior.AllowGet);
                }
                string str = new DAL.CommonDA().InsertUpdateShed(M01, sptype);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddAreaView(IndustrialAreaMasterModal Objform)
        //{
        //    //Objform.UserId = UserSession.LoggedInUserId.ToString();
        //    Objform.Mode = "Insert";
        //    string result = CommonBL.Details_Established_industrialBal(Objform);
        //    if (result == "Success")
        //    {
        //        ViewBag.Alert = "Application Insert Successfully";
        //    }
        //    else
        //    {
        //        ViewBag.Alert = "Some Error In Insert Application";
        //    }

        //    return View();
        //    //return RedirectToAction("EntryForm", "Form");
        //}

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

        //[HttpGet]
        //public ActionResult AddPlot()
        //{
        //    return View();
        //}

        public ActionResult AddPlot()
        {
            List<SelectListItem> IndustrialArea = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", IndustrialArea, "IA", "-1", "");
            AP.IndustrialEstate = IndustrialArea;
            return View(AP);
        }

        public ActionResult AddShed()
        {
            List<SelectListItem> IndustrialArea = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", IndustrialArea, "IA", "-1", "");
            As.IndustrialEstate = IndustrialArea;
            return View(As);
        }

        public ActionResult AddAllotee()
        {
            //List<SelectListItem> IndustrialArea = new List<SelectListItem>();
            //CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", IndustrialArea, "IA", "-1", "");
            //As.IndustrialEstate = IndustrialArea;
            return View();
        }
        public ActionResult GetIndustrialEstateList()
        {
            return View();
        }

        public ActionResult GetShed()
        {
            return View();
        }

        public ActionResult GetPlot()
        {
            return View();
        }
    }
}