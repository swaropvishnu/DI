using DI.BLL;
using DI.Filters;
using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    public class FormController : Controller
    {
        IndustrialAreaMasterModal dd = new IndustrialAreaMasterModal();
        CommonBL bl = new CommonBL();
        [SessionExpireFilterAttribute]
        public ActionResult EntryForm()
        {
            return View();
        }
        public ActionResult Uploadform(string name , string yojana)
        {
            uploadform uf = new Models.uploadform();
            DataTable  dt = new DAL.CommonDA().GetApplicant_Doc(new DI.Crypto().Decrypt(name.Trim()),short.Parse( new DI.Crypto().Decrypt(yojana.Trim())),-1);
            StringBuilder str = new StringBuilder();
            str.Append("<div class='portlet light ' id='form_wizard_1'>");
            if (dt != null)
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       // if (dt.Tables[0].Rows[0]["Status"].ToString().Trim() == "0")
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
                            if (dt.Select("doc_code='I'") != null && dt.Select("doc_code='I'").Length > 0)
                            {
                                DataTable dtimage = dt.Select("doc_code='I'").CopyToDataTable();
                                str.Append("<div class='col-md-6 col-sm-6 col-xs-12 text-center col-50'>");
                                str.Append("<div class='myborder' style='border:1px solid #ccc;'>");
                                str.Append("<div class='panel-heading'>फोटो </div>");
                                str.Append("<div style='height:200px;padding:5px;'>");
                                if (dtimage.Rows[0]["doc"].ToString().Trim()=="")
                                {
                                    str.Append("<img  id='output' " + "src='" + "data:image/" + dtimage.Rows[0]["doc_content_type"].ToString().Trim() + ";base64," + Convert.ToBase64String((Byte[])(dtimage.Rows[0]["doc"]), 0) + "' alt='image' class='images-01'/>");
                                }
                                else
                                {
                                     str.Append("<img id='output' src='../no_image.png' alt='image' class='images-01'>");
                                }

                                
                                str.Append("</div>");
                                str.Append("<span class='font-red'>Size:</span> 20kb");
                                str.Append("<span class='font-red'>Resolution:</span> 110 X 140");
                                str.Append("<div style='border:1px solid #ccc;padding:5px;'>");
                                str.Append("<input type='file' id='FileUpload1' class='form-control' data-min-width='110' data-min-height='140' data-max-width='110' data-max-height='140'   onchange='loadPayFile(event,&#39;FileUpload1&#39;,&#39;I&#39;,&#39;" + dtimage.Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;output&#39;)' /><input type='hidden' id='hfPpath' /><div class='clearfix'></div>");
                                str.Append("</div>");
                                str.Append("</div>");
                                str.Append("</div>");
                            }
                            if (dt.Select("doc_code='S'") != null && dt.Select("doc_code='S'").Length > 0)
                            {
                                DataTable dtSig = dt.Select("doc_code='S'").CopyToDataTable();
                                str.Append("<div class='col-md-6 col-sm-6 col-xs-12 text-center col-50'>");
                                str.Append("<div class='myborder' style='border:1px solid #ccc;'>");
                                str.Append("<div class='panel-heading'>हस्ताक्षर</div>");
                                str.Append("<div style='height:200px;padding:5px;'>");
                                if (dtSig.Rows[0]["doc"].ToString().Trim() == "")
                                {
                                    str.Append("<img  id='Img1' " + "src='" + "data:image/" + dtSig.Rows[0]["doc_content_type"].ToString().Trim() + ";base64," + Convert.ToBase64String((Byte[])(dtSig.Rows[0]["doc"]), 0) + "' alt='image' class='images-01'  width='200px' height='150px'/>");
                                }
                                else
                                {
                                    str.Append("<img id='Img1' src='../no_image.png' alt='image' class='images-01' width='200px' height='150px'></div>");
                                }
                                str.Append("</div>");
                                str.Append("<span class='font-red'>Size:</span> 20kb");
                                str.Append("<span class='font-red'>Resolution:</span> 140 X 110");
                                str.Append("<div style='border:1px solid #ccc;padding:5px;'>");
                                str.Append("<input type='file' id='FileUpload2' class='form-control' onchange='loadPayFile(event,&#39;FileUpload2&#39;,&#39;S&#39;,  &#39;" + dt.Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;Img1&#39;)' />");
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
                            str.Append("<th> Preview </th>");
                            str.Append("<th> Action </th>");
                            str.Append("</tr>");
                            str.Append("</thead>");
                            str.Append("<tbody>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["doc_code"].ToString() != "I" && dt.Rows[i]["doc_code"].ToString() != "S")
                                {
                                    str.Append("<tr>");
                                    str.Append("<td>" + (i + 1) + "</td>");
                                    str.Append("<td>" + dt.Rows[i]["u_description"].ToString() + "</td>");
                                    if (dt.Rows[i]["doc"].ToString()!="")
                                    {
                                        str.Append("<td><a href='javascript:;' onclick ='DownloadFile(&#39" + name + "&#39,&#39" + yojana + "&#39," + dt.Rows[i]["doc_id"].ToString() + ")'>download<i class='fa fa-download'></i></a></td>");
                                    }
                                    else
                                    {
                                        str.Append("<td><a href='javascript:;>pending<i class='fa fa-clock-o'></i></a></td>");
                                    }
                                    

                                    str.Append("<td><input type='file' id='FileUpload" + (i + 3) + "' class='form - control' onchange='loadPayFile(event,  &#39;FileUpload" + (i + 3) + "&#39;, &#39;" + dt.Rows[i]["doc_code"].ToString().Trim() + "&#39;,&#39;" + dt.Rows[i]["registration_code"].ToString().Trim() + "&#39; , &#39;-1&#39;)'/></td>");
                                    str.Append("</tr>");
                                }
                            }
                            str.Append("</tbody>");
                            str.Append("</table>");
                            str.Append("<div class='row'>");
                            str.Append("<div class='col-md-offset-9 col-md-12'>");
                            str.Append("<button type = 'button' class='btn btn-outline green button-next' onclick='myfunction()' id='btncont'>Submit<i class='fa fa-angle-right'></i></button>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</form>");
                            str.Append("</div>");
                            str.Append("</div>");
                        }

                    }
                }
            }
            str.Append("</div>");
            uf.page = str.ToString().Trim();
            return View(uf);
        }
        public ActionResult GetAll_MYSY_Applicant()
        {
            return View();
        }
        public ActionResult MYSY_Application_Print()
        {
            return View();
        }
        public ActionResult View_Applicant(string Name)
        {
            MYSY_Plant_Machinery CM = new MYSY_Plant_Machinery();
            try
            {
                DataSet ds = new DAL.CommonDA().GetApplicantinfo_MYSY(-1, -1, new DI.Crypto().Decrypt(Name));
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
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
                        CM.industry_code = int.Parse(ds.Tables[0].Rows[0]["industry_code"].ToString().Trim());
                        CM.category_suffix = (ds.Tables[0].Rows[0]["category_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["category_suffix"].ToString().Trim());
                        CM.industry_activity = (ds.Tables[0].Rows[0]["industry_activity_suffix"].ToString().Trim());
                        CM.sp_category_suffix = (ds.Tables[0].Rows[0]["sp_category_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["sp_category_suffix"].ToString().Trim());
                        CM.gender_suffix = (ds.Tables[0].Rows[0]["gender_suffix"].ToString().Trim() == "" ? "-1" : ds.Tables[0].Rows[0]["gender_suffix"].ToString().Trim());
                        CM.pancard = (ds.Tables[0].Rows[0]["pancard"].ToString().Trim());
                        CM.is_address_same = (ds.Tables[0].Rows[0]["is_address_same"].ToString().Trim());
                        CM.Is_edp_training = (ds.Tables[0].Rows[0]["Is_edp_training"].ToString().Trim());
                        CM.edp_training_ins = (ds.Tables[0].Rows[0]["edp_training_ins"].ToString().Trim());
                        CM.sponsoring_office_code = int.Parse(ds.Tables[0].Rows[0]["sponsoring_office_code"].ToString().Trim());
                        CM.bank_code = short.Parse(ds.Tables[0].Rows[0]["bank_code"].ToString().Trim());
                        CM.branch_Address = ds.Tables[0].Rows[0]["ifsc_address"].ToString().Trim();
                        CM.ifsc = ds.Tables[0].Rows[0]["ifsc_code"].ToString().Trim();
                       /// CM.ifsc = ds.Tables[0].Rows[0]["ifsc_address"].ToString().Trim();
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
                                        str.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='del(" + (i + 1) + ")'></td>");
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
                        CM.manufacturing = ds.Tables[0].Rows[0]["manufacturing"].ToString().Trim();
                        CM.services = ds.Tables[0].Rows[0]["services"].ToString().Trim();
                        if (ds.Tables[4] != null)
                        {
                            if (ds.Tables[4].Rows.Count > 0)
                            {
                                StringBuilder str4 = new StringBuilder();
                                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                {
                                    str4.Append("<tr>");
                                    str4.Append("<td>" + (i + 1) + "</td>");
                                    str4.Append("<td><input type='text' id='txtMachine" + (i + 1) + "' placeholder='Title' name='txtMachine' value='" + ds.Tables[4].Rows[i]["machine_name"].ToString().Trim() + "' maxlength='100'></td>");
                                    str4.Append("<td><input type='text' id='txtcost" + (i + 1) + "' placeholder='Title' name='txtcost' value='" + ds.Tables[4].Rows[i]["price"].ToString().Trim() + "' maxlength='100'></td>");
                                    str4.Append("<td><input type='text' id='txtsupplier" + (i + 1) + "' placeholder='Title' name='txtsupplier' value='" + ds.Tables[4].Rows[i]["supplier"].ToString().Trim() + "' maxlength='100'></td>");
                                    str4.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='del(" + (i + 1) + ")'></td>");
                                    str4.Append("</tr>");
                                }
                                CM.machinetable = str4.ToString().Trim();
                                CM.fixed_deposite = decimal.Parse(ds.Tables[4].Rows[0]["fixed_deposite"].ToString().Trim());
                                CM.working_capital = decimal.Parse(ds.Tables[4].Rows[0]["working_capital"].ToString().Trim());
                                CM.project_cost = decimal.Parse(ds.Tables[4].Rows[0]["project_cost"].ToString().Trim());
                                CM.Marketingsystem = ds.Tables[4].Rows[0]["marketing"].ToString().Trim();
                            }
                            
                        }
                        if (ds.Tables[5] != null)
                        {
                            if (ds.Tables[5].Rows.Count > 0)
                            {
                                StringBuilder str5 = new StringBuilder();
                                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                {
                                    str5.Append("<tr>");
                                    str5.Append("<td>"+(i+1)+"</td>");
                                    str5.Append("<td>");
                                    str5.Append("<input type='text' id='txtShare"+ (i + 1) + "' placeholder='share' name='txtShare' value='" + ds.Tables[5].Rows[i]["self_share"].ToString().Trim() + "' maxlength='8' onkeypress='return IsNumeric(event);'>");
                                    str5.Append("</td>");
                                    str5.Append("<td><input type='text' id='txtbank_loan" + (i + 1) + "' placeholder='loan' name='txtbank_loan' value='" + ds.Tables[5].Rows[i]["bank_loan"].ToString().Trim() + "' maxlength='8' onkeypress='return IsNumeric(event);'></td>");
                                    str5.Append("<td><input type='text' id='txtmargine" + (i + 1) + "' placeholder='margine' name='txtmargine' value='" + ds.Tables[5].Rows[i]["margin_money"].ToString().Trim() + "' maxlength='8' onkeypress='return IsNumeric(event);'></td>");
                                    str5.Append("<td><label>"+ ds.Tables[5].Rows[i]["total"].ToString().Trim() + "</label></td>");
                                    str5.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='del2("+ (i + 1) + ")'></td>");
                                    str5.Append("</tr>");
                                }
                                CM.fintable = str5.ToString().Trim();
                                CM.total_Production = decimal.Parse(ds.Tables[5].Rows[0]["total_Production"].ToString().Trim());
                                CM.approx_sale = decimal.Parse(ds.Tables[5].Rows[0]["approx_sale"].ToString().Trim());
                                CM.profit = decimal.Parse(ds.Tables[5].Rows[0]["profit"].ToString().Trim());
                            }
                        }
                    }
                }
                return View(CM);
            }
            catch (Exception ex)
            {

                return View(CM);
            }


        }
        [SessionExpireFilterAttribute]
        public ActionResult DistrictIndrustiesInformation()
        {
            ViewBag.DisLst = DllDistrict();
            return View();
        }
        #region Dropdown
        public List<SelectListItem> DllDistrict()
        {
            DataSet objDt = new DataSet();
            objDt = CommonBL.bindDropDownHn("proc_Detail", "DS", "", "");
            List<SelectListItem> objLst = new List<SelectListItem>();
            SelectListItem Items;
            Items = new SelectListItem();
            Items.Text = "--SELECT--";
            Items.Value = "-1";
            objLst.Add(Items);
            if (objDt != null && objDt.Tables[0].Rows.Count > 0)
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
        public ActionResult GetMYSY()
        {
            return View();

        }
        public ActionResult InsertUpdateYojona_Master()
        {
            return View();
        }
        public ActionResult Get_Data_MYSY()
        {
            return View();
        }
        public ActionResult Entry_plant_machinery()
        {
            return View();
        }
        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Family> objFamily, string @sptype, string steps, string PensionCard, decimal family_income, List<Plant_Machinery> objMachine, List<Plant_Machinery> objFinance)
        {
            try
            {
                try
                {
                    DateTime dtime = BLL.CommonBL.Setdate(Objform.inputdob);
                    Objform.dob = dtime;
                }
                catch (Exception)
                {

                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                Objform.@Password = "";
                Objform.steps = steps;

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
                            dt.Rows[dt.Rows.Count - 1]["workingfield"] = objFamily[i].workingfield;
                        }
                    }
                }
                DataTable dtMachine = new DataTable();
                if (objMachine != null)
                {
                    if (objMachine.Count > 0)
                    {
                        dtMachine.Columns.Add("registration_code", typeof(long));
                        dtMachine.Columns.Add("machine_name", typeof(string));
                        dtMachine.Columns.Add("price", typeof(decimal));
                        dtMachine.Columns.Add("supplier", typeof(string));
                        dtMachine.Columns.Add("fixed_deposite", typeof(decimal));
                        dtMachine.Columns.Add("working_capital", typeof(decimal));
                        dtMachine.Columns.Add("project_cost", typeof(decimal));
                        dtMachine.Columns.Add("marketing", typeof(string));
                        for (int i = 0; i < objMachine.Count; i++)
                        {
                            dtMachine.Rows.Add();
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["registration_code"] = objMachine[i].registration_code;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["machine_name"] = objMachine[i].machine_name;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["price"] = objMachine[i].price;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["supplier"] = objMachine[i].supplier;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["fixed_deposite"] = objMachine[i].fixed_deposite;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["working_capital"] = objMachine[i].working_capital;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["project_cost"] = objMachine[i].project_cost;
                            dtMachine.Rows[dtMachine.Rows.Count - 1]["marketing"] = objMachine[i].Marketingsystem;
                        }
                    }
                }
                DataTable dtfin = new DataTable();
                if (objFinance != null)
                {
                    if (objFinance.Count > 0)
                    {
                        dtfin.Columns.Add("registration_code", typeof(long));
                        dtfin.Columns.Add("self_share", typeof(decimal));
                        dtfin.Columns.Add("bank_loan", typeof(decimal));
                        dtfin.Columns.Add("margin_money", typeof(decimal));
                        dtfin.Columns.Add("total_Production", typeof(decimal));
                        dtfin.Columns.Add("approx_sale", typeof(decimal));
                        dtfin.Columns.Add("profit", typeof(decimal));

                        for (int i = 0; i < objFinance.Count; i++)
                        {
                            dtfin.Rows.Add();
                            dtfin.Rows[dtfin.Rows.Count - 1]["registration_code"] = objFinance[i].registration_code;
                            dtfin.Rows[dtfin.Rows.Count - 1]["self_share"] = objFinance[i].self_share;
                            dtfin.Rows[dtfin.Rows.Count - 1]["bank_loan"] = objFinance[i].bank_loan;
                            dtfin.Rows[dtfin.Rows.Count - 1]["margin_money"] = objFinance[i].margin_money;
                            dtfin.Rows[dtfin.Rows.Count - 1]["total_Production"] = objFinance[i].total_Production;
                            dtfin.Rows[dtfin.Rows.Count - 1]["approx_sale"] = objFinance[i].approx_sale;
                            dtfin.Rows[dtfin.Rows.Count - 1]["profit"] = objFinance[i].profit;

                        }
                    }
                }
                string str = new DAL.CommonDA().InsertUpdateCMYSS_ApplicantbyAdmin(Objform, "2", dt, PensionCard, family_income, dtMachine, dtfin);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public FileResult DownloadFile(string name , string yojana, short id )
        {
            byte[] bytes;
            string fileName, contentType;
            DataTable dt = new DAL.CommonDA().GetApplicant_Doc(new DI.Crypto().Decrypt(name.Trim()), short.Parse(new DI.Crypto().Decrypt(yojana.Trim())), id);

            bytes =  (Byte[])(dt.Rows[0]["doc"]);
            fileName = dt.Rows[0]["doc_name"].ToString().Trim();
            contentType = dt.Rows[0]["content_type"].ToString().Trim();
            return File(bytes, contentType, fileName);
        }

    }
}