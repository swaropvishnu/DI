using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DI.Controllers
{
    public class UserController : Controller
    {
        public ActionResult AllotmentPlot()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult GetCMSY_ApplicationPrint()
        {
            return View();
        }
        public ActionResult PlotApplicantWizard()
        {
            return View();
        }
        public ActionResult PlantAndMachinery_EntryForm()
        {
            return View();
        }
        //CMYSS_Applicant ad = new CMYSS_Applicant(); PlantAndMachinery_EntryForm
        public ActionResult CMYS_SchemeEntryForm()
        {
            CMYSS_Applicant CM = new CMYSS_Applicant();
            try
            {
                if (@UserSession.LoggedInUserName != null)
                {
                    DataSet ds = new DAL.CommonDA().GetApplicantinfo(-1, -1, @UserSession.LoggedInUserName);
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
                            CM.bank_name = ds.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                            CM.self_share = decimal.Parse(ds.Tables[0].Rows[0]["self_share"].ToString().Trim());
                            CM.deposit_amt = decimal.Parse(ds.Tables[0].Rows[0]["deposit_amt"].ToString().Trim());
                            CM.branch_name = ds.Tables[0].Rows[0]["branch_name"].ToString().Trim();
                            CM.bank_account_no = ds.Tables[0].Rows[0]["bank_account_no"].ToString().Trim();
                            CM.ifsc_code = ds.Tables[0].Rows[0]["ifsc_code"].ToString().Trim();
                            CM.DistCode= int.Parse( ds.Tables[0].Rows[0]["district_code_census"].ToString().Trim());
                            CM.TehsilCode  = int.Parse( ds.Tables[0].Rows[0]["tehsil_code_census"].ToString().Trim());
                            CM.BlockCode = int.Parse(ds.Tables[0].Rows[0]["block_code"].ToString().Trim()); ;
                            CM.VillCode = int.Parse(ds.Tables[0].Rows[0]["village_code"].ToString().Trim());
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                if (ds.Tables[1].Rows[0]["relation_code"].ToString().Trim() == "F")
                                {
                                    CM.Father_Name = ds.Tables[1].Rows[0]["person_name"].ToString().Trim();
                                    CM.Father_Age = int.Parse(ds.Tables[1].Rows[0]["age"].ToString().Trim());
                                    CM.Father_work = ds.Tables[1].Rows[0]["workingfield"].ToString().Trim();
                                }
                                if (ds.Tables[1].Rows[0]["relation_code"].ToString().Trim() == "M")
                                {
                                    CM.Mother_Name = ds.Tables[0].Rows[0]["person_name"].ToString().Trim();
                                    CM.Mother_Age = int.Parse(ds.Tables[0].Rows[0]["age"].ToString().Trim());
                                    CM.Mother_work = ds.Tables[0].Rows[0]["workingfield"].ToString().Trim();
                                }
                                if (ds.Tables[1].Rows[0]["relation_code"].ToString().Trim() == "R")
                                {
                                    CM.brother_sister_Name = ds.Tables[0].Rows[0]["person_name"].ToString().Trim();
                                    CM.brother_sister_Age = int.Parse(ds.Tables[0].Rows[0]["age"].ToString().Trim());
                                    CM.brother_sister_Work = ds.Tables[0].Rows[0]["workingfield"].ToString().Trim();
                                }
                                if (ds.Tables[1].Rows[0]["relation_code"].ToString().Trim() == "N")
                                {
                                    CM.children_Name = ds.Tables[0].Rows[0]["person_name"].ToString().Trim();
                                    CM.children_Age = int.Parse(ds.Tables[0].Rows[0]["age"].ToString().Trim());
                                    CM.children_Work = ds.Tables[0].Rows[0]["workingfield"].ToString().Trim();
                                }
                            }
                            CM.manufacturing = ds.Tables[0].Rows[0]["manufacturing"].ToString().Trim();
                            CM.services = ds.Tables[0].Rows[0]["services"].ToString().Trim();
                            CM.steps = ds.Tables[0].Rows[0]["steps"].ToString().Trim();
                        }
                    }
                }
                return View(CM);
            }
            catch (Exception)
            {
                return View(CM);
            }
        }
        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Doc> objdoc, List<CMYSS_Applicant_Family> objFamily, bool @sptype)
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

                string str = new DAL.CommonDA().InsertUpdateCMYSS_Applicant(Objform, objdoc, objFamily, @sptype);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertUpdateCMYSS_Applicantfamily(List<CMYSS_Applicant_Family> objFamily)
        {
            try
            {
                string str = new DAL.CommonDA().InsertUpdateCMYSS_Applicantfamily(objFamily);
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
            string doc_cat = "";
            string doctype = "";
            string applicant = "";
            //HttpFileCollectionBase files, string applicant, string doctype, string doc_cat
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
                                    dt.Columns.Add("applicant_code",typeof (long));
                                    dt.Columns.Add("doc" ,typeof(System.Byte[]));
                                    dt.Columns.Add("doc_type",typeof(string));
                                    dt.Columns.Add("doc_content_type",typeof(string));

                                }
                                else
                                {
                                    dt = (DataTable)Session["Doc"];
                                    dt.Rows.Add();
                                }
                                dt.Rows[dt.Rows.Count - 1]["doc"] = img;
                                dt.Rows[dt.Rows.Count - 1]["doc_content_type"] = ext[1];
                                Session["Doc"] = dt;
                                return Json("files upload");

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
            }
            else
            {
                return Json("No files selected.");
            }
        }
        //InsertUpdatedoc
        //[HttpPost]
        //public ActionResult UploadPdf()
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        try
        //        {
        //            string fname = "";
        //            string fname2 = "";
        //            HttpFileCollectionBase files = Request.Files;
        //            for (int i = 0; i < files.Count; i++)
        //            {
        //               // string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
        //                //string filename = Path.GetFileName(Request.Files[i].FileName);
        //                HttpPostedFileBase file = files[i];
        //                byte[] doc= new byte[file.ContentLength];
        //                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //                {
        //                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //                    fname = testfiles[testfiles.Length - 1];
        //                    fname2 = testfiles[testfiles.Length - 1];
        //                }
        //                else
        //                {
        //                    fname = file.FileName;
        //                    fname2 = file.FileName;
        //                }
        //                string[] ext = fname.Split('.');
        //                if (ext[1].ToString().Trim() == "pdf" || ext[1].ToString().Trim() == "docx" || ext[1].ToString().Trim() == "doc" || ext[1].ToString().Trim() == "xls" || ext[1].ToString().Trim() == "jpg" || ext[1].ToString().Trim() == "jpeg" || ext[1].ToString().Trim() == "png" || ext[1].ToString().Trim() == "xlsx")
        //                {
        //                    file.InputStream.Read(doc, 0, file.ContentLength);
        //                    //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
        //                    //file.SaveAs(fname);
        //                }
        //                else
        //                {
        //                    return Json("only .pdf/.docx /.doc/.xls/.xlsx/.jpg/jpeg/.png  files upload");
        //                }
        //            }
        //            return Json("File Uploaded Successfully!" + "~/Uploads/" + fname2.Trim());
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json("Error occurred. Error details: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Json("No files selected.");
        //    }
        //}
        public JsonResult Uploadfiledone(string doc_type, string applicant_code)
        {
            DataTable dt = (DataTable)Session["Doc"];
            dt.Rows[dt.Rows.Count - 1]["doc_type"] = doc_type;
            dt.Rows[dt.Rows.Count - 1]["applicant_code"] = applicant_code;
            return Json("Record Save", JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertUpdatedoc2()
        {
            try
            {
                if (Session["Doc"]!=null)
                {
                    DataTable dt = (DataTable)Session["Doc"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count ==7)
                        {
                            string msg = new DAL.CommonDA().InsertUpdateCMYSS_Applicantdoc2(dt);
                            if (msg.Contains("Save"))
                            {
                                Session["Doc"] = null;
                            }
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Please upload all documents", JsonRequestBehavior.AllowGet);
                        }
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

        }

        public ActionResult GetDistrict()
        {
            List<SelectListItem> distNames = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", distNames, "ds", "28", "10");
            return Json(distNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getplot(int EstateCode)
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
                        str.Append("<td>"+(i+1)+"</td>");
                        str.Append("<td>"+ dt.Rows[i]["PlotSerial"].ToString().Trim() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Plot_Area"].ToString().Trim() + "</td>");
                        str.Append("<td><input id='Checkbox"+ (i + 1) + "' type='checkbox' onclick='funplot(&#39;" + dt.Rows[i]["PlotSerial"].ToString().Trim() + "&#39;,&#39;Checkbox" + (i + 1) + "&#39;)' /></td>");
                        str.Append("</tr>");
                    }
                }
            }
            return Json(str.ToString().Trim(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult certificate_Letter()
        {
            return View();
        }
        public ActionResult Affidavit_Letter()
        {
            return View();
        }
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
        public ActionResult GetZone()
        {
            List<SelectListItem> Zone = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Zone, "Z", "-1", "");
            return Json(Zone, JsonRequestBehavior.AllowGet);
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
                objApp.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(dob[2].ToString().Trim() + dob[1].ToString().Trim()+ dob[0].ToString().Trim()+"234", "sha256");
                string str = new DAL.CommonDA().Plot_Applicant(objER, objRP, objApp,false);
                if (str.Contains("Save") && str.Contains("/"))
                {
                    string[] Msg = str.Split('/');
                    return Json(Msg[0] + "\n" + "User Name :" + Msg[1] + "\n" + "Password :" + dob[2].ToString().Trim() + dob[1].ToString().Trim()  + dob[0].ToString().Trim(), JsonRequestBehavior.AllowGet);
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

        }
    }
}