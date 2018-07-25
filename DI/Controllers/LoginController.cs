using CaptchaMvc.HtmlHelpers;
using DI.BLL;
using DI.Filters;
using DI.Models;
using MVCCaptchaDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace DI.Controllers
{
    public class LoginController : Controller
    {
        CMYSS_Applicant CM = new CMYSS_Applicant();
        // GET: Login
        public ActionResult Login()
        {
            string salt = CreateSalt(5);
            Session["salt"] = salt.ToString();
            return View();
        }
        public ActionResult TestView()
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
                return RedirectToAction("Login", "Login");
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
        //
        // POST: /Account/Login
        [HttpPost]        
        [AllowAnonymous]
        public ActionResult Login(LoginModal Model)
        {
            
            if (Model.CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                // Ensure we have a valid viewModel to work with
                //if (!ModelState.IsValid)
                //    return View(Model);
                string salt = CreateSalt(5);
                string usrname = Model.UserName;
                string password = Model.Password;
                DataSet ds = new DataSet();
                ds = UserDtl.VerifyUser(usrname);                
                if (ds != null)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();                    
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                    {

                        string hashed_pwd = CalculateHash(psw.ToString().ToLower() + Session["salt"].ToString());
                        if (hashed_pwd.ToString().ToLower().Equals(Model.Password.ToLower()))
                        {
                            if (ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim()=="6")
                            {
                                Session["tbl_Session"] = ds.Tables[0];
                                FormsAuthentication.SetAuthCookie(usrname, Model.RememberMe);
                                return RedirectToAction("Index", "Dashboard");
                            }
                            else
                            {
                                ViewBag.ErrMessage = "Invalid Username or Password.";
                                return RedirectToAction("Login", "Login");
                            }

                        }
                        else
                        {
                            //Login Fail
                            TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                            return View(Model);
                            //return RedirectToAction("Login", "Login");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Invalid Username or Password.";
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    ViewBag.ErrMessage = "Invalid Username or Password.";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                return RedirectToAction("Login", "Login");
            }
        }
        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        /// 
        #region --> Generate HASH Using SHA512
        
        #endregion
        private string CreateSalt(int size) //Generate the salt via Randon Number Genertor cryptography
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        private string CalculateHash(string input)
        {
            using (var algorithm = SHA256.Create()) //or MD5 SHA512 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        ///////////////////////
        [SessionExpireFilterAttribute]
        public ActionResult FirstTimeLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstTimeLogin(LoginModal IstTimeLogin)
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
                    string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(IstTimeLogin.NewPassword.ToString(), "md5");
                    string hashed_pwd = IstTimeLogin.OldPassword;
                    string hashed_newpwd = type_pwd_salt;
                    if ((lpsw.ToLower().Equals(hashed_newpwd.ToLower())) == true)
                    {
                        ViewBag.ErrMessage = "Your new password should not match with last old password ?";
                        return RedirectToAction("Login", "FirstTimeLogin");
                    }
                    if ((psw.ToLower().Equals(hashed_newpwd.ToLower())) == false)
                    {
                        if (psw.ToLower().Equals(hashed_pwd.ToLower()))
                        {
                            string hashed_pwdNew = type_pwd_salt;
                            string res = UserDtl.Userpasswordchange(UserSession.LoggedInUserName, hashed_pwdNew.ToLower(), hashed_pwd.ToLower());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + res + "'); window.location='/EPDS2017/logout.aspx';", true);
                            return RedirectToAction("Login", "Login");
                        }
                        else
                        {
                            ViewBag.ErrMessage = "Your old Password is not correct ?";
                            return RedirectToAction("Login", "FirstTimeLogin");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Your new Password mathced with old password Please change new password. ?";
                        return RedirectToAction("Login", "FirstTimeLogin");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "FirstTimeLogin");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Invalid Username or Password.";
                return RedirectToAction("Login", "Login");
            }
        }
        //POST: Logout
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
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
                //return RedirectToLocal();
                return View();
            }
            catch
            {
                throw;
            }
        }
        [SessionExpireFilterAttribute]
        public ActionResult ChangePassword()
        {
            return View();
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
        [SessionExpireFilterAttribute]
        public ActionResult ProfileUpdate()
        {
            LoginModal objUserData = new LoginModal();
            objUserData.UserId =UserSession.LoggedInUserId.ToString();
            objUserData = CommonBL.GetUserDetail(objUserData);
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(objUserData.UserImage, 0);
            return View(objUserData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult ProfileUpdate(LoginModal objUserData, HttpPostedFileBase pimg)
        {
            Byte[] img = null;
            if (pimg != null && pimg.ContentLength > 0)
            {   /*****IMG-DB-CODE******/
                int FileSize = pimg.ContentLength;
                img = new Byte[FileSize];
                pimg.InputStream.Read(img, 0, FileSize);
                objUserData.UserImage = img;
            }
            objUserData.UserId = UserSession.LoggedInUserId.ToString();

           string A = CommonBL.UpdateUserDetail(objUserData);
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(objUserData.UserImage, 0);
            if (A=="Success")
            {
                ViewBag.AlertUpdate = "Update SuccessFully..";
            }
            else
            {
                ViewBag.AlertUpdate = "Failed To Update Profile..";
            }
            return View(objUserData);
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult RegistrationLogin()
        {
            return View();
        }
        public ActionResult Registration_Login()
        {
            string salt = CreateSalt(5);
            Session["salt"] = salt.ToString();
            List<SelectListItem> Scheme = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Scheme, "y", "", "");
            CM.Scheme = Scheme;
            return View(CM);
        }

        public JsonResult InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform)
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
                Objform.steps = "0";
                string[] dobTime = Objform.inputdob.ToString().Split(' ');
                string[] dob = dobTime[0].Split('/');
                //string Mobchar = Objform.mobile_no.Substring(0, 2);
                List<CMYSS_Applicant_Doc> objdoc = new List<CMYSS_Applicant_Doc>();
                List<CMYSS_Applicant_Family> objFamily = new List<CMYSS_Applicant_Family>();
                Objform.Password= FormsAuthentication.HashPasswordForStoringInConfigFile(dob[2].ToString().Trim()+ dob[1].ToString().Trim()+ dob[0].ToString().Trim(), "sha256");
                string str = new DAL.CommonDA().InsertUpdateCMYSS_Applicant(Objform, objdoc, objFamily, "1");
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
        public JsonResult Applicant_Login(LoginModal Model)
        {
            if (Model.CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                string salt = CreateSalt(5);
                string usrname = Model.UserName;
                string password = Model.Password;
                DataSet ds = new DataSet();
                ds = UserDtl.VerifyApplicant(usrname,Model.yojana_code);
                if (ds != null)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                    {
                        string hashed_pwd = CalculateHash(psw.ToString().ToLower() + Session["salt"].ToString());
                        if (hashed_pwd.ToString().ToLower().Equals(Model.Password.ToLower()))
                        {
                            if (ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim() == "30")
                            {
                                Session["tbl_Session"] = ds.Tables[0];
                                FormsAuthentication.SetAuthCookie(usrname, Model.RememberMe);
                                if (ds.Tables[0].Rows[0]["scheme_code"].ToString() == "1")
                                {
                                    return Json("1", JsonRequestBehavior.AllowGet);
                                    
                                }
                                else if (ds.Tables[0].Rows[0]["scheme_code"].ToString() == "5")
                                {
                                    return Json("5", JsonRequestBehavior.AllowGet);

                                }
                                else
                                {
                                    return Json("Invalid Username or Password.", JsonRequestBehavior.AllowGet);
                                }
                               // return Json("Sucess", JsonRequestBehavior.AllowGet);
                                //return RedirectToAction("CMYSS_Applicant", "User");
                            }
                            else
                            {
                                ViewBag.ErrMessage = "Invalid Username or Password.";
                                return Json("Invalid Username or Password.", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            //Login Fail
                            TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                            return Json("Access Denied! Wrong Credential", JsonRequestBehavior.AllowGet);
                            //return RedirectToAction("Login", "Login");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Invalid Username or Password.";
                        return Json("Invalid Username or Password.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    ViewBag.ErrMessage = "Invalid Username or Password.";
                    return Json("Invalid Username or Password.", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                return Json("Error: captcha is not valid.", JsonRequestBehavior.AllowGet);
            }
        }
    }
}