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
            CommonDA CommonDA = new CommonDA();
            return CommonDA.GetUserDetail(objUserData);
        }

       public static String UpdateUserDetail(LoginModal objUserData)
        {
            CommonDA CommonDA = new CommonDA();
            return CommonDA.UpdateUserDetail(objUserData);
        }
    }
}