using DI.Filters;
using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DI.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class UserController : Controller
    {
        public ActionResult AllotmentPlot()
        {
            return View();
        }
        public ActionResult spy_affidavite()
        {
            return View();
        }
        public ActionResult MHPY_Affidavit()
        {
            return View();
        }
        public ActionResult Print_MHPY()
        {
            return View();
        }
        public ActionResult MYSY_Plant_Machinery_print()
        {
            return View();
        }
        public ActionResult Print_spy()
        {
            return View();
        }
        public ActionResult MHPY_EntryForm()
        {
            MHPY_Applicant MHPY = new MHPY_Applicant();
            try
            {
                if (@UserSession.LoggedInUserName != null)
                {
                    DataSet ds = new DAL.CommonDA().GetApplicantinfo_mhpy(-1, -1, @UserSession.LoggedInUserName);
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            MHPY.applicant_name = ds.Tables[0].Rows[0]["applicant_name"].ToString().Trim();
                            MHPY.steps = ds.Tables[0].Rows[0]["steps"].ToString().Trim();
                            MHPY.inputdob = ds.Tables[0].Rows[0]["dob"].ToString().Trim();
                            MHPY.applicant_code = long.Parse(ds.Tables[0].Rows[0]["registration_code"].ToString().Trim());
                            MHPY.yojana_code = short.Parse(ds.Tables[0].Rows[0]["scheme_code"].ToString().Trim());
                            MHPY.adhar_no = ds.Tables[0].Rows[0]["adhar_no"].ToString().Trim();
                            MHPY.Husband_father_name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            MHPY.current_address = ds.Tables[0].Rows[0]["current_address"].ToString().Trim();
                            MHPY.permanent_address = ds.Tables[0].Rows[0]["permanent_address"].ToString().Trim();
                            MHPY.mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString().Trim();
                            MHPY.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                            MHPY.DistCode = int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
                            MHPY.TehsilCode = int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
                            MHPY.BlockCode = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()); ;
                            MHPY.VillCode = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
                            MHPY.artisian_no = ds.Tables[0].Rows[0]["artisian_no"].ToString().Trim();
                            MHPY.handicraft_work = (ds.Tables[0].Rows[0]["handicraft_work"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(MHPY);
        }
        public ActionResult SPY_EntryForm()
        {
            SPY_Applicant spy = new SPY_Applicant();
            try
            {
                if (@UserSession.LoggedInUserName != null)
                {
                    DataSet ds = new DAL.CommonDA().GetApplicantinfo_spy(-1, -1, @UserSession.LoggedInUserName);
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            spy.applicant_name = ds.Tables[0].Rows[0]["applicant_name"].ToString().Trim();
                            spy.working_craftwork = ds.Tables[0].Rows[0]["working_craftwork"].ToString().Trim();
                            spy.steps = ds.Tables[0].Rows[0]["steps"].ToString().Trim();
                            spy.inputdob = ds.Tables[0].Rows[0]["dob"].ToString().Trim();
                            spy.applicant_code = long.Parse(ds.Tables[0].Rows[0]["registration_code"].ToString().Trim());
                            spy.yojana_code = short.Parse(ds.Tables[0].Rows[0]["scheme_code"].ToString().Trim());
                            spy.adhar_no = ds.Tables[0].Rows[0]["adhar_no"].ToString().Trim();
                            spy.Husband_father_name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            spy.current_address = ds.Tables[0].Rows[0]["current_address"].ToString().Trim();
                            spy.permanent_address = ds.Tables[0].Rows[0]["permanent_address"].ToString().Trim();
                            spy.mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString().Trim();
                            spy.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                            spy.DistCode = int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
                            spy.TehsilCode = int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
                            spy.BlockCode = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()); ;
                            spy.VillCode = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
                            spy.artisian_no = ds.Tables[0].Rows[0]["artisian_no"].ToString().Trim();
                            spy.awards = ds.Tables[0].Rows[0]["awards"].ToString().Trim();
                            spy.handicraft_work = (ds.Tables[0].Rows[0]["handicraft_work"].ToString().Trim());
                            spy.work_exp = short.Parse(ds.Tables[0].Rows[0]["work_exp"].ToString().Trim());
                            spy.inputawardyear = ds.Tables[0].Rows[0]["award_yearDATE"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(spy);
        }
        public ActionResult VHPP_Entryform()
        {
            vhpp_Applicant VH = new vhpp_Applicant();
            try
            {
                if (@UserSession.LoggedInUserName != null)
                {
                    DataSet ds = new DAL.CommonDA().GetApplicantinfo_vhpp(-1, -1, @UserSession.LoggedInUserName);
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            VH.applicant_name = ds.Tables[0].Rows[0]["applicant_name"].ToString().Trim();
                            VH.steps = ds.Tables[0].Rows[0]["steps"].ToString().Trim();
                            VH.inputdob = ds.Tables[0].Rows[0]["dob"].ToString().Trim();
                            VH.applicant_code = long.Parse(ds.Tables[0].Rows[0]["registration_code"].ToString().Trim());
                            VH.yojana_code = short.Parse(ds.Tables[0].Rows[0]["scheme_code"].ToString().Trim());
                            VH.adhar_no = ds.Tables[0].Rows[0]["adhar_no"].ToString().Trim();
                            VH.Husband_father_name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            VH.current_address = ds.Tables[0].Rows[0]["current_address"].ToString().Trim();
                            VH.permanent_address = ds.Tables[0].Rows[0]["permanent_address"].ToString().Trim();
                            VH.mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString().Trim();
                            VH.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                            VH.DistCode = int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
                            VH.TehsilCode = int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
                            VH.BlockCode = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()); ;
                            VH.VillCode = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
                            VH.handicraft_id_no = ds.Tables[0].Rows[0]["id_no"].ToString().Trim();
                            VH.input_architect_experience = ds.Tables[0].Rows[0]["architect_experience"].ToString().Trim() == "01/01/1900" ? "" : ds.Tables[0].Rows[0]["architect_experience"].ToString().Trim();
                            VH.input_Artwork_start_date = ds.Tables[0].Rows[0]["artwork_start_date"].ToString().Trim() == "01/01/1900" ? "" : ds.Tables[0].Rows[0]["artwork_start_date"].ToString().Trim();
                            VH.input_Artwork_to_date = ds.Tables[0].Rows[0]["artwork_to_date"].ToString().Trim() == "01/01/1900" ? "" : ds.Tables[0].Rows[0]["artwork_to_date"].ToString().Trim();
                            VH.teacher = ds.Tables[0].Rows[0]["teacher"].ToString().Trim();
                            VH.participation_count = int.Parse(ds.Tables[0].Rows[0]["participation_count"].ToString().Trim());
                            VH.Award_count = int.Parse(ds.Tables[0].Rows[0]["award_count"].ToString().Trim());
                            VH.craft_name = ds.Tables[0].Rows[0]["craft_name"].ToString().Trim();
                            VH.Artwork_subject = ds.Tables[0].Rows[0]["artwork_subject"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception EX)
            {

                throw;
            }
            return View(VH);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult VHPPAffidavitPrint()
        {
            return View();
        }
        public ActionResult GetCMSY_ApplicationPrint()
        {
            return View();
        }
        public ActionResult Print_VHPPForm()
        {
            return View();
        }
        public ActionResult PlotApplicantWizard()
        {
            return View();
        }
        public ActionResult PlantAndMachinery_EntryForm()
        {
            Plant_Machinery PM = new Plant_Machinery();
            if (@UserSession.LoggedInUserName != null)
            {
                DataSet ds = new DAL.CommonDA().GetApplicantinfo_MYSY(-1, -1, @UserSession.LoggedInUserName);
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        if (int.Parse(ds.Tables[0].Rows[0]["steps"].ToString().Trim()) <=3)
                        {
                            string mesg = Message("MYSY", ds.Tables[0].Rows[0]["steps"].ToString().Trim());
                            PM.steps = "-1";
                            PM.message  = mesg;
                            return View(PM);
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            PM.fixed_deposite = decimal.Parse(ds.Tables[0].Rows[0]["machinery_cost"].ToString().Trim());
                            PM.working_capital = decimal.Parse(ds.Tables[0].Rows[0]["working_capital"].ToString().Trim());
                            PM.project_cost = decimal.Parse(ds.Tables[0].Rows[0]["project_cost"].ToString().Trim());
                            PM.address = ds.Tables[0].Rows[0]["permanent_address"].ToString().Trim();
                            PM.adhar_no = ds.Tables[0].Rows[0]["adhar_no"].ToString().Trim();
                            PM.applicant_name = ds.Tables[0].Rows[0]["applicant_name"].ToString().Trim();
                            PM.father_name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            PM.mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString().Trim();
                            PM.office_address = ds.Tables[0].Rows[0]["proposed_office_address"].ToString().Trim();
                            PM.manufacturing = ds.Tables[0].Rows[0]["Industry_apply"].ToString().Trim();
                            PM.registration_code = ds.Tables[0].Rows[0]["registration_code"].ToString().Trim();
                           
                        }
                        if (ds.Tables[4] != null)
                        {
                            if (ds.Tables[4].Rows.Count > 0)
                            {
                                StringBuilder str = new StringBuilder();
                                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                {
                                    str.Append("<tr>");
                                    str.Append("<td>" + (i + 1) + "</td>");
                                    str.Append("<td><input type='text' id='txtMachine" + (i + 1) + "' placeholder='Title' name='txtMachine' value='" + ds.Tables[4].Rows[i]["machine_name"].ToString().Trim() + "' maxlength='100'></td>");

                                    str.Append("<td><input type='text' id='txtsupplier" + (i + 1) + "' placeholder='Title' name='txtsupplier' value='" + ds.Tables[4].Rows[i]["supplier"].ToString().Trim() + "' maxlength='100'></td>");

                                    str.Append("<td><input type='text' id='txtcost" + (i + 1) + "' placeholder='Title' name='txtcost' value='" + ds.Tables[4].Rows[i]["price"].ToString().Trim() + "' maxlength='100' onkeyup='sumcost()'></td>");

                                    str.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='del(this)'></td>");
                                    str.Append("</tr>");
                                }
                                PM.machinetable = str.ToString().Trim();
                                PM.steps = "1";
                                
                                PM.Marketingsystem = ds.Tables[4].Rows[0]["marketing"].ToString().Trim();
                            }
                            else
                            {
                                PM.steps = "0";
                                PM.message = "";
                            }
                        }
                        if (ds.Tables[5] != null)
                        {
                            if (ds.Tables[5].Rows.Count > 0)
                            {
                                PM.self_share = decimal.Parse(ds.Tables[5].Rows[0]["self_share"].ToString());
                                PM.bank_loan = decimal.Parse(ds.Tables[5].Rows[0]["bank_loan"].ToString());
                                PM.margin_money = decimal.Parse(ds.Tables[5].Rows[0]["margin_money"].ToString());
                                if (int.Parse(ds.Tables[5].Rows[0]["steps"].ToString().Trim())>=6)
                                {
                                    PM.steps = "2";
                                    PM.message = "";
                                    
                                }
                                
                            }
                        }

                    }
                }


            }


            return View(PM);
        }
        public JsonResult Getifsc(string Prefix, int District,int bank_code)
        {
            DataTable dt = new DAL.CommonDA().Getifsc(Prefix, District, bank_code);

            List<ifsc> SL = new List<ifsc>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ifsc s1 = new ifsc();
                s1.ifsc_code = dt.Rows[i]["ifsc"].ToString().Trim();
                s1.bank_code = dt.Rows[i]["bank_code"].ToString().Trim();
                s1.branch_code = dt.Rows[i]["branch_code"].ToString().Trim();
                s1.branch_name = dt.Rows[i]["branch_name"].ToString().Trim();
                s1.address = dt.Rows[i]["address"].ToString().Trim();
                SL.Add(s1);
            }

            return Json(SL, JsonRequestBehavior.AllowGet);
        }
        #region MYSY_scheme
        public ActionResult MYSY_SchemeForm()
        {
            CMYSS_Applicant CM = new CMYSS_Applicant();
            try
            {
                if (@UserSession.LoggedInUserName != null)
                {
                    DataSet ds = new DAL.CommonDA().GetApplicantinfo_MYSY(-1, -1, @UserSession.LoggedInUserName);
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            CM.Is_village = ds.Tables[0].Rows[0]["Is_village"].ToString().Trim();
                            CM.applicant_name = ds.Tables[0].Rows[0]["applicant_name"].ToString().Trim();
                            CM.inputdob = ds.Tables[0].Rows[0]["dob"].ToString().Trim();
                            CM.applicant_code = long.Parse(ds.Tables[0].Rows[0]["registration_code"].ToString().Trim());
                            CM.yojana_code = short.Parse(ds.Tables[0].Rows[0]["scheme_code"].ToString().Trim());
                            CM.adhar_no = ds.Tables[0].Rows[0]["adhar_no"].ToString().Trim();
                            CM.Husband_father_name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            CM.current_address = ds.Tables[0].Rows[0]["current_address"].ToString().Trim();
                            CM.proposed_office_address = ds.Tables[0].Rows[0]["proposed_office_address"].ToString().Trim();
                            CM.permanent_address = ds.Tables[0].Rows[0]["permanent_address"].ToString().Trim();
                            CM.mobile_no = ds.Tables[0].Rows[0]["mobile_no"].ToString().Trim();
                            CM.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                            CM.pansion_card_No = ds.Tables[0].Rows[0]["pansion_card_No"].ToString().Trim();
                            CM.family_income = decimal.Parse(ds.Tables[0].Rows[0]["family_income"].ToString().Trim());
                            CM.project_name = ds.Tables[0].Rows[0]["project_name"].ToString().Trim();
                            CM.project_cost = decimal.Parse(ds.Tables[0].Rows[0]["project_cost"].ToString().Trim());
                            CM.machinery_cost = decimal.Parse(ds.Tables[0].Rows[0]["machinery_cost"].ToString().Trim());
                            CM.working_capital = decimal.Parse(ds.Tables[0].Rows[0]["working_capital"].ToString().Trim());
                            CM.self_share = decimal.Parse(ds.Tables[0].Rows[0]["self_share"].ToString().Trim());
                            CM.deposit_amt = decimal.Parse(ds.Tables[0].Rows[0]["deposit_amt"].ToString().Trim());
                            CM.steps = ds.Tables[0].Rows[0]["steps"].ToString().Trim();
                            CM.bank_account_no = ds.Tables[0].Rows[0]["bank_account_no"].ToString().Trim();
                            CM.DistCode = int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
                            CM.TehsilCode = int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
                            CM.BlockCode = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()); ;
                            CM.VillCode = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
                            CM.current_district_code_census = int.Parse(ds.Tables[0].Rows[0]["current_district_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["current_district_code_census"].ToString().Trim());
                            CM.current_tehsil_code_census = int.Parse(ds.Tables[0].Rows[0]["current_tehsil_code_census"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["current_tehsil_code_census"].ToString().Trim());
                            CM.current_block_code = int.Parse(ds.Tables[0].Rows[0]["current_block_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["current_block_code"].ToString().Trim()); ;
                            CM.current_village_code = int.Parse(ds.Tables[0].Rows[0]["current_village_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["current_village_code"].ToString().Trim());
                            CM.proposed_office_district = int.Parse(ds.Tables[0].Rows[0]["proposed_office_district"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["proposed_office_district"].ToString().Trim());
                            CM.proposed_office_tehsil = int.Parse(ds.Tables[0].Rows[0]["proposed_office_tehsil"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["proposed_office_tehsil"].ToString().Trim());
                            CM.proposed_office_block = int.Parse(ds.Tables[0].Rows[0]["proposed_office_block"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["proposed_office_block"].ToString().Trim()); ;
                            CM.proposed_office_village = int.Parse(ds.Tables[0].Rows[0]["proposed_office_village"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["proposed_office_village"].ToString().Trim());
                            CM.qualification_code = int.Parse(ds.Tables[0].Rows[0]["qualification_code"].ToString().Trim()) == 0 ? -1 : int.Parse(ds.Tables[0].Rows[0]["qualification_code"].ToString().Trim());
                            CM.product_service_code = int.Parse(ds.Tables[0].Rows[0]["product_service_code"].ToString().Trim());
                            CM.category_suffix = (ds.Tables[0].Rows[0]["category_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["category_suffix"].ToString().Trim());
                            CM.industry_activity = (ds.Tables[0].Rows[0]["industry_activity_suffix"].ToString().Trim());
                            CM.sp_category_suffix = (ds.Tables[0].Rows[0]["sp_category_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["sp_category_suffix"].ToString().Trim());
                            CM.gender_suffix = (ds.Tables[0].Rows[0]["gender_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["gender_suffix"].ToString().Trim());
                            CM.pancard = (ds.Tables[0].Rows[0]["pancard"].ToString().Trim());
                            CM.is_address_same = (ds.Tables[0].Rows[0]["is_address_same"].ToString().Trim());
                            CM.Is_edp_training = (ds.Tables[0].Rows[0]["Is_edp_training"].ToString().Trim());
                            CM.edp_training_ins = (ds.Tables[0].Rows[0]["edp_training_ins"].ToString().Trim());
                            CM.sponsoring_office_code = int.Parse(ds.Tables[0].Rows[0]["sponsoring_office_code"].ToString().Trim());
                            CM.dist_name = (ds.Tables[0].Rows[0]["dist_name"].ToString().Trim());
                            StringBuilder str = new StringBuilder();
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                DataTable dtFather = ds.Tables[1].Select("relation_code='F'").CopyToDataTable();
                                DataTable dtMother = ds.Tables[1].Select("relation_code='M'").CopyToDataTable();
                                DataTable dtOther = new DataTable();
                                if (ds.Tables[1].Select("relation_code<>'M' AND relation_code<>'F'") != null && ds.Tables[1].Select("relation_code<>'M' AND relation_code<>'F'").Length > 0)
                                {
                                    dtOther = ds.Tables[1].Select("relation_code<>'M' AND relation_code<>'F'").CopyToDataTable();
                                }

                                if (dtFather != null)
                                {
                                    if (dtFather.Rows.Count > 0)
                                    {
                                        CM.Father_Name = dtFather.Rows[0]["person_name"].ToString().Trim();
                                        CM.Father_Age = int.Parse(dtFather.Rows[0]["age"].ToString().Trim());
                                        CM.Father_work = dtFather.Rows[0]["workingfield"].ToString().Trim();
                                        CM.chkfamily = true;
                                    }

                                }
                                if (dtMother != null)
                                {
                                    if (dtMother.Rows.Count > 0)
                                    {
                                        CM.Mother_Name = dtMother.Rows[0]["person_name"].ToString().Trim();
                                        CM.Mother_Age = int.Parse(dtMother.Rows[0]["age"].ToString().Trim());
                                        CM.Mother_work = dtMother.Rows[0]["workingfield"].ToString().Trim();
                                    }
                                }

                                if (dtOther != null)
                                {
                                    if (dtOther.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtOther.Rows.Count; i++)
                                        {
                                            str.Append("<tr>");
                                            str.Append("<td>" + (i + 1) + "</td>");
                                            str.Append("<td><input type='hidden' id='hfRelation_Code" + (i + 1) + "' name='hfRelation_Code' value='" + dtOther.Rows[i]["relation_code"].ToString().Trim() + "'><label>" + dtOther.Rows[i]["relation"].ToString().Trim() + "</label></td>");
                                            str.Append("<td><input type='text' id='txtName" + (i + 1) + "' placeholder='Title' name='txtName' value='" + dtOther.Rows[i]["person_name"].ToString().Trim() + "' maxlength='50'></td>");
                                            str.Append("<td><input type='text' id='txtAge" + (i + 1) + "' placeholder='Title' name='txtAge' value='" + dtOther.Rows[i]["age"].ToString().Trim() + "' maxlength='2'></td>");
                                            str.Append("<td><input type='text' id='txtWork" + (i + 1) + "' placeholder='Title' name='txtWork' value='" + dtOther.Rows[i]["workingfield"].ToString().Trim() + "' maxlength='50'></td>");
                                            str.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='del(this)'></td>");
                                            str.Append("</tr>");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                CM.Father_Name = ds.Tables[0].Rows[0]["father_name"].ToString().Trim();
                            }
                            CM.OtherRelation = str.ToString().Trim();
                            CM.other_product_service = ds.Tables[0].Rows[0]["other_product_service"].ToString().Trim();
                            CM.permanent_add_pincode = ds.Tables[0].Rows[0]["permanent_add_pincode"].ToString().Trim();
                            CM.curent_add_pincode = ds.Tables[0].Rows[0]["curent_add_pincode"].ToString().Trim();
                            CM.@proposed_office_pincode = ds.Tables[0].Rows[0]["proposed_office_pincode"].ToString().Trim();
                            CM.edp_training_ins_code = int.Parse(ds.Tables[0].Rows[0]["edp_training_ins_code"].ToString().Trim());
                            CM.branch_Address = ds.Tables[0].Rows[0]["branch_Address"].ToString().Trim();
                            CM.bank_code = short.Parse(ds.Tables[0].Rows[0]["bank_code"].ToString().Trim());
                            CM.branch_code = int.Parse(ds.Tables[0].Rows[0]["branch_code"].ToString().Trim());
                            CM.ifsc = ds.Tables[0].Rows[0]["ifsc_code"].ToString().Trim();
                            // CM.bank_account_no= ds.Tables[0].Rows[0]["bank_account_no"].ToString().Trim();
                            // CM.deposit_amt = decimal.Parse(ds.Tables[0].Rows[0]["bank_account_no"].ToString().Trim());
                            // CM.self_share = decimal.Parse(ds.Tables[0].Rows[0]["self_share"].ToString().Trim());
                        }
                    }
                }
                return View(CM);
            }
            catch (Exception ex)
            {
                return View(CM);
            }
            finally
            {
            }
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Doc> objdoc, List<CMYSS_Applicant_Family> objFamily, string @sptype, string steps)
        {
            try
            {
                try
                {
                    if (steps == "1")
                    {
                        DateTime dt = BLL.CommonBL.Setdate(Objform.inputdob);
                        Objform.dob = dt;
                    }
                    
                }
                catch (Exception)
                {

                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                Objform.@Password = "";
                Objform.steps = steps;
                string str = new DAL.CommonDA().UpdateCMYSS_Applicant(Objform, "2");
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateVHPP_Applicant(vhpp_Applicant Objform, string @sptype, List<vhpp_artwork_details> Lvhpp)
        {
            try
            {
                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(Objform.inputdob);
                    Objform.dob = dt;
                }
                catch (Exception)
                {
                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                try
                {
                    DateTime dt1 = BLL.CommonBL.Setdate(Objform.input_architect_experience);
                    DateTime dt2 = BLL.CommonBL.Setdate(Objform.input_Artwork_start_date);
                    DateTime dt3 = BLL.CommonBL.Setdate(Objform.input_Artwork_to_date);
                    Objform.architect_experience = dt1;
                    Objform.Artwork_start_date = dt2;
                    Objform.Artwork_to_date = dt3;
                }
                catch (Exception)
                {
                    return Json("Date must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                Objform.@Password = "";
                DataTable dtab = new DataTable();
                if (Lvhpp != null)
                {
                    if (Lvhpp.Count > 0)
                    {

                        dtab.Columns.Add("artwork_place_code", typeof(int));
                        dtab.Columns.Add("registration_code", typeof(int));
                        dtab.Columns.Add("artwork_place_name", typeof(string));
                        dtab.Columns.Add("Description", typeof(string));
                        dtab.Columns.Add("user_id", typeof(string));
                        dtab.Columns.Add("user_ip", typeof(string));
                        dtab.Columns.Add("time_stamp", typeof(DateTime));
                        dtab.Columns.Add("user_mac", typeof(string));
                        dtab.Columns.Add("art_work_name", typeof(string));
                        for (int i = 0; i < Lvhpp.Count; i++)
                        {
                            dtab.Rows.Add();
                            dtab.Rows[dtab.Rows.Count - 1]["artwork_place_code"] = Lvhpp[i].artwork_place_code;
                            dtab.Rows[dtab.Rows.Count - 1]["registration_code"] = Lvhpp[i].registration_code;
                            dtab.Rows[dtab.Rows.Count - 1]["artwork_place_name"] = Lvhpp[i].artwork_place_name;
                            dtab.Rows[dtab.Rows.Count - 1]["Description"] = Lvhpp[i].Description;
                            dtab.Rows[dtab.Rows.Count - 1]["user_id"] = @UserSession.LoggedInUser.UserName;
                            dtab.Rows[dtab.Rows.Count - 1]["user_ip"] = "";
                            dtab.Rows[dtab.Rows.Count - 1]["time_stamp"] = DateTime.Now;
                            dtab.Rows[dtab.Rows.Count - 1]["user_mac"] = "";
                            dtab.Rows[dtab.Rows.Count - 1]["art_work_name"] = Lvhpp[i].art_work_name;
                        }
                    }
                }


                string str = new DAL.CommonDA().InsertUpdateVHPY(Objform, "2", dtab);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCMYSS_Applicantfamily(List<CMYSS_Applicant_Family> objFamily, string PensionCard, decimal family_income)
        {
            try
            {
                DataTable dt = new DataTable();
                if (objFamily != null)
                {
                    if (objFamily.Count > 0)
                    {
                        dt.Columns.Add("registration_code", typeof(long));
                        dt.Columns.Add("relation_code", typeof(string));
                        dt.Columns.Add("person_name", typeof(string));
                        dt.Columns.Add("age", typeof(int));
                        dt.Columns.Add("workingfield", typeof(string));
                        for (int i = 0; i < objFamily.Count; i++)
                        {
                            dt.Rows.Add();
                            dt.Rows[dt.Rows.Count - 1]["registration_code"] = objFamily[i].applicant_code;
                            dt.Rows[dt.Rows.Count - 1]["relation_code"] = objFamily[i].relation_code;
                            dt.Rows[dt.Rows.Count - 1]["person_name"] = objFamily[i].person_name;
                            dt.Rows[dt.Rows.Count - 1]["age"] = objFamily[i].age;
                            dt.Rows[dt.Rows.Count - 1]["workingfield"] = objFamily[i].workingfield==null?"": objFamily[i].workingfield;
                        }
                    }
                }
                string str = new DAL.CommonDA().InsertUpdateCMYSS_Applicantfamily(dt, PensionCard, family_income);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCMYSS_MachininaryDetails(List<Plant_Machinery> objFamily)
        {
            try
            {
                if (objFamily.Count <= 0)
                {
                    return Json("कृप्या मशीन का विवरण भरे ", JsonRequestBehavior.AllowGet);

                }
                DataTable dt = new DataTable();
                if (objFamily != null)
                {
                    if (objFamily.Count > 0)
                    {
                        dt.Columns.Add("registration_code", typeof(long));
                        dt.Columns.Add("machine_name", typeof(string));
                        dt.Columns.Add("price", typeof(decimal));
                        dt.Columns.Add("supplier", typeof(string));
                        dt.Columns.Add("fixed_deposite", typeof(decimal));
                        dt.Columns.Add("working_capital", typeof(decimal));
                        dt.Columns.Add("project_cost", typeof(decimal));
                        dt.Columns.Add("marketing", typeof(string));
                        for (int i = 0; i < objFamily.Count; i++)
                        {
                            dt.Rows.Add();
                            dt.Rows[dt.Rows.Count - 1]["registration_code"] = objFamily[i].registration_code;
                            dt.Rows[dt.Rows.Count - 1]["machine_name"] = objFamily[i].machine_name;
                            dt.Rows[dt.Rows.Count - 1]["price"] = objFamily[i].price;
                            dt.Rows[dt.Rows.Count - 1]["supplier"] = objFamily[i].supplier;
                            dt.Rows[dt.Rows.Count - 1]["fixed_deposite"] = objFamily[i].fixed_deposite;
                            dt.Rows[dt.Rows.Count - 1]["working_capital"] = objFamily[i].working_capital;
                            dt.Rows[dt.Rows.Count - 1]["project_cost"] = objFamily[i].project_cost;
                            dt.Rows[dt.Rows.Count - 1]["marketing"] = objFamily[i].Marketingsystem;
                        }
                    }
                }
                string str = new DAL.CommonDA().InsertUpdateCMYSS_MachininaryDetails(dt);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateMYSYFinancedetails(decimal @total_Production,decimal @approx_sale, decimal @approx_profit,long registration_code)
        {
            try
            {
                //if (objFamily.Count <= 0)
                //{
                //    return Json("कृप्या पूंजी का विवरण भरे ", JsonRequestBehavior.AllowGet);

                //}
                //DataTable dt = new DataTable();
                //if (objFamily != null)
                //{
                //    if (objFamily.Count > 0)
                //    {
                //        dt.Columns.Add("registration_code", typeof(long));
                //        dt.Columns.Add("self_share", typeof(decimal));
                //        dt.Columns.Add("bank_loan", typeof(decimal));
                //        dt.Columns.Add("margin_money", typeof(decimal));
                //        dt.Columns.Add("total_Production", typeof(decimal));
                //        dt.Columns.Add("approx_sale", typeof(decimal));
                //        dt.Columns.Add("profit", typeof(decimal));

                //        for (int i = 0; i < objFamily.Count; i++)
                //        {
                //            dt.Rows.Add();
                //            dt.Rows[dt.Rows.Count - 1]["registration_code"] = objFamily[i].registration_code;
                //            dt.Rows[dt.Rows.Count - 1]["self_share"] = objFamily[i].self_share;
                //            dt.Rows[dt.Rows.Count - 1]["bank_loan"] = objFamily[i].bank_loan;
                //            dt.Rows[dt.Rows.Count - 1]["margin_money"] = objFamily[i].margin_money;
                //            dt.Rows[dt.Rows.Count - 1]["total_Production"] = objFamily[i].total_Production;
                //            dt.Rows[dt.Rows.Count - 1]["approx_sale"] = objFamily[i].approx_sale;
                //            dt.Rows[dt.Rows.Count - 1]["profit"] = objFamily[i].profit;

                //        }
                //    }
                //}
                string str = new DAL.CommonDA().InsertRegistrationFinancedetails(total_Production,approx_sale,approx_profit,registration_code);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Uploadfile()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string fname = "";
                    string fname2 = "";
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        // string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        //string filename = Path.GetFileName(Request.Files[i].FileName);
                        HttpPostedFileBase file = files[i];
                        // byte[] doc = new byte[file.ContentLength];
                        Byte[] img = null;
                        if (file != null && file.ContentLength > 0)
                        {   /*****IMG-DB-CODE******/
                            int FileSize = file.ContentLength;
                            img = new Byte[FileSize];
                            file.InputStream.Read(img, 0, FileSize);
                            //objUserData.UserImage = img;
                        }//
                        // objUserData.UserId = UserSession.LoggedInUserId.ToString();
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fname2 = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            fname2 = file.FileName;
                        }
                        string[] ext = fname.Split('.');

                        if (ext[1].ToString().Trim() == "pdf" || ext[1].ToString().Trim() == "docx" || ext[1].ToString().Trim() == "doc" || ext[1].ToString().Trim() == "xls" || ext[1].ToString().Trim() == "jpg" || ext[1].ToString().Trim() == "jpeg" || ext[1].ToString().Trim() == "png" || ext[1].ToString().Trim() == "xlsx")
                        {
                            DataTable dt = new DataTable();
                            if (Session["Doc"] == null)
                            {
                                dt.Rows.Add();
                                dt.Columns.Add("applicant_code", typeof(long));
                                dt.Columns.Add("doc", typeof(System.Byte[]));
                                dt.Columns.Add("doc_type", typeof(string));
                                dt.Columns.Add("doc_content_type", typeof(string));
                            }
                            else
                            {
                                dt = (DataTable)Session["Doc"];
                                dt.Rows.Add();
                            }
                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            Session["Doc"] = dt;
                        }
                        else
                        {
                            return Json("only .pdf/.docx /.doc/.xls/.xlsx/.jpg/jpeg/.png  files upload");
                        }
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
                finally
                {
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult Uploadphoto()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string fname = "";
                    string fname2 = "";
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        Byte[] img = null;
                        if (file != null && file.ContentLength > 0)
                        {   /*****IMG-DB-CODE******/
                            int FileSize = file.ContentLength;
                            img = new Byte[FileSize];
                            file.InputStream.Read(img, 0, FileSize);

                        }//
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fname2 = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            fname2 = file.FileName;
                        }
                        string[] ext = fname.Split('.');

                        if (ext[1].ToString().Trim() == "jpg" || ext[1].ToString().Trim() == "jpeg" || ext[1].ToString().Trim() == "png")
                        {
                            Bitmap img2 = new Bitmap(file.InputStream);
                            if (img2.Width != 110 || img2.Height != 140)
                            {
                                return Json("Resolution must be 110*140");
                            }
                            DataTable dt = new DataTable();
                            if (Session["Doc"] == null)
                            {
                                dt.Rows.Add();
                                dt.Columns.Add("applicant_code", typeof(long));
                                dt.Columns.Add("doc", typeof(System.Byte[]));
                                dt.Columns.Add("doc_type", typeof(string));
                                dt.Columns.Add("doc_content_type", typeof(string));
                            }
                            else
                            {
                                dt = RemovedPrevious((DataTable)Session["Doc"], "I");
                                dt.Rows.Add();

                            }
                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            Session["Doc"] = dt;
                        }
                        else
                        {
                            return Json("only .pdf/.docx /.doc/.xls/.xlsx/.jpg/jpeg/.png  files upload");
                        }
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
                finally
                {
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public DataTable RemovedPrevious(DataTable dt, string doc_type)
        {
            DataTable dt2 = new DataTable();
            if (dt != null)
            {
                if (dt.Select("doc_type='" + doc_type + "'") != null && dt.Select("doc_type='" + doc_type + "'").Length > 0)
                {
                    dt2 = dt.Select("doc_type='" + doc_type + "'").CopyToDataTable();
                }
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dt.Rows)
                        {
                            foreach (DataRow row2 in dt2.Rows)
                            {
                                if (row1["doc_type"].ToString() == row2["doc_type"].ToString())
                                {
                                    rows_to_remove.Add(row1);
                                }
                            }
                        }

                        foreach (DataRow row in rows_to_remove)
                        {
                            dt.Rows.Remove(row);
                            dt.AcceptChanges();
                        }
                    }

                }

            }
            return dt;
        }
        public ActionResult Uploadsign()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string fname = "";
                    string fname2 = "";
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        Byte[] img = null;
                        if (file != null && file.ContentLength > 0)
                        {   /*****IMG-DB-CODE******/
                            int FileSize = file.ContentLength;
                            img = new Byte[FileSize];
                            file.InputStream.Read(img, 0, FileSize);

                        }//
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fname2 = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            fname2 = file.FileName;
                        }
                        string[] ext = fname.Split('.');

                        if (ext[1].ToString().Trim() == "jpg" || ext[1].ToString().Trim() == "jpeg" || ext[1].ToString().Trim() == "png")
                        {
                            Bitmap img2 = new Bitmap(file.InputStream);
                            if (img2.Width != 140 || img2.Height != 110)
                            {
                                return Json("Resolution must be 110*140");
                            }
                            DataTable dt = new DataTable();
                            if (Session["Doc"] == null)
                            {
                                dt.Rows.Add();
                                dt.Columns.Add("applicant_code", typeof(long));
                                dt.Columns.Add("doc", typeof(System.Byte[]));
                                dt.Columns.Add("doc_type", typeof(string));
                                dt.Columns.Add("doc_content_type", typeof(string));
                            }
                            else
                            {
                                dt = RemovedPrevious((DataTable)Session["Doc"], "S");
                                dt.Rows.Add();
                            }
                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            Session["Doc"] = dt;
                        }
                        else
                        {
                            return Json("only .pdf/.docx /.doc/.xls/.xlsx/.jpg/jpeg/.png  files upload");
                        }
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
                finally
                {
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult Uploadfiledone(string doc_type, string applicant_code)
        {
            try
            {
                DataTable dt = RemovedPrevious((DataTable)Session["Doc"], doc_type);
                if (dt != null)
                {
                    dt.Rows[dt.Rows.Count - 1]["doc_type"] = doc_type;
                    dt.Rows[dt.Rows.Count - 1]["applicant_code"] = applicant_code;
                    Session["Doc"] = dt;
                    return Json("File upload", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("File not upload", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }
        }
        public JsonResult InsertUpdatedoc2()
        {
            try
            {
                if (Session["Doc"] != null)
                {
                    DataTable dt = (DataTable)Session["Doc"];
                    if (dt != null)
                    {
                        //if (dt.Rows.Count >= 4)
                        //{
                        string msg = new DAL.CommonDA().InsertUpdateCMYSS_Applicantdocument(dt);
                        if (msg.Contains("Save"))
                        {
                            Session["Doc"] = null;
                        }
                        return Json(msg, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json("Please upload all documents", JsonRequestBehavior.AllowGet);
                        //}
                    }
                    else
                    {
                        Session["Doc"] = null;
                        return Json("Session Expire Please refresh page", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Session["Doc"] = null;
                    return Json("Session Expire Please refresh page", JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }

        }
        public ActionResult GetDistricteng(string district_code_census, string state_code)
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "DSE", district_code_census.Trim(), state_code.Trim());
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getplot(int EstateCode)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                DataTable dt = new DAL.CommonDA().GetPlotInfo(EstateCode, -1, "", "", BLL.CommonBL.Setdate("01/01/1990"), DateTime.Now, "", "");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            str.Append("<tr>");
                            str.Append("<td>" + (i + 1) + "</td>");
                            str.Append("<td>" + dt.Rows[i]["PlotSerial"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["Plot_Area"].ToString().Trim() + "</td>");
                            str.Append("<td><input id='Checkbox" + (i + 1) + "' type='checkbox' onclick='funplot(&#39;" + dt.Rows[i]["PlotSerial"].ToString().Trim() + "&#39;,&#39;Checkbox" + (i + 1) + "&#39;)' /></td>");
                            str.Append("</tr>");
                        }
                    }
                }
                return Json(str.ToString().Trim(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }
        }
        public JsonResult ApplicantImage()
        {
            try
            {
                string str = "";
                DataSet ds = new DataSet();
                ds = new DAL.CommonDA().GetApplicantinfo_MYSY(-1, -1, UserSession.LoggedInUserName);

                if (ds != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        str = ds.Tables[2].Rows[0]["image1"].ToString().Trim();
                    }
                }
                return Json(str.ToString().Trim(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }
        }
        public ActionResult certificate_Letter()
        {
            return View();
        }

        public ActionResult Application_Status()
        {
            return View();
        }
        public ActionResult Getscheme()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "y", "", "");
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Uploadform()
        {
            uploadform uf = new Models.uploadform();
            DataSet dt = new DAL.CommonDA().GetScheme_Doc(@UserSession.LoggedInUserName, UserSession.yojanacode);
            StringBuilder str = new StringBuilder();
            str.Append("<div class='portlet light ' id='form_wizard_1'>");
            if (dt != null)
            {
                
                if (dt.Tables[0] != null)
                {
                    
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        if (dt.Tables[2] != null)
                        {
                            if (dt.Tables[2].Rows.Count > 0)
                            {
                                
                                if (int.Parse(dt.Tables[2].Rows[0]["steps"].ToString().Trim())<=5)
                                {
                                   string mesg= Message(dt.Tables[0].Rows[0]["scheme_initial"].ToString().Trim(), dt.Tables[2].Rows[0]["steps"].ToString().Trim());
                                    uf.page = mesg.Trim();
                                    return View(uf);
                                }
                                
                            }
                        }
                        if (dt.Tables[0].Rows[0]["Status"].ToString().Trim() == "0")
                        {
                            str.Append("<div class='portlet-title'>");
                            str.Append(" <div class='caption'>");
                            str.Append("<i class='icon-layers font-red'></i>");
                            str.Append("<span class='caption-subject font-red bold uppercase'>दस्तावेज़ अपलोड करें</span>");
                            str.Append("</div>");
                            str.Append("</div>");

                            str.Append("<div class='portlet-body form'>");
                            str.Append("<form class='form-horizontal' action='#' id='submit_form' method='POST' novalidate='novalidate'>");
                            str.Append("<div class='form-wizard'>");
                            str.Append("<div class='form-body'>");
                            str.Append("<div class='row'>");
                            if (dt.Tables[0].Select("doc_code='I'") != null && dt.Tables[0].Select("doc_code='I'").Length > 0)
                            {
                                str.Append("<div class='col-md-6 col-sm-6 col-xs-12 text-center col-50'>");
                                str.Append("<div class='myborder' style='border:1px solid #ccc;'>");
                                str.Append("<div class='panel-heading'>फोटो </div>");
                                str.Append("<div style='height:200px;padding:5px;'>");
                                str.Append("<img id='output' src='../no_image.png' alt='image' class='images-01'>");
                                str.Append("</div>");
                                str.Append("<span class='font-red'>Size:</span> 20kb");
                                str.Append("<span class='font-red'>Resolution:</span> 110 X 140");
                                str.Append("<div style='border:1px solid #ccc;padding:5px;'>");
                                str.Append("<input type='file' id='FileUpload1' class='form-control' data-min-width='110' data-min-height='140' data-max-width='110' data-max-height='140'   onchange='loadPayFile(event,&#39;FileUpload1&#39;,&#39;I&#39;,&#39;" + dt.Tables[0].Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;output&#39;)' /><input type='hidden' id='hfPpath' /><div class='clearfix'></div>");
                                str.Append("</div>");
                                str.Append("</div>");
                                str.Append("</div>");
                            }
                            if (dt.Tables[0].Select("doc_code='S'") != null && dt.Tables[0].Select("doc_code='S'").Length > 0)
                            {
                                str.Append("<div class='col-md-6 col-sm-6 col-xs-12 text-center col-50'>");
                                str.Append("<div class='myborder' style='border:1px solid #ccc;'>");
                                str.Append("<div class='panel-heading'>हस्ताक्षर</div>");
                                str.Append("<div style='height:200px;padding:5px;'>");
                                str.Append("<img id='Img1' src='../no_image.png' alt='image' class='images-01' width='200px' height='150px'></div>");
                                str.Append("<span class='font-red'>Size:</span> 20kb");
                                str.Append("<span class='font-red'>Resolution:</span> 140 X 110");
                                str.Append("<div style='border:1px solid #ccc;padding:5px;'>");
                                str.Append("<input type='file' id='FileUpload2' class='form-control' onchange='loadPayFile(event,&#39;FileUpload2&#39;,&#39;S&#39;,  &#39;" + dt.Tables[0].Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;Img1&#39;)' />");
                                str.Append("<div class='clearfix'></div>");
                                str.Append("</div>");
                                str.Append("</div>");
                                str.Append("</div>");
                            }
                            str.Append("<div class='clearfix'></div></div>");
                            str.Append("<div class='portlet box '>");
                            str.Append("<div class='portlet-title'>");
                            str.Append("</div>");
                            str.Append("<div class='portlet-body flip-scroll'>");
                            str.Append("<table class='table table-bordered table-striped table-condensed flip-content'>");
                            str.Append("<thead class='flip-content'>");
                            str.Append("<tr>");
                            str.Append("<th width='5%'> Sr.No </th>");
                            str.Append("<th> Document </th>");
                            str.Append("<th> Action </th>");
                            str.Append("</tr>");
                            str.Append("</thead>");
                            str.Append("<tbody>");
                            int k = 0;
                            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                            {
                                if (dt.Tables[0].Rows[i]["doc_code"].ToString().Trim() != "I" && dt.Tables[0].Rows[i]["doc_code"].ToString().Trim() != "S")
                                {
                                    k = k + 1;
                                    str.Append("<tr>");
                                    str.Append("<td>" + k + "</td>");
                                    str.Append("<td>" + dt.Tables[0].Rows[i]["u_description"].ToString() + "</td>");
                                    str.Append("<td><input type='file' id='FileUpload" + (i + 3) + "' class='form - control' onchange='loadPayFile(event,  &#39;FileUpload" + (i + 3) + "&#39;, &#39;" + dt.Tables[0].Rows[i]["doc_code"].ToString().Trim() + "&#39;,&#39;" + dt.Tables[0].Rows[i]["registration_code"].ToString().Trim() + "&#39; , &#39;-1&#39;)'/></td>");
                                    str.Append("</tr>");
                                }
                            }
                            str.Append("</tbody>");
                            str.Append("</table>");
                            str.Append("<div class='row'>");
                            str.Append("<div class='col-md-offset-9 col-md-12'>");
                            str.Append("<button type = 'button' class='btn btn-outline green button-next' onclick='popup()' id='btncont'>Submit<i class='fa fa-angle-right'></i></button>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</form>");
                            str.Append("</div>");
                        }
                        else
                        {
                            str.Append("<div class='portlet-title'>");
                            str.Append("<div class='caption'>");
                            str.Append(" <i class='icon-layers font-red'></i>");
                            str.Append(" <span class='caption-subject font-red bold uppercase'>");
                            str.Append(dt.Tables[0].Rows[0]["scheme_name_u"].ToString().Trim());
                            str.Append("</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='tab-pane' id='tab5'>");
                            if (dt.Tables[1] != null)
                            {
                                if (dt.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                                    {
                                        str.Append("<a href='"+ dt.Tables[1].Rows[i]["NavUrl"].ToString().Trim() + "' class='btn btn-outline green button-next'>" + dt.Tables[1].Rows[i]["Description"].ToString().Trim() + "</a>&nbsp;&nbsp;");
                                    }

                                }
                            }
                            //str.Append("<button onclick = 'funopen('../user/GetCMSY_ApplicationPrint')' class='btn btn - outline green button - next'>Print Application Form</button>");
                            //str.Append("<button onclick = 'funopen('../user/certificate_Letter')' class='btn btn - outline green button - next'>Print Certificate Letter</button>");
                            //str.Append("<button onclick = 'funopen('../user/Affidavit_Letter')' class='btn btn - outline green button - next'>Print Affidavit Letter</button>");
                            str.Append("</div>");
                        }

                    }
                }
            }
            str.Append("</div>");
            uf.page = str.ToString().Trim();
            return View(uf);
        }
        public ActionResult Affidavit_Letter()
        {
            return View();
        }
        public ActionResult DLTFC_Applicant()
        {
            return View();
        }

        //

        public ActionResult LogOut()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                Session.Abandon(); // it will clear the session at the end of request
                return RedirectToAction("Registration_Login", "Login");
            }
            catch
            {
                throw;
            }
            finally
            {
                Session.Abandon();
            }
        }

        public ActionResult GetEstate(short ID)
        {
            List<SelectListItem> Estate = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Estate, "IE", ID.ToString().Trim(), "");
            return Json(Estate, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRelation()
        {
            List<SelectListItem> Estate = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Estate, "R", "", "");
            return Json(Estate, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZone()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "Z", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Getbank()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "Bank", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getartplace()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "AP", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Getbranch()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "branch", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEDP()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "EDP", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUpdateSPY_Applicant(SPY_Applicant Objform)
        {
            try
            {



                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(Objform.inputdob);
                    Objform.dob = dt;
                }
                catch (Exception)
                {
                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                try
                {
                    DateTime dt2 = BLL.CommonBL.Setdate(Objform.inputawardyear == null ? "01/01/1900" : Objform.inputawardyear);
                    Objform.awardyear = dt2;
                }
                catch (Exception)
                {

                    throw;
                }

                Objform.@Password = "";


                string str = new DAL.CommonDA().InsertUpdatevSPY(Objform, "2");
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult InsertUpdateMHPY_Applicant(MHPY_Applicant Objform)
        {
            try
            {
                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(Objform.inputdob);
                    Objform.dob = dt;
                }
                catch (Exception)
                {
                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }


                Objform.@Password = "";


                string str = new DAL.CommonDA().InsertUpdateMHPY(Objform, "2");
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult InsertUpdatePlotApplicant(estate_request objER, List<requested_plot> objRP, IndustrialEstateApplicant objApp)
        {
            try
            {
                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(objApp.inputdob);
                    objApp.dob = dt;
                }
                catch (Exception)
                {

                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                string[] dobTime = objApp.inputdob.ToString().Split(' ');
                string[] dob = dobTime[0].Split('/');
                // string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(dob[2].ToString().Trim() + dob[1].ToString().Trim() + dob[0].ToString().Trim(), "sha256");
                objApp.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(dob[2].ToString().Trim() + dob[1].ToString().Trim() + dob[0].ToString().Trim() + "234", "sha256");
                string str = new DAL.CommonDA().Plot_Applicant(objER, objRP, objApp, false);
                if (str.Contains("Save") && str.Contains("/"))
                {
                    string[] Msg = str.Split('/');
                    return Json(Msg[0] + "\n" + "User Name :" + Msg[1] + "\n" + "Password :" + dob[2].ToString().Trim() + dob[1].ToString().Trim() + dob[0].ToString().Trim(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(str, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult ChangePassword(LoginModal ChangePwd)
        {
            DataSet ds = new DataSet();
            ds = UserDtl.VerifyUser(UserSession.LoggedInUserName.ToString());
            //btnlogin.Attributes.Add("onclick", "return HashPwdwithSalt('" + salt.ToString() + "');");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                    string lpsw = ds.Tables[0].Rows[0]["OldPassWord"].ToString();
                    string pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.OldPassword_CHG.ToString(), "sha256");
                    string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.NewPassword_CHG.ToString(), "sha256");

                    string hashed_pwd = pwd_salt;
                    string hashed_newpwd = type_pwd_salt;
                    if ((lpsw.ToLower().Equals(hashed_newpwd.ToLower())) == true)
                    {
                        ViewBag.ErrMessage = "Your new password should not match with last old password ?";
                        //return RedirectToAction("Index", "Default");
                    }
                    if ((psw.ToLower().Equals(hashed_newpwd.ToLower())) == false)
                    {
                        if (psw.ToLower().Equals(hashed_pwd.ToLower()))
                        {
                            string hashed_pwdNew = type_pwd_salt;
                            string res = UserDtl.Userpasswordchange(UserSession.LoggedInUserName, hashed_pwdNew.ToLower(), hashed_pwd.ToLower());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + res + "'); window.location='/EPDS2017/logout.aspx';", true);
                            //return RedirectToAction("ChangePassword", "Login");
                            ViewBag.ErrMessage = "Your Password Changed Successfully...";
                        }
                        else
                        {
                            ViewBag.ErrMessage = "Your old Password is not correct ?";
                            //return RedirectToAction("Index", "Default");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Your new Password mathced with old password Please change new password. ?";
                        //return RedirectToAction("Index", "Default");
                    }
                }
                else
                {
                    //return RedirectToAction("Index", "Default");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Invalid Username or Password.";
                //return RedirectToAction("FirstTimeLogin", "Login");
            }
            return View();
        }

        public ActionResult Getcategory()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "C", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getgender()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "G", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getqualification()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "Q", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getsp_category()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "SPC", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getindustry(string Is_service)
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "ind", Is_service.Trim(), "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getbankifsc(int @bank_code, int district)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                DataTable dt = new DAL.CommonDA().GetBankDetails(@bank_code, district);
                str.Append("<div class='portlet box green'>");
                str.Append("<div class='portlet-title red'>");
                str.Append("<div class='caption'>");
                str.Append("<i class='fa fa-globe'></i>  बैंक विवरण प्राप्त करें");
                str.Append("</div>");
                str.Append("<div class='tools'> </div>");
                str.Append("</div>");
                str.Append("<div class='portlet-body'>");
                str.Append("<table class='table table-striped table-bordered table-hover' id='Datatable'>");
                str.Append("<thead>");
                str.Append("<tr>");
                str.Append("<th width='15%'> </th>");
                str.Append("<th width='5%'> क्र० सं० </th>");
                str.Append("<th width='10%'> जनपद   </th>");
                str.Append("<th width='15%'> बैक का नाम </th>");
                str.Append("<th width='15%'> आईएफएससी कोड</th>");
                str.Append("<th width='15%'> शाखा  का नाम</th>");
                str.Append("<th width='15%'> शाखा  का पता</th>");

                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            str.Append("<tr>");
                            str.Append("<td><input type='button' value='Select' onclick='funbifscselect(&#39;" + dt.Rows[i]["ifsc"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[i]["branch_code"].ToString().Trim() + "&#39;,&#39;" + dt.Rows[i]["address"].ToString().Trim() + "&#39;)' /></td>");
                            str.Append("<td>" + (i + 1) + "</td>");
                            str.Append("<td>" + dt.Rows[i]["dist_name_u"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["bank_name"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["ifsc"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["BrName"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["address"].ToString().Trim() + "</td>");

                            str.Append("</tr>");
                        }
                    }
                }
                str.Append("</tbody>");
                str.Append("</table>");
                str.Append("</div>");
                str.Append("</div>");
                return Json(str.ToString().Trim(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }
        }
         public string Message(string sch,string steps)
        {
            string str = "";
            if (sch=="MYSY")
            {
                if (steps.Trim() == "0")
                {
                    //  str = "<div role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div>< div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  व्यक्तिगत विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  व्यक्तिगत विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया है ।</a></p></form></div></div></div></div>";
                }
                if (steps.Trim() == "1")
                {
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  परिवार विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                }
                if (steps.Trim() == "2")
                {
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में   परियोजना विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                }
                if (steps.Trim() == "3")
                {
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  बैंक विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                    //str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  व्यक्तिगत विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                }
                if (steps.Trim() == "4")
                {
                    //str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  बैंक विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/PlantAndMachinery_EntryForm'>अपने अभी युवा स्वरोजगार योजना में  प्लांट एंड मशीनरी विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                }
                if (steps.Trim() == "5")
                {
                    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/PlantAndMachinery_EntryForm'>अपने अभी युवा स्वरोजगार योजना में वित्तीय विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                  //  str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में  प्लांट एंड मशीनरी विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                }
                //if (steps.Trim() == "6")
                //{
                //    str = "<div   role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><h2><i class='icon-info'></i>&nbsp;&nbsp;&nbsp;<label class=''> सूचना  </label>  </h2></div><div class='modal-body'><form><p class='font-red'> <a href='../user/MYSY_SchemeForm'>अपने अभी युवा स्वरोजगार योजना में वित्तीय विवरण की समस्त प्रविष्टियो को अपलोड नहीं किया  है।</a></p></form></div></div></div></div>";
                //}
            }
            return str;
        }


    }
}