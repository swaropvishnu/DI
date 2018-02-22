using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DI.Models;
using DI.DAL;
using System.Data;

namespace DI.BLL
{
    public class CommonBL
    {
        //public long InsertOfficeDetailsBL(DDbind commonbo)
        //{
        //    try
        //    {
        //        CommonDA objCommonda = new CommonDA(); // Creating object of Dataccess
        //        return objCommonda.InsertOfficeDetailsDA(commonbo); // calling Method of DataAccess 
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        internal static LoginModal GetUserDetail(LoginModal objUserData)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.GetUserDetail(objUserData);
            }
            catch
            {
                throw;
            }
        }

       public static String UpdateUserDetail(LoginModal objUserData)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.UpdateUserDetail(objUserData);
            }
            catch
            {
                throw;
            }
        }

       internal static string InsertUpdateApplicationFormDetail(FormModal Objform)
       {
           try
           {
               CommonDA CommonDA = new CommonDA();
               return CommonDA.InsertUpdateApplicationFormDetail(Objform);
           }
           catch
           {
               throw;
           }
       }

        public static List<FormModal> ReportApplicationFormDetail(FormModal objform)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.ReportApplicationFormDetail(objform);
            }
            catch
            {
                throw;
            }
        }

       public static DataSet bindDropDownHn(string ProcName, string parm1, string parm2, string parm3)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.bindDropDownHn(ProcName, parm1, parm2, parm3);
            }
            catch
            {
                throw;
            }
        }
    }
}