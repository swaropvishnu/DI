using CaptchaMvc.HtmlHelpers;
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
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace DI.Controllers
{
    
   
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Login");
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModal Model)
        {
            //if (this.IsCaptchaValid("Captcha is not valid"))
            if (Model.CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                string salt = CreateSalt(5);
                string usrname = Model.UserName;
                string password = Model.Password;
                DataSet ds = new DataSet();
                ds = UserDtl.VerifyUser(usrname);
                //btnlogin.Attributes.Add("onclick", "return HashPwdwithSalt('" + salt.ToString() + "');");
                if (ds != null)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                    bool isLogin = CompareHashValue(Model.Password, Model.UserName, psw, salt);
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                    {

                        string hashed_pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(psw.ToString().ToLower() + salt, "md5");
                        string userpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Model.Password.ToLower() + salt, "md5");
                        //if (hashed_pwd.ToString().ToLower().Equals(userpwd.ToString().ToLower()))
                        if (isLogin)
                        {
                            Session["tbl_Session"] = ds.Tables[0];
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
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


        #region --> Comapare HASH Value
        public static bool CompareHashValue(string password, string username, string OldHASHValue, string SALT)
        {
            try
            {
                string expectedHashString = Get_HASH_SHA512(password, SALT);
                string hashed_pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(OldHASHValue.ToLower() + SALT, "md5");
                return (hashed_pwd == expectedHashString);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region --> Generate HASH Using SHA512
        public static string Get_HASH_SHA512(string password, string _salt)
        {
            try
            {
                string type_pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(type_pwd.ToLower() + _salt, "md5");
                return type_pwd_salt;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
        private string CreateSalt(int size) //Generate the salt via Randon Number Genertor cryptography
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
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
        [ValidateAntiForgeryToken]
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

                    string pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.OldPassword_CHG.ToString(), "md5");
                    string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.NewPassword_CHG.ToString(), "md5");

                    string hashed_pwd = pwd_salt;
                    string hashed_newpwd = type_pwd_salt;


                    if ((lpsw.ToLower().Equals(hashed_newpwd.ToLower())) == true)
                    {
                        ViewBag.ErrMessage = "Your new password should not match with last old password ?";
                        return RedirectToAction("Index", "Default");
                    }

                    if ((psw.ToLower().Equals(hashed_newpwd.ToLower())) == false)
                    {
                        if (psw.ToLower().Equals(hashed_pwd.ToLower()))
                        {

                            string hashed_pwdNew = type_pwd_salt;
                            string res = UserDtl.Userpasswordchange(UserSession.LoggedInUserName, hashed_pwdNew.ToLower(), hashed_pwd.ToLower());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + res + "'); window.location='/EPDS2017/logout.aspx';", true);
                            return RedirectToAction("Index", "Default");
                        }
                        else
                        {
                            ViewBag.ErrMessage = "Your old Password is not correct ?";
                            return RedirectToAction("Index", "Default");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Your new Password mathced with old password Please change new password. ?";
                        return RedirectToAction("Index", "Default");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Default");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Invalid Username or Password.";
                return RedirectToAction("FirstTimeLogin", "Login");
            }
        }

        [SessionExpireFilterAttribute]
        public ActionResult ProfileUpdate()
        {
            return View();
        }


        public ActionResult Error()

        {
            return View();
        }        
    }
}