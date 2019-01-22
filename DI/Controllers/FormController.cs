using DI.BLL;
using DI.Filters;
using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class FormController : Controller
    {
        IndustrialAreaMasterModal dd = new IndustrialAreaMasterModal();
        CommonBL bl = new CommonBL();
        public ActionResult EntryForm()
        {
            return View();
        }
        public ActionResult MYSY_MM_claim_list()
        {
            return View();
        }

        public ActionResult Dltfc_applicant_status()
        {
            return View();
        }
        public ActionResult DLTFC_meeting_cancellation()
        {
            return View();
        }
        public ActionResult bank_fin(string code)
        {
            bank_fin fin = new bank_fin();
            DataTable dt = new DAL.CommonDA().GetMYSY_Applicant_DIC("B", UserSession.LoggedInUser.UserName, long.Parse(new DI.Crypto().Decrypt(code)),"");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    fin.capital_expenditure_approved = decimal.Parse(dt.Rows[0]["capital_expenditure_approved"].ToString().Trim());
                    fin.capital_expenditure_finance= decimal.Parse(dt.Rows[0]["capital_expenditure_finance"].ToString().Trim());
                    fin.bank_finance_per= dt.Rows[0]["bank_finance_per"].ToString().Trim();
                    fin.applicant_name = dt.Rows[0]["applicant_name"].ToString().Trim();
                    fin.applicant_code = dt.Rows[0]["user_name"].ToString().Trim();
                    fin.caste = dt.Rows[0]["caste"].ToString().Trim();
                    fin.category = dt.Rows[0]["category"].ToString().Trim();
                    fin.adhar_no = dt.Rows[0]["adhar_no"].ToString().Trim();
                    fin.pancard = dt.Rows[0]["pancard"].ToString().Trim();
                    fin.unitloaction = dt.Rows[0]["unitloaction"].ToString().Trim();
                    fin.industrytype = dt.Rows[0]["industrytype"].ToString().Trim();
                    fin.industry = dt.Rows[0]["industry"].ToString().Trim();
                    fin.Qualification = dt.Rows[0]["Qualification"].ToString().Trim();
                    fin.Project_cost = dt.Rows[0]["Project_cost"].ToString().Trim();
                    fin.benificary_contribution = dt.Rows[0]["Project_cost"].ToString().Trim();
                    fin.gender = dt.Rows[0]["gender"].ToString().Trim();
                    fin.email = dt.Rows[0]["email"].ToString().Trim();
                    fin.mobile = dt.Rows[0]["mobile"].ToString().Trim();
                    fin.steps = dt.Rows[0]["steps"].ToString().Trim();
                    fin.scheme_code = short.Parse(dt.Rows[0]["steps"].ToString().Trim());
                    fin.registration_code = long.Parse(dt.Rows[0]["registration_code"].ToString().Trim());
                    fin.finanace_id = long.Parse(dt.Rows[0]["finance_id"].ToString().Trim());
                    fin.account_no_beneficiary = dt.Rows[0]["account_no_beneficiary"].ToString().Trim();
                    fin.adjustment_amt = decimal.Parse(dt.Rows[0]["adjustment_amt"].ToString().Trim());
                    fin.input_adjustment_date = dt.Rows[0]["adjustment_date"].ToString().Trim()=="01/01/1900"?"": dt.Rows[0]["adjustment_date"].ToString().Trim();
                    fin.input_doc_rec_date = dt.Rows[0]["doc_rec_date"].ToString().Trim() == "01/01/1900" ? "" : dt.Rows[0]["doc_rec_date"].ToString().Trim();
                    fin.input_loan_released_last_date = dt.Rows[0]["loan_released_last_date"].ToString().Trim()=="01/01/1900"?"": dt.Rows[0]["loan_released_last_date"].ToString().Trim();
                    fin.input_mm_date = dt.Rows[0]["mm_date"].ToString().Trim()=="01/01/1900"?"": dt.Rows[0]["mm_date"].ToString().Trim();
                    fin.input_TDR_deposite_date = dt.Rows[0]["TDR_deposite_date"].ToString().Trim()== "01/01/1900"?"":dt.Rows[0]["TDR_deposite_date"].ToString().Trim();
                    fin.interest_rate = decimal.Parse(dt.Rows[0]["interest_rate"].ToString().Trim());
                    fin.is_CGTMS = (dt.Rows[0]["is_CGTMS"].ToString().Trim());
                    fin.mm_amt = decimal.Parse(dt.Rows[0]["mm_amt"].ToString().Trim());
                    fin.mm_refrence_no = dt.Rows[0]["mm_refrence_no"].ToString().Trim();
                    fin.moratorium_period = short.Parse(dt.Rows[0]["moratorium_period"].ToString().Trim());
                    fin.project_cost_approved = decimal.Parse(dt.Rows[0]["project_cost_approved"].ToString().Trim());
                    fin.project_cost_finance = decimal.Parse(dt.Rows[0]["project_cost_finance"].ToString().Trim());
                    fin.repayment_period = short.Parse(dt.Rows[0]["repayment_period"].ToString().Trim());
                    fin.TDR_acct = dt.Rows[0]["TDR_acct"].ToString().Trim();
                    fin.transient_account = dt.Rows[0]["transient_account"].ToString().Trim();
                    fin.working_capital_approved = decimal.Parse(dt.Rows[0]["working_capital_approved"].ToString().Trim());
                    fin.working_capital_finance = decimal.Parse(dt.Rows[0]["working_capital_finance"].ToString().Trim());
                    fin.remark = dt.Rows[0]["remark"].ToString().Trim();
                    fin.remark2 = dt.Rows[0]["remark2"].ToString().Trim();
                    fin.status = dt.Rows[0]["bank_status"].ToString().Trim();
                    fin.branch_code = long.Parse(dt.Rows[0]["branch_code"].ToString().Trim());
                    fin.project_cost_dltfc = dt.Rows[0]["project_cost_dltfc"].ToString().Trim();
                    fin.amt_released = decimal.Parse(dt.Rows[0]["first_Amt_Released"].ToString().Trim());
                    fin.input_Contributiondepositedate = dt.Rows[0]["contribution_deposite_date"].ToString().Trim() == "01/01/1900" ? "" : dt.Rows[0]["contribution_deposite_date"].ToString().Trim(); ;
                    fin.inputEDPFromDate = dt.Rows[0]["EDP_from_date"].ToString().Trim() == "01/01/1900" ? "" : dt.Rows[0]["EDP_from_date"].ToString().Trim(); ;
                    fin.inputEDPToDate= dt.Rows[0]["EDP_todate"].ToString().Trim() == "01/01/1900" ? "" : dt.Rows[0]["EDP_todate"].ToString().Trim(); ;
                    fin.ContributionAmt = decimal.Parse(dt.Rows[0]["contribution_amt"].ToString().Trim());
                    fin.EDP_inst_code = int.Parse(dt.Rows[0]["EDP_inst_code"].ToString().Trim());
                    fin.inputsanction_date= dt.Rows[0]["sanction_date"].ToString().Trim() == "01/01/1900" ? DateTime.Now.Day.ToString().Trim()+"/"+DateTime.Now.Month.ToString().Trim()+"/"+DateTime.Now.Year.ToString().Trim() : dt.Rows[0]["sanction_date"].ToString().Trim();
                }
            }
            return View(fin);
        }
        public ActionResult bank_view()
        {
            return View();
        }
        public ActionResult MYSY_Bank_Forword()
        {
            return View();
        }
        public ActionResult MYSY_Plant_Machinery_print()
        {
            return View();
        }
        public ActionResult DLTFC_Applicant()
        {
            return View();
        }
        public ActionResult testing_editor()
        {
            return View();
        }
        public ActionResult getstatusMYSY()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "vw", "B", "");
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Uploadform(string name, string yojana, string type)
        {
            uploadform uf = new Models.uploadform();
            DataTable dt = new DAL.CommonDA().GetApplicant_Doc(new DI.Crypto().Decrypt(name.Trim()), short.Parse(new DI.Crypto().Decrypt(yojana.Trim())), -1);
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
                                if (dtimage.Rows[0]["doc"].ToString().Trim() != "")
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
                                if (type == "U")
                                {
                                    str.Append("<div class='col-md-12'><input type='file' id='FileUpload1' class='form-control' data-min-width='110' data-min-height='140' data-max-width='110' data-max-height='140'   onchange='loadPayFile(event,&#39;FileUpload1&#39;,&#39;I&#39;,&#39;" + dtimage.Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;output&#39;,&#39;" + dtimage.Rows[0]["scheme_code"].ToString().Trim() + "&#39;)' /></div><div><input type='hidden' id='hfPpath' /></div><div class='clearfix'></div>");
                                }

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
                                if (dtSig.Rows[0]["doc"].ToString().Trim() != "")
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
                                if (type == "U")
                                {
                                    str.Append("<div class='col-md-12'><input type='file' id='FileUpload2' class='form-control' onchange='loadPayFile(event,&#39;FileUpload2&#39;,&#39;S&#39;,  &#39;" + dt.Rows[0]["registration_code"].ToString().Trim() + "&#39;,&#39;Img1&#39;,&#39;" + dt.Rows[0]["scheme_code"].ToString().Trim() + "&#39;)' /></div><div class='col-md-3'></div>");
                                }
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
                            if (type == "U")
                            {
                                str.Append("<th> Action </th>");
                            }
                            str.Append("</tr>");
                            str.Append("</thead>");
                            str.Append("<tbody>");
                            int k = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["doc_code"].ToString().Trim() != "I" && dt.Rows[i]["doc_code"].ToString().Trim() != "S")
                                {
                                    if (dt.Rows[i]["status"].ToString().Trim() == "A")
                                    {
                                        k = k + 1;
                                        str.Append("<tr>");
                                        str.Append("<td>" + k + "</td>");
                                        str.Append("<td>" + dt.Rows[i]["u_description"].ToString() + "</td>");
                                        if (dt.Rows[i]["doc"].ToString() != "")
                                        {
                                            str.Append("<td><a href='javascript:;' onclick ='DownloadFile(&#39" + name + "&#39,&#39" + yojana + "&#39," + dt.Rows[i]["doc_id"].ToString() + ")'>download<i class='fa fa-download'></i></a></td>");
                                        }
                                        else
                                        {
                                            str.Append("<td><i class='fa fa-clock-o'>  pending</i></td>");
                                        }
                                        if (type == "U")
                                        {

                                            str.Append("<td><div class='col-md-9'><input type='file' id='FileUpload" + (i + 3) + "' class='form - control' onchange='loadPayFile(event,  &#39;FileUpload" + (i + 3) + "&#39;, &#39;" + dt.Rows[i]["doc_code"].ToString().Trim() + "&#39;,&#39;" + dt.Rows[i]["registration_code"].ToString().Trim() + "&#39; , &#39;-1&#39;,&#39;" + dt.Rows[0]["scheme_code"].ToString().Trim() + "&#39;)'/> </div></td>");
                                        }
                                        str.Append("</tr>");
                                    }

                                }
                            }
                            str.Append("</tbody>");
                            str.Append("</table>");
                            str.Append("<div class='row'>");
                            str.Append("<div class='col-md-offset-9 col-md-12'>");
                            //str.Append("<textarea name='content' class='md-editor active' rows='10'></textarea>");
                            //str.Append("<button type = 'button' class='btn btn-outline green button-next' onclick='myfunction()' id='btncont'>Submit<i class='fa fa-angle-right'></i></button>");
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
                        CM.fixed_deposite = decimal.Parse(ds.Tables[0].Rows[0]["machinery_cost"].ToString().Trim());
                        CM.working_capital = decimal.Parse(ds.Tables[0].Rows[0]["working_capital"].ToString().Trim());
                        CM.project_cost = decimal.Parse(ds.Tables[0].Rows[0]["project_cost"].ToString().Trim());
                        CM.branch_code = int.Parse(ds.Tables[0].Rows[0]["branch_code"].ToString().Trim());
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
                        CM.bank_code = short.Parse(ds.Tables[0].Rows[0]["bank_code"].ToString().Trim());
                        CM.branch_Address = ds.Tables[0].Rows[0]["ifsc_address"].ToString().Trim();
                        CM.ifsc = ds.Tables[0].Rows[0]["ifsc_code"].ToString().Trim();
                        CM.permanent_add_pincode = ds.Tables[0].Rows[0]["permanent_add_pincode"].ToString().Trim();
                        CM.curent_add_pincode = ds.Tables[0].Rows[0]["curent_add_pincode"].ToString().Trim();
                        CM.@proposed_office_pincode = ds.Tables[0].Rows[0]["proposed_office_pincode"].ToString().Trim();

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
                        CM.other_product_service = ds.Tables[0].Rows[0]["Industry_apply"].ToString().Trim();
                        // CM.services = ds.Tables[0].Rows[0]["services"].ToString().Trim();
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

                                    str4.Append("<td><input type='text' id='txtsupplier" + (i + 1) + "' placeholder='Title' name='txtsupplier' value='" + ds.Tables[4].Rows[i]["supplier"].ToString().Trim() + "' maxlength='100' ></td>");
                                    str4.Append("<td><input type='text' id='txtcost" + (i + 1) + "' placeholder='Title' name='txtcost' value='" + ds.Tables[4].Rows[i]["price"].ToString().Trim() + "' maxlength='100' onkeyup='sumcost()'></td>");
                                    str4.Append("<td><input type='button' value='Delete' class='btn red' id='btnadd' onclick='delmachine(this)'></td>");
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
                                CM.self_share = decimal.Parse(ds.Tables[5].Rows[0]["self_share"].ToString());
                                CM.bank_loan = decimal.Parse(ds.Tables[5].Rows[0]["bank_loan"].ToString());
                                CM.margin_money = decimal.Parse(ds.Tables[5].Rows[0]["margin_money"].ToString());
                                CM.total_Production = decimal.Parse(ds.Tables[5].Rows[0]["total_Production"].ToString().Trim());
                                CM.approx_sale = decimal.Parse(ds.Tables[5].Rows[0]["approx_sale"].ToString().Trim());
                                CM.profit = decimal.Parse(ds.Tables[5].Rows[0]["approx_profit"].ToString().Trim());
                                CM.edp_training_ins_code = int.Parse(ds.Tables[0].Rows[0]["edp_training_ins_code"].ToString().Trim());
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

        //public ActionResult DistrictIndrustiesInformation()
        //{
        //    ViewBag.DisLst = DllDistrict();
        //    return View();
        //}

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
        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Family> objFamily, string @sptype, string PensionCard, decimal family_income, List<Plant_Machinery> objMachine, decimal total_Production, decimal approx_sale, decimal approx_profit)
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
                Objform.steps = "7";

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


                string str = new DAL.CommonDA().InsertUpdateCMYSS_ApplicantbyAdmin(Objform, "2", dt, PensionCard, family_income, dtMachine, total_Production, approx_sale, approx_profit);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public FileResult DownloadFile(string name, string yojana, short id)
        {
            byte[] bytes;
            string fileName, contentType;
            DataTable dt = new DAL.CommonDA().GetApplicant_Doc(new DI.Crypto().Decrypt(name.Trim()), short.Parse(new DI.Crypto().Decrypt(yojana.Trim())), id);

            bytes = (Byte[])(dt.Rows[0]["doc"]);
            fileName = dt.Rows[0]["doc_name"].ToString().Trim();
            contentType = dt.Rows[0]["content_type"].ToString().Trim();
            return File(bytes, contentType, fileName);
        }

        [HttpPost]
        public FileResult bankDownloadFile(long id)
        {
            byte[] bytes;
            string fileName, contentType;
            DataTable dt = new DAL.CommonDA().GetMYSY_Applicant_DIC("B","",id,"");

            bytes = (Byte[])(dt.Rows[0]["doc"]);
            fileName = "Bank"+ dt.Rows[0]["user_name"].ToString().Trim()+"."+ dt.Rows[0]["doc_content_type"].ToString().Trim();
            contentType = dt.Rows[0]["doc_content_type"].ToString().Trim();
            return File(bytes, contentType, fileName);
        }
        public JsonResult InsertUpdateMeeting(mysy_meeting_schedule mysy, string sptype)
        {
            try
            {

                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(mysy.inputdate);
                    mysy.meeting_date = dt;
                }
                catch (Exception)
                {

                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                DataTable dtapplicant = new DataTable();
                dtapplicant.Columns.Add("meeting_id", typeof(long));
                dtapplicant.Columns.Add("registration_code", typeof(long));
                dtapplicant.Columns.Add("status", typeof(char));
                dtapplicant.Columns.Add("entrydate", typeof(DateTime));
                dtapplicant.Columns.Add("authority_level", typeof(char));
                dtapplicant.Rows.Add();
                dtapplicant.Rows[dtapplicant.Rows.Count - 1]["meeting_id"] = "-1";
                dtapplicant.Rows[dtapplicant.Rows.Count - 1]["registration_code"] = mysy.registration_code;
                dtapplicant.Rows[dtapplicant.Rows.Count - 1]["status"] = "A";
                dtapplicant.Rows[dtapplicant.Rows.Count - 1]["entrydate"] = DateTime.Now;
                dtapplicant.Rows[dtapplicant.Rows.Count - 1]["authority_level"] = "A";



                string str = new DAL.CommonDA().InsertUpdate_Mysy_meeting(mysy, sptype, dtapplicant);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertBulkMeeting(mysy_meeting_schedule mysy, string sptype, List<meeting_applicant> L1)
        {
            try
            {
                if (L1.Count == 0)
                {
                    return Json("Select applicant", JsonRequestBehavior.AllowGet);
                }

                try
                {
                    DateTime dt = BLL.CommonBL.Setdate(mysy.inputdate);
                    mysy.meeting_date = dt;
                }
                catch (Exception)
                {

                    return Json("Date of Birth must be dd/mm/yyyy format", JsonRequestBehavior.AllowGet);
                }
                DataTable dtapplicant = new DataTable();
                dtapplicant.Columns.Add("meeting_id", typeof(long));
                dtapplicant.Columns.Add("registration_code", typeof(long));
                dtapplicant.Columns.Add("status", typeof(char));
                dtapplicant.Columns.Add("entrydate", typeof(DateTime));
                dtapplicant.Columns.Add("authority_level", typeof(char));
                for (int i = 0; i < L1.Count; i++)
                {
                    dtapplicant.Rows.Add();
                    dtapplicant.Rows[dtapplicant.Rows.Count - 1]["meeting_id"] = "-1";
                    dtapplicant.Rows[dtapplicant.Rows.Count - 1]["registration_code"] = L1[i].registration_code;
                    dtapplicant.Rows[dtapplicant.Rows.Count - 1]["status"] = "A";
                    dtapplicant.Rows[dtapplicant.Rows.Count - 1]["entrydate"] = DateTime.Now;
                    dtapplicant.Rows[dtapplicant.Rows.Count - 1]["authority_level"] = "D";
                }
                string str = new DAL.CommonDA().InsertUpdate_Mysy_meeting(mysy, sptype, dtapplicant);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertUpdate_mysy_status(mysy_status mysy, string sptype, string authority)
        {
            try
            {
                string str = new DAL.CommonDA().InsertUpdate_Mysy_status(mysy, sptype, authority);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertUpdate_mysy_statuswithbankfin(mysy_status mysy, bank_fin bankfin, string sptype, string authority)
        {
            try
            {
                if (bankfin.input_doc_rec_date == "")
                {
                    bankfin.doc_rec_date = BLL.CommonBL.Setdate("01/01/1900");
                }
                else
                {
                    bankfin.doc_rec_date = BLL.CommonBL.Setdate(bankfin.input_doc_rec_date.Trim());

                }
                if (bankfin.inputsanction_date == "" || bankfin.inputsanction_date.Trim() == null)
                {
                    bankfin.sanction_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.sanction_date = BLL.CommonBL.Setdate(bankfin.inputsanction_date.Trim());

                }
                if (bankfin.input_adjustment_date == "" || bankfin.input_adjustment_date == null)
                {
                    bankfin.adjustment_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.adjustment_date = BLL.CommonBL.Setdate(bankfin.input_adjustment_date.Trim());

                }
                if (bankfin.input_loan_released_last_date == "" || bankfin.input_loan_released_last_date == null && bankfin.input_loan_released_last_date == "01/01/0001")
                {
                    bankfin.loan_released_last_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.loan_released_last_date = BLL.CommonBL.Setdate(bankfin.input_loan_released_last_date.Trim());

                }
                if (bankfin.input_mm_date == "" || bankfin.input_mm_date == null)
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate(bankfin.input_mm_date.Trim());

                }
                if (bankfin.input_TDR_deposite_date == "" || bankfin.input_TDR_deposite_date == null)
                {
                    bankfin.TDR_deposite_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.TDR_deposite_date = BLL.CommonBL.Setdate(bankfin.input_TDR_deposite_date.Trim());

                }
                if (bankfin.inputEDPFromDate == "" || bankfin.inputEDPFromDate == null)
                {
                    bankfin.EDPFromDate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.EDPFromDate = BLL.CommonBL.Setdate(bankfin.inputEDPFromDate.Trim());

                }
                if (bankfin.inputEDPToDate == "" || bankfin.inputEDPToDate == null)
                {
                    bankfin.EDPToDate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.EDPToDate = BLL.CommonBL.Setdate(bankfin.inputEDPToDate.Trim());

                }
                if (bankfin.input_Contributiondepositedate == "" || bankfin.input_Contributiondepositedate == null)
                {
                    bankfin.Contributiondepositedate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.Contributiondepositedate = BLL.CommonBL.Setdate(bankfin.input_Contributiondepositedate.Trim());

                }

                string str = new DAL.CommonDA().InsertUpdate_Mysy_statuswithbankfin(mysy, bankfin, sptype, authority, "1");
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertUpdate_mysy_bankfin(bank_fin bankfin, string sptype, string steps)
        {
            try
            {
                if (bankfin.input_doc_rec_date == "")
                {
                    bankfin.doc_rec_date = BLL.CommonBL.Setdate("01/01/1900");
                }
                else
                {
                    bankfin.doc_rec_date = BLL.CommonBL.Setdate(bankfin.input_doc_rec_date.Trim());

                }
                if (bankfin.inputsanction_date == "" || bankfin.inputsanction_date.Trim() == null)
                {
                    bankfin.sanction_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.sanction_date = BLL.CommonBL.Setdate(bankfin.inputsanction_date.Trim());

                }
                if (bankfin.input_adjustment_date == "" || bankfin.input_adjustment_date == null)
                {
                    bankfin.adjustment_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.adjustment_date = BLL.CommonBL.Setdate(bankfin.input_adjustment_date.Trim());

                }
                if (bankfin.input_loan_released_last_date == "" || bankfin.input_loan_released_last_date == null && bankfin.input_loan_released_last_date == "01/01/0001")
                {
                    bankfin.loan_released_last_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.loan_released_last_date = BLL.CommonBL.Setdate(bankfin.input_loan_released_last_date.Trim());

                }
                if (bankfin.input_mm_date == "" || bankfin.input_mm_date == null)
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate(bankfin.input_mm_date.Trim());

                }
                if (bankfin.input_TDR_deposite_date == "" || bankfin.input_TDR_deposite_date == null)
                {
                    bankfin.TDR_deposite_date = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.TDR_deposite_date = BLL.CommonBL.Setdate(bankfin.input_TDR_deposite_date.Trim());

                }
                if (bankfin.inputEDPFromDate == "" || bankfin.inputEDPFromDate == null)
                {
                    bankfin.EDPFromDate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.EDPFromDate = BLL.CommonBL.Setdate(bankfin.inputEDPFromDate.Trim());

                }
                if (bankfin.inputEDPToDate == "" || bankfin.inputEDPToDate == null)
                {
                    bankfin.EDPToDate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.EDPToDate = BLL.CommonBL.Setdate(bankfin.inputEDPToDate.Trim());

                }
                if (bankfin.input_Contributiondepositedate == "" || bankfin.input_Contributiondepositedate == null)
                {
                    bankfin.Contributiondepositedate = BLL.CommonBL.Setdate("01/01/1900");

                }
                else
                {
                    bankfin.Contributiondepositedate = BLL.CommonBL.Setdate(bankfin.input_Contributiondepositedate.Trim());

                }
                if (bankfin.inputEDPToDate != "")
                {
                    if (bankfin.EDPToDate<=bankfin.EDPFromDate)
                    {
                        return Json("Training Duration To date must be greater than from date", JsonRequestBehavior.AllowGet);
                    }
                }
                string str = new DAL.CommonDA().InsertUpdate_MYSY_bank_fin(bankfin, sptype, steps);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult MM_claim_Approved(loan_adjustment_amt bankfin)
        {
            try
            {
                bankfin.adjustment_date = BLL.CommonBL.Setdate("01/01/1900");
                if (bankfin.inputmm_date.Trim() == "" || bankfin.inputmm_date.Trim() == null)
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate("01/01/1900");
                }
                else
                {
                    bankfin.mm_date = BLL.CommonBL.Setdate(bankfin.inputmm_date.Trim());
                }
                bankfin.authority = "A";
                
                string str = new DAL.CommonDA().Insert_MYSY_adjustment_amt(bankfin);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult MYSY_Adjustment(loan_adjustment_amt bankfin)
        {
            try
            {
                bankfin.mm_date = BLL.CommonBL.Setdate("01/01/1900");
                if (bankfin.inputadjustment_date.Trim() == "" || bankfin.inputadjustment_date.Trim() == null)
                {
                      return Json("Enter Adjustment Date", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bankfin.adjustment_date = BLL.CommonBL.Setdate(bankfin.inputadjustment_date.Trim());
                }
                bankfin.authority = "B";

                string str = new DAL.CommonDA().Insert_MYSY_adjustment_amt(bankfin);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMeetingDate(long @registration_code, string @authority_level)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                DataSet ds = new DAL.CommonDA().Get_Mysy_MeetingDetails(@registration_code, @authority_level, -1, "");
                str.Append("<div class='modal-header'>");
                str.Append("<button type='button' class='close' onclick='modal2close()'>&times;</button>");
                str.Append("<h3><i class='icon-paper-clip'></i>&nbsp;&nbsp;&nbsp;<label class=''> बैठक विवरण  </label>  </h3>");
                str.Append("</div>");
                str.Append("<div class='modal-body'>");
                if (ds != null)
                {
                    if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            str.Append("<div class='row'>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>पंजीकरण संख्या</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span  class='form-control'>" + ds.Tables[1].Rows[0]["user_name"].ToString().Trim() + "</span>");

                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>आवेदक</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span id = 'sp_app_name1' class='form-control'>" + ds.Tables[1].Rows[0]["applicant_name"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>पंजीकरण तारीख</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span id = 'sp_app_date1' class='form-control'>" + ds.Tables[1].Rows[0]["entrydate"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append(" <div class='row'>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>ई-मेल आई0डी0</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span class='form-control'>" + ds.Tables[1].Rows[0]["email"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>मोबाइल नं0</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span class='form-control'>" + ds.Tables[1].Rows[0]["mobile_no"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                        }
                    }
                }
                str.Append("<table class='table table-striped table-bordered table-hover' id='Datatable2'>");
                str.Append("<thead>");
                str.Append("<tr>");
                str.Append("<th width='3%'>क्र० सं०</th>");
                str.Append("<th width='10%'>मिलने का तारीख</th>");
                str.Append("<th width='10%'>मिलने का समय</th>");
                str.Append("<th width='5%'>नोडल अफसर का नाम</th>");
                str.Append("<th width='10%'>पद</th>");
                str.Append("<th width='10%'>टिप्पणी</th>");
                str.Append("<th width='10%'>Action</th>");
                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");
                if (ds != null)
                {
                    if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {//style="background-color:#faafaf;"
                                if (ds.Tables[1].Rows[i]["status"].ToString().Trim() != "A")
                                {
                                    str.Append("<tr style='background-color:#faafaf;'>");
                                }
                                else
                                {
                                    str.Append("<tr>");
                                }
                                str.Append("<td>" + (i + 1) + "</td>");
                                str.Append("<td>" + ds.Tables[1].Rows[i]["meeting_date"].ToString().Trim() + "</td>");
                                str.Append("<td>" + ds.Tables[1].Rows[i]["meeting_time"].ToString().Trim() + "</td>");
                                str.Append("<td>" + ds.Tables[1].Rows[i]["officer_name"].ToString().Trim() + "</td>");
                                str.Append("<td>" + ds.Tables[1].Rows[i]["designation"].ToString().Trim() + "</td>");
                                str.Append("<td>" + Server.HtmlDecode(ds.Tables[1].Rows[i]["remark"].ToString().Trim()) + "</td>");
                                //onclick="SaveMeeting(-1,document.getElementById('hfregistration').value,'A','1')"
                                if (ds.Tables[1].Rows[i]["status"].ToString().Trim() == "A")
                                {

                                    str.Append("<td><button id='btncancel' class='" + ds.Tables[1].Rows[i]["btnclass"].ToString().Trim() + "' onclick='CancelMeeting(&#39;" + ds.Tables[1].Rows[i]["meeting_id"].ToString().Trim() + " &#39;,&#39;" + ds.Tables[1].Rows[i]["registration_code"].ToString().Trim() + "&#39;,&#39;C&#39;,&#39;2&#39; )'>Cancel</button></td>");
                                }
                                else
                                {
                                    str.Append("<td></td>");
                                }
                                str.Append("</tr>");
                            }
                        }
                        else
                        {
                            str.Append("<tr>");
                            str.Append("<td colspan='7'>No record found</td>");
                            str.Append("</tr>");
                        }
                    }
                    else
                    {
                        str.Append("<tr>");
                        str.Append("<td colspan='7'>No record found</td>");
                        str.Append("</tr>");
                    }
                }
                else
                {
                    str.Append("<tr>");
                    str.Append("<td colspan='7'>No record found</td>");
                    str.Append("</tr>");
                }
                str.Append("</tbody>");
                str.Append("</table>");
                str.Append("</div>");
                str.Append("<div class='modal-footer'>");
                if (ds != null)
                {
                    if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            str.Append("<button type='button' class='btn btn-primary' onclick='createmeeting(&#39;" + ds.Tables[1].Rows[0]["user_name"].ToString().Trim() + "&#39;, &#39;" + ds.Tables[1].Rows[0]["applicant_name"].ToString().Trim() + "&#39;, &#39;" + ds.Tables[1].Rows[0]["entrydate"].ToString().Trim() + "&#39;, &#39;" + ds.Tables[1].Rows[0]["email"].ToString().Trim() + "&#39;, &#39;" + ds.Tables[1].Rows[0]["mobile_no"].ToString().Trim() + "&#39;, &#39;" + ds.Tables[1].Rows[0]["registration_code"].ToString().Trim() + "&#39;, &#39;0&#39;)'>Create next Meeting</button>");

                        }
                    }
                }

                str.Append("<button type='button' class='btn btn-danger' onclick='modal2close()'>Close</button>");
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
        public JsonResult Getapplication_status(long @registration_code, string @authority_level)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                DataTable dt = new DAL.CommonDA().Get_status_details(@registration_code, @authority_level);

                str.Append("<div class='modal-header'>");
                str.Append("<button type='button' class='close' onclick='modal4close()'>&times;</button>");
                str.Append("<h3><i class='icon-paper-clip'></i>&nbsp;&nbsp;&nbsp;<label class=''>  आवेदन स्थिति विवरण  </label>  </h3>");
                str.Append("</div>");
                str.Append("<div class='modal-body'>");
                // if (ds != null)
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            str.Append("<div class='row'>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>पंजीकरण संख्या</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span  class='form-control'>" + dt.Rows[0]["user_name"].ToString().Trim() + "</span>");

                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>आवेदक</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span id = 'sp_app_name1' class='form-control'>" + dt.Rows[0]["applicant_name"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>पंजीकरण तारीख</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span id = 'sp_app_date1' class='form-control'>" + dt.Rows[0]["entrydate"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append(" <div class='row'>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>ई-मेल आई0डी0</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span class='form-control'>" + dt.Rows[0]["email"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("<div class='col-md-4'>");
                            str.Append("<div class='form-group'>");
                            str.Append("<label class='control-label col-md-12' id='lblname'>मोबाइल नं0</label>");
                            str.Append("<div class='col-md-12'>");
                            str.Append("<span class='form-control'>" + dt.Rows[0]["mobile_no"].ToString().Trim() + "</span>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                            str.Append("</div>");
                        }
                    }
                }
                str.Append("<table class='table table-striped table-bordered table-hover' id='Datatable2'>");
                str.Append("<thead>");
                str.Append("<tr>");
                str.Append("<th width='3%'>क्र० सं०</th>");
                str.Append("<th width='10%'> तारीख</th>");
                str.Append("<th width='10%'> विवरण</th>");

                str.Append("<th width='10%'>टिप्पणी</th>");
                //  str.Append("<th width='10%'></th>");
                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {//style="background-color:#faafaf;"
                         //if (dt.Rows[i]["status"].ToString().Trim() != "A")
                         //{
                         //    str.Append("<tr style='background-color:#faafaf;'>");
                         //}
                         //else
                         //{
                            str.Append("<tr>");
                            //}
                            str.Append("<td>" + (i + 1) + "</td>");
                            str.Append("<td>" + dt.Rows[i]["c_time_stamp"].ToString().Trim() + "</td>");
                            str.Append("<td>" + dt.Rows[i]["status_dec"].ToString().Trim() + "</td>");
                            str.Append("<td>" + (dt.Rows[i]["remark"].ToString().Trim()) + "</td>");
                            //str.Append("<td>" + dt.Rows[i]["designation"].ToString().Trim() + "</td>");
                            //str.Append("<td>" + dt.Rows[i]["remark"].ToString().Trim() + "</td>");
                            //onclick="SaveMeeting(-1,document.getElementById('hfregistration').value,'A','1')"
                            //if (dt.Rows[i]["status"].ToString().Trim() == "A")
                            //{
                            //    str.Append("<td><button id='btncancel' class='btn btn-danger' onclick='CancelMeeting(&#39;" + dt.Rows[i]["meeting_id"].ToString().Trim() + " &#39;,&#39;" + dt.Rows[i]["registration_code"].ToString().Trim() + "&#39;,&#39;C&#39;,&#39;2&#39; )'>Cancel</button></td>");
                            //}
                            //else
                            //{
                            //    str.Append("<td></td>");
                            //}
                            str.Append("</tr>");
                        }
                    }
                    else
                    {
                        str.Append("<tr>");
                        str.Append("<td colspan='4'>No record found</td>");
                        str.Append("</tr>");
                    }
                }
                else
                {
                    str.Append("<tr>");
                    str.Append("<td colspan='4'>No record found</td>");
                    str.Append("</tr>");
                }

                str.Append("</tbody>");
                str.Append("</table>");
                str.Append("</div>");
                str.Append("<div class='modal-footer'>");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["status"].ToString().Trim() == "U")
                        {
                            str.Append("<button type='button' class='btn btn-primary' onclick='modalcall(&#39;" + dt.Rows[0]["user_name"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["applicant_name"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["entrydate"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["email"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["mobile_no"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["registration_code"].ToString().Trim() + "&#39;, &#39;" + dt.Rows[0]["scheme_code"].ToString().Trim() + "&#39;)'>Status Entry</button>");
                        }

                    }
                }

                str.Append("<button type='button' class='btn btn-danger' onclick='modal4close()'>Close</button>");
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

                            dt.Rows.Add();

                            dt.Columns.Add("doc", typeof(System.Byte[]));

                            dt.Columns.Add("doc_content_type", typeof(string));

                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            TempData["Doc"] = dt;
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

                            dt.Rows.Add();
                            //dt.Columns.Add("applicant_code", typeof(long));
                            dt.Columns.Add("doc", typeof(System.Byte[]));
                            // dt.Columns.Add("doc_type", typeof(string));
                            dt.Columns.Add("doc_content_type", typeof(string));

                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            TempData["Doc"] = dt;
                            TempData.Keep();
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
                            dt.Rows.Add();
                            // dt.Columns.Add("applicant_code", typeof(long));
                            dt.Columns.Add("doc", typeof(System.Byte[]));
                            //dt.Columns.Add("doc_type", typeof(string));
                            dt.Columns.Add("doc_content_type", typeof(string));
                            dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                            dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                            TempData["Doc"] = dt;
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
        public JsonResult Uploadfiledone(string doc_type, string registration_code, string scheme_code)
        {
            try
            {
                if (TempData["Doc"] != null)
                {
                    DataTable dt = TempData["Doc"] as DataTable;
                    string msg = new DAL.CommonDA().UpdateRegistrationDocbyAdmin(long.Parse(registration_code.Trim()), short.Parse(scheme_code.Trim()), (Byte[])(dt.Rows[0]["doc"]), doc_type.Trim(), dt.Rows[0]["doc_content_type"].ToString().Trim());
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


        public JsonResult bankUploadfiledone( string Sch, string fincode, string Code)
        {
            try
            {
                if (TempData["Doc"] != null)
                {
                    DataTable dt = TempData["Doc"] as DataTable;
                    string msg = new DAL.CommonDA().Insertbank_doc(long.Parse(Code.Trim()), short.Parse(Sch.Trim()), (Byte[])(dt.Rows[0]["doc"]), dt.Rows[0]["doc_content_type"].ToString().Trim(), long.Parse(fincode) );
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

        public JsonResult back()
        {
            string result = "../home/index";
            if (UserSession.LoggedInUserLevelId.Trim()=="6")
            {
                return Json("../Form/GetAll_MYSY_Applicant", JsonRequestBehavior.AllowGet);
            }
            if (UserSession.LoggedInUserLevelId.Trim() == "6")
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            if (UserSession.LoggedInUserLevelId.Trim() == "6")
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}