using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DI.BLL;
using DI.Models;


namespace DI.DAL
{
    public class CommonDA
    {
        SqlDataAdapter adap;
        SqlCommand cmd;
        DataTable dt;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

        internal LoginModal GetUserDetail(LoginModal objUserData)
        {
            LoginModal fm = new LoginModal();
            try
            {
                con.Open();
                cmd = new SqlCommand("proc_GetUserMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", SqlDbType.Int).Value = objUserData.UserId;
                adap = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adap.Fill(dt);
                cmd.Dispose();
                if (dt!=null && dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fm.UserId = objUserData.UserId;
                        fm.UserName = dt.Rows[i]["Name"].ToString();
                        fm.UserMobile = dt.Rows[i]["MobileNo"].ToString();
                        fm.UserNameHindi = dt.Rows[i]["NameHindi"].ToString();
                        fm.UserEmail = dt.Rows[i]["EmailId"].ToString();
                        fm.UserAddress = dt.Rows[i]["Office_Add"].ToString();
                        fm.UserImage = (Byte[])dt.Rows[i]["PhotoContent"]; 
                    }
                   

                }
                return fm;

            }
            catch
            {
                throw;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
        }

      internal  String UpdateUserDetail(LoginModal objUserData)
        {
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("proc_UpdateUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", SqlDbType.Int).Value = objUserData.UserId;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = objUserData.UserName;
                cmd.Parameters.AddWithValue("@UserHindiName", SqlDbType.NVarChar).Value = objUserData.UserNameHindi;
                cmd.Parameters.AddWithValue("@UserMobile", SqlDbType.VarChar).Value = objUserData.UserMobile;
                cmd.Parameters.AddWithValue("@UserEmail", SqlDbType.VarChar).Value = objUserData.UserEmail;
                cmd.Parameters.AddWithValue("@UserAddress", SqlDbType.VarChar).Value = objUserData.UserAddress;
                cmd.Parameters.AddWithValue("@UserProfileImage", SqlDbType.VarBinary).Value = objUserData.UserImage;
                cmd.ExecuteNonQuery();
                result = "Success";

            }
            catch
            {
                result = "Failed";
                throw;
               
            }
            finally
            {

                con.Close();
                con.Dispose();
            }

            return result;
        }

        //public long InsertOfficeDetailsDA(DDbind objCommonda)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("Proc_addoffice", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = objCommonda.SocietyName;
        //        cmd.Parameters.AddWithValue("@dist_code", SqlDbType.Int).Value = objCommonda.RegNo ;
        //        cmd.Parameters.AddWithValue("@depart_code", SqlDbType.Int).Value = objCommonda.Department;
        //        cmd.Parameters.AddWithValue("@office_type", SqlDbType.NVarChar).Value = objCommonda.dateoflasteletion ;
        //        cmd.Parameters.AddWithValue("@OfficeCode", SqlDbType.NVarChar).Value = objCommonda.DistrictNames ;

        //        cmd.Parameters.AddWithValue("@office_name", SqlDbType.NVarChar).Value = objCommonda.TehsilName ;
        //        /*********salman 31-08-2106**********/
        //        cmd.Parameters.AddWithValue("@office_dist", SqlDbType.NVarChar).Value = objCommonda.RegNo ;
        //        /***********end **********/
        //        cmd.Parameters.AddWithValue("@office_add", SqlDbType.NVarChar).Value = objCommonda.Address ;
        //        cmd.Parameters.AddWithValue("@paycom", SqlDbType.Int).Value = objCommonda.BlockName ;
        //        cmd.Parameters.AddWithValue("@exept_reason", SqlDbType.NVarChar).Value = objCommonda.VillageName;
        //        cmd.Parameters.AddWithValue("@delete_status", SqlDbType.Bit).Value = objCommonda.pin ;
        //        cmd.Parameters.AddWithValue("@exempted", SqlDbType.NVarChar).Value = objCommonda.Phone ;

        //        //cmd.Parameters.AddWithValue("@head1_name", SqlDbType.NVarChar).Value = objCommonda.HeadName1;//head1_name, 
        //        //cmd.Parameters.AddWithValue("@head1_email", SqlDbType.NVarChar).Value = objCommonda.H1email;
        //        ////cmd.Parameters.AddWithValue("@head1_gender", SqlDbType.NVarChar).Value = objCommonda.H1gender;
        //        //cmd.Parameters.AddWithValue("@head1_desig_code", SqlDbType.Int).Value = objCommonda.HeadDesigCode1;
        //        //cmd.Parameters.AddWithValue("@head1_desig", SqlDbType.NVarChar).Value = objCommonda.HeadDesigName1;
        //        //cmd.Parameters.AddWithValue("@head1_oph", SqlDbType.NVarChar).Value = objCommonda.Head1Mobile2;
        //        //cmd.Parameters.AddWithValue("@head1_mob", SqlDbType.NVarChar).Value = objCommonda.Head1Mobile1;

        //        //cmd.Parameters.AddWithValue("@head2_name", SqlDbType.NVarChar).Value = objCommonda.HeadName2;
        //        //cmd.Parameters.AddWithValue("@head2_email", SqlDbType.NVarChar).Value = objCommonda.H2email;
        //        ////cmd.Parameters.AddWithValue("@head2_gender", SqlDbType.NVarChar).Value = objCommonda.H2gender;
        //        //cmd.Parameters.AddWithValue("@head2_desig_code", SqlDbType.Int).Value = objCommonda.HeadDesigCode2;
        //        //cmd.Parameters.AddWithValue("@head2_desig", SqlDbType.NVarChar).Value = objCommonda.HeadDesigName2;
        //        //cmd.Parameters.AddWithValue("@head2_oph", SqlDbType.NVarChar).Value = objCommonda.Head2Mobile2;
        //        //cmd.Parameters.AddWithValue("@head2_mob", SqlDbType.NVarChar).Value = objCommonda.Head2Mobile1;

        //        //cmd.Parameters.AddWithValue("@nop_total", SqlDbType.Int).Value = objCommonda.TotalPP;
        //        //cmd.Parameters.AddWithValue("@nop_officer", SqlDbType.Int).Value = objCommonda.TotalOfficer;
        //        //cmd.Parameters.AddWithValue("@nop_personal", SqlDbType.Int).Value = objCommonda.TotalEmp;
        //        //cmd.Parameters.AddWithValue("@nop_female", SqlDbType.Int).Value = objCommonda.totFemale;
        //        //cmd.Parameters.AddWithValue("@TotMale", SqlDbType.Int).Value = objCommonda.totMale;

        //        //cmd.Parameters.AddWithValue("@remarks", SqlDbType.NVarChar).Value = objCommonda.Remarks;

        //        ////cmd.Parameters.AddWithValue("@freeze_status", SqlDbType.Int).Value = objCommonda.freezestat;
        //        //cmd.Parameters.AddWithValue("@ipaddress", SqlDbType.VarChar).Value = objCommonda.IPAddress;
        //        ////cmd.Parameters.AddWithValue("@entrystamp", SqlDbType.NVarChar).Value = objCommonda.TimeStamp;

        //        //cmd.Parameters.AddWithValue("@driver", SqlDbType.Int).Value = objCommonda.Driver;
        //        //cmd.Parameters.AddWithValue("@maternityleave", SqlDbType.Int).Value = objCommonda.MaternityLeave;
        //        //cmd.Parameters.AddWithValue("@handicaped", SqlDbType.Int).Value = objCommonda.Handicaped;
        //        //cmd.Parameters.AddWithValue("@sweeper", SqlDbType.Int).Value = objCommonda.Sweeper;
        //        //cmd.Parameters.AddWithValue("@watchman", SqlDbType.Int).Value = objCommonda.Watchman;


        //        SqlParameter pvNewId = new SqlParameter();
        //        pvNewId.ParameterName = "@pOutPut";
        //        pvNewId.DbType = DbType.String;
        //        pvNewId.Size = 500;
        //        pvNewId.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(pvNewId);
        //        con.Open();
        //        Int64 Result1 = cmd.ExecuteNonQuery();
        //        Int64 Result = Convert.ToInt64(cmd.Parameters["@pOutPut"].Value.ToString());
        //        cmd.Dispose();
        //        return Result;

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {

        //        con.Close();
        //        con.Dispose();
        //        //ObjBO = null;
        //    }
        //}
    }
}