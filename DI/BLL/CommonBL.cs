using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DI.Models;
using DI.DAL;

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
    }
}