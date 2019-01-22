using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DI.Models;
using DI.BLL;
using System.Data;
using System.Text;
using System.Data.Entity.Infrastructure;
using DI.Filters;

namespace DI.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class IndustrialAreaMasterController : Controller
    {
        // GET: IndustrialAreaMaster
        IndustrialAreaMasterModal dd = new IndustrialAreaMasterModal();
        IndustrialEstateAllotee IEA = new IndustrialEstateAllotee();
        AddPlot AP = new AddPlot();
        AddShed As = new AddShed();
        CommonBL bl = new CommonBL();
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult testpage(string e)
        //{
        //   TEST t1 = new TEST();
        //    if (e=="E")
        //    {
        //        t1.IsPlot_Disputed2 = "0";
        //    }
           

        //    //TEST _radiobuttonliatmodel = new TEST();
        //    //DataTable DT = new DAL.CommonDA().Getplot(-1, -1, "", "", BLL.CommonBL.Setdate("01/01/1990"), BLL.CommonBL.Setdate("01/10/2018"), "", "");
        //    //_radiobuttonliatmodel.RadioButtonListData = new List<RadioButtonData>();


        //    //_radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = DT.Rows[1]["IsPlot_Disputed"].ToString().Trim(), Value = "हाँ" });
        //    //_radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = DT.Rows[1]["IsPlot_Disputed"].ToString().Trim(), Value = "नहीं" });

        //    return View(t1);
        //}
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
        public ActionResult GetDistrict()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDistricteng(string district_code_census, string state_code)
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "DSE", district_code_census.Trim(), state_code.Trim());
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getschemeeng()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ye", "", "");
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult addAreaView2()
        {
            return View();
        }
        public JsonResult InsertLoc(IndustrialAreaMasterModal M01,bool Isdel)
        {
            try
            {
                try
                {
                    DateTime Estyear = BLL.CommonBL.Setdate(M01.EditEstablishment);
                    M01.Establishment = Estyear;
                }
                catch (Exception)
                {

                    return Json("Establishment year must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
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
        public JsonResult InsertUpdateEstateAllotee(IndustrialEstateAllotee IEA, List<IndustrialEstateAlloteePlot> L01, List<IndustrialEstateAlloteeShed> L02 ,bool Sptype)
        {
            try
            {
                string str = new DAL.CommonDA().InsertUpdateEstateAllotee(IEA, L01, L02, Sptype);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetTehsil(string DistID)
        {
            List<SelectListItem> tehsilNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", tehsilNames, "TEHSIL", DistID.ToString(), "");
            return Json(tehsilNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult bindshed(string IndustrialEstate)
        {
            List<SelectListItem> shed = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", shed, "SV", IndustrialEstate.ToString(), "");
            return Json(shed, JsonRequestBehavior.AllowGet);
        }
        public ActionResult bindplot(string IndustrialEstate)
        {
            List<SelectListItem> plot = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", plot, "PV", IndustrialEstate.ToString(), "");
            return Json(plot, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBlock(string TehID)
        {
            List<SelectListItem> blockNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", blockNames, "block", TehID.ToString(), "");
            return Json(blockNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVillage(string BlockID)
        {
            List<SelectListItem> villageNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", villageNames, "village", BlockID.ToString(), "");
            return Json(villageNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompanyType()
        {
            List<SelectListItem> CompanyType = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", CompanyType, "CT", "-1", "");
          
            return Json(CompanyType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIndustrytype()
        {
            List<SelectListItem> CompanyType = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", CompanyType, "IT", "-1", "");

            return Json(CompanyType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEstate(int District)
        {
            List<SelectListItem> CompanyType = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", CompanyType, "IA", District.ToString().Trim(), "");
            return Json(CompanyType, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult AddPlot()
        {
            List<SelectListItem> DistrictName = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", DistrictName, "ds", "28", "10");
            AP.DistrictNames = DistrictName;
            return View(AP);
        }
        public ActionResult AddShed()
        {
            List<SelectListItem> DistrictName = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", DistrictName, "ds", "28", "10");
            As.DistrictNames = DistrictName;
            return View(As);
        }
        public ActionResult AddAllotee()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            IEA.DistrictNames = distNames;
            return View(IEA);
        }
        public ActionResult GetIndustrialEstateList()
        {
            return View();
        }
        public ActionResult ReportIndustrialEstate()
        {
            return View();
        }
        public ActionResult ReportAllotees()
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
        public ActionResult AddAlloteeWizard()
        {
            //List<SelectListItem> IndustrialArea = new List<SelectListItem>();
            //CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", IndustrialArea, "IA", "-1", "");
            //As.IndustrialEstate = IndustrialArea;
            return View();
        }
        public ActionResult GetAllotee()
        {
            //List<SelectListItem> IndustrialArea = new List<SelectListItem>();
            //CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", IndustrialArea, "IA", "-1", "");
            //As.IndustrialEstate = IndustrialArea;
            return View();
        }
        public ActionResult EditIndustrialEstate(string  Code)
        {
            string ID=  new DI.Crypto().Decrypt(Code);
            DataTable dt = new DAL.CommonDA().GetIndustrialEstateInfo(int.Parse(ID), "", -1, -1, -1, -1, "", "", "", BLL.CommonBL.Setdate("01/01/1900"), DateTime.Now, BLL.CommonBL.Setdate("01/01/1990"), DateTime.Now,"");
            IndustrialAreaMasterModal IA = new Models.IndustrialAreaMasterModal();
            IA.IndustrialEstateCode = int.Parse(dt.Rows[0]["IndustrialEstateCode"].ToString().Trim());
            IA.IndustrialEstateName = dt.Rows[0]["IndustrialEstateName"].ToString().Trim();
            IA.EditEstablishment = dt.Rows[0]["Establishment"].ToString().Trim();
            IA.Address = dt.Rows[0]["Address"].ToString().Trim();
            IA.DistrictCode = int.Parse(dt.Rows[0]["DistrictCode"].ToString().Trim());
            IA.TehsilCode = int.Parse(dt.Rows[0]["TehsilCode"].ToString().Trim());
            IA.BlockCode = int.Parse(dt.Rows[0]["BlockCode"].ToString().Trim());
            IA.VillageCode = int.Parse(dt.Rows[0]["VillageCode"].ToString().Trim());
            IA.PinCode = dt.Rows[0]["PinCode"].ToString().Trim();
            IA.AreaPerSqfeet = decimal.Parse(dt.Rows[0]["AreaPerSqfeet"].ToString().Trim());
            IA.RatePerSqFeet = decimal.Parse(dt.Rows[0]["rate_per_sqFeet"].ToString().Trim());
            IA.PlotNo = int.Parse(dt.Rows[0]["plot_count"].ToString().Trim());
            IA.ShadeNo = int.Parse(dt.Rows[0]["shade_count"].ToString().Trim());
            IA.NearestRailwayStationKm = decimal.Parse(dt.Rows[0]["NearestRailwayStationKm"].ToString().Trim());
            IA.NearestBusStationKM = decimal.Parse(dt.Rows[0]["NearestBusStationKM"].ToString().Trim());
            IA.IsStreet = dt.Rows[0]["is_street"].ToString().Trim()=="Y"?true:false;
            IA.IsElectriccity = dt.Rows[0]["is_electriccity"].ToString().Trim() == "Y" ? true : false;
            IA.IsDrainage = dt.Rows[0]["is_drainage"].ToString().Trim() == "Y" ? true : false;
            IA.IsDrinkingWater = dt.Rows[0]["is_drinking_water"].ToString().Trim() == "Y" ? true : false;
            IA.IsIndustrialPark = dt.Rows[0]["is_industrial_park"].ToString().Trim() == "Y" ? true : false;
            IA.IsRawMaterialsSiding = dt.Rows[0]["is_rawmaterials_Siding"].ToString().Trim() == "Y" ? true : false;
            IA.industrytype_code_diff = dt.Rows[0]["industrytype_code_diff"].ToString().Trim();
            return View(IA);
        }
        public ActionResult EditAllotee(string Code)
        {
            string ID = new DI.Crypto().Decrypt(Code);
            DataSet  ds = new DAL.CommonDA().GetEstateeAllotee(long.Parse(ID));
            IndustrialEstateAllotee IEA = new IndustrialEstateAllotee();
            IEA.allotee_code = long.Parse(ds.Tables[0].Rows[0]["allotee_code"].ToString().Trim());
            IEA.allotee_name = ds.Tables[0].Rows[0]["allotee_name"].ToString().Trim();
            IEA.company_name = ds.Tables[0].Rows[0]["company_name"].ToString().Trim();
            IEA.panno = ds.Tables[0].Rows[0]["panno"].ToString().Trim();
            IEA.cinno = ds.Tables[0].Rows[0]["cinno"].ToString().Trim();
            IEA.address = ds.Tables[0].Rows[0]["address"].ToString().Trim();
            IEA.district_code_census = int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
            IEA.tehsil_code_census = int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
            IEA.block_code = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim());
            IEA.village_code = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
            IEA.mobile = ds.Tables[0].Rows[0]["mobile"].ToString().Trim();
            IEA.email = (ds.Tables[0].Rows[0]["email"].ToString().Trim());
            IEA.industrytype_code_diff = (ds.Tables[0].Rows[0]["industrytype_code_diff"].ToString().Trim());
            IEA.companytype_code = int.Parse(ds.Tables[0].Rows[0]["companytype_code"].ToString().Trim());
            IEA.estimated_cost = decimal.Parse(ds.Tables[0].Rows[0]["estimated_cost"].ToString().Trim());
            IEA.projected_employment = int.Parse(ds.Tables[0].Rows[0]["projected_employment"].ToString().Trim());
            IEA.estimate_time = int.Parse(ds.Tables[0].Rows[0]["estimate_time"].ToString().Trim());
            IEA.uncovered_area = decimal.Parse(ds.Tables[0].Rows[0]["uncovered_area"].ToString().Trim());
            IEA.covered_area = decimal.Parse(ds.Tables[0].Rows[0]["covered_area"].ToString().Trim());
            IEA.land_investment = decimal.Parse(ds.Tables[0].Rows[0]["land_investment"].ToString().Trim());
            IEA.other_investment = decimal.Parse(ds.Tables[0].Rows[0]["other_investment"].ToString().Trim());
            IEA.building_investment = decimal.Parse(ds.Tables[0].Rows[0]["building_investment"].ToString().Trim());
            IEA.equipment_investment = decimal.Parse(ds.Tables[0].Rows[0]["equipment_investment"].ToString().Trim());
            IEA.property_cost = decimal.Parse(ds.Tables[0].Rows[0]["property_cost"].ToString().Trim());
            IEA.is_produce_smoke =ds.Tables[0].Rows[0]["is_produce_smoke"].ToString().Trim()=="1"?true:false;
            IEA.is_electricity = ds.Tables[0].Rows[0]["is_electricity"].ToString().Trim() == "1" ? true : false;
            IEA.is_telephone = ds.Tables[0].Rows[0]["is_telephone"].ToString().Trim() == "1" ? true : false;
            IEA.grossprofit = decimal.Parse(ds.Tables[0].Rows[0]["grossprofit"].ToString().Trim());
            IEA.industrial_estate_code = int.Parse(ds.Tables[0].Rows[0]["industrial_estate_code"].ToString().Trim());
            IEA.industrial_estate_code_distict = int.Parse(ds.Tables[0].Rows[0]["Estate_district"].ToString().Trim());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>"+(i+1)+"</td>");
                sb.Append("<td><input type='hidden' id='hfIndustrialEstate"+(i+1)+"' name='hfIndustrialEstate' value='"+ ds.Tables[1].Rows[i]["industrial_estate_code"].ToString().Trim() + "'><label>" + ds.Tables[1].Rows[i]["industrial_estate_name"].ToString().Trim() + "</label></td>");
                sb.Append("<td>"+ ds.Tables[1].Rows[i]["type"].ToString().Trim() +"</td>");
                sb.Append("<td><input type='hidden' id='hfShedName" + (i + 1) + "' name='hfShedName' value='" + ds.Tables[1].Rows[i]["plot_code"].ToString().Trim() + "'><input type='hidden' id='hfShedtype" + (i + 1) + "' name='hfShedtype' value='"+ ds.Tables[1].Rows[i]["typecode"].ToString().Trim() + "'> <label>"+ ds.Tables[1].Rows[i]["display"].ToString().Trim() + "</label></td>");
                sb.Append("<td><input type='text' id='txtAlotmentNo" + (i + 1) + "' placeholder='Title' name='txtAlotmentNo' value='"+ ds.Tables[1].Rows[i]["allotmentno"].ToString().Trim() + "' maxlength='8' disabled='disabled'></td>");
                sb.Append("<td><input type='submit' value='Delete' class='btn red' disabled='disabled'  id='btnadd' onclick='del(" + (i+1) + ")'></td>");

                sb.Append("</tr>");
            }
            IEA.tablestring = sb.ToString().Trim();
            return View(IEA);
        }
        public ActionResult Adddoc_type()
        {
            //List<SelectListItem> doc_type = new List<SelectListItem>();
            //CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", doc_type, "doc_type", "", "");
            //DT.Docment = doc_type;
            return View();
        }
        public JsonResult InsertUpdate_doc_type(Doc_type Objform, string sptype)
        {
            try
            {
                Objform.Mode = sptype;
                string str = new DAL.CommonDA().InsertUpdate_doc_type(Objform);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Affidavit_Letter()
        {
            return View();
        }
        public ActionResult certificate_Letter()
        {
            return View();
        }
    }
}