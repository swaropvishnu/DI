using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DI.BLL;
using DI.Models;
using Microsoft.ApplicationBlocks.Data;

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

        public List<FormModal> ReportApplicationFormDetail(FormModal objform)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Proc_InsertUpdateApplicationFormDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", SqlDbType.VarChar).Value = objform.Mode;
                List<FormModal> Obj = new List<FormModal>();
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FormModal objdd = new FormModal();
                    objdd.ApplicantName = reader["ApplicantName"].ToString();
                    objdd.ApplicantAddress = reader["ApplicantAddress"].ToString();
                    objdd.IndrustryName = reader["IndrustryName"].ToString();
                    objdd.ApplicationFee = Convert.ToDecimal(reader["ApplicationFee"]);
                    objdd.ApplicationFeeDate = Convert.ToDateTime(reader["ApplicationFeeDate"]);
                    objdd.ApplicationFeedetails = reader["ApplicationFeedetails"].ToString();
                    objdd.ProductName= reader["ProductName"].ToString(); 
                    objdd.FiananceDetails = reader["FiananceDetails"].ToString();
                    objdd.RowMeterialSource = reader["RowMeterialSource"].ToString();
                    objdd.PracticalProjectReport = reader["PracticalProjectReport"].ToString();
                    objdd.HelpFromForegin= reader["HelpFromForegin"].ToString();
                    Obj.Add(objdd);
                }
                reader.Close();
                con.Close();
                cmd.Dispose();
                return Obj;
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

        public DataSet bindDropDownHn(string procName, string parm1, string parm2, string parm3)
        {
            DataSet ds = new DataSet();
            try
            {
               
                if (parm1.Length > 0 && parm1 != "")
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@Parm1", parm1));
                    if (parm2.Length > 0 && parm2 != "")
                        parameters.Add(new SqlParameter("@Parm2", parm2));
                    if (parm3.Length > 0 && parm3 != "")
                        parameters.Add(new SqlParameter("@Parm3", parm3));
                   
                    ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, procName, parameters.ToArray());
                }
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
            return ds;
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

  

      internal  string InsertUpdateApplicationFormDetail(FormModal Objform)
      {
          string result = "";
          try
          {
              con.Open();
              cmd = new SqlCommand("Proc_InsertUpdateApplicationFormDetail", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@UserId", SqlDbType.BigInt).Value = Objform.UserId;
              cmd.Parameters.AddWithValue("@ApplicantName", SqlDbType.NVarChar).Value = Objform.ApplicantName;
              cmd.Parameters.AddWithValue("@ApplicantAddress", SqlDbType.NVarChar).Value = Objform.ApplicantAddress;
              cmd.Parameters.AddWithValue("@IndrustryName", SqlDbType.NVarChar).Value = Objform.IndrustryName;
              cmd.Parameters.AddWithValue("@ApplicationFee", SqlDbType.Decimal).Value = Objform.ApplicationFee;
               cmd.Parameters.AddWithValue("@ApplicationFeeDate", SqlDbType.DateTime).Value = Objform.ApplicationFeeDate;
              cmd.Parameters.AddWithValue("@ApplicationFeedetails", SqlDbType.NVarChar).Value = Objform.ApplicationFeedetails;
              cmd.Parameters.AddWithValue("@IsPreRegistered", SqlDbType.Bit).Value = Objform.IsPreRegistered;
               if (Objform.IsPreRegistered == false)
               {
                    cmd.Parameters.AddWithValue("@OldRegistraionNo", SqlDbType.BigInt).Value = null;
                    cmd.Parameters.AddWithValue("@OldRegistrationDate", SqlDbType.DateTime).Value = null;

                }
                else
                {
                    cmd.Parameters.AddWithValue("@OldRegistraionNo", SqlDbType.BigInt).Value = Objform.OldRegistraionNo;
                    cmd.Parameters.AddWithValue("@OldRegistrationDate", SqlDbType.DateTime).Value = Objform.OldRegistrationDate;
                }
             
              cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = Objform.ProductName;
              cmd.Parameters.AddWithValue("@FiananceDetails", SqlDbType.NVarChar).Value = Objform.FiananceDetails;
              cmd.Parameters.AddWithValue("@RowMeterialSource", SqlDbType.NVarChar).Value = Objform.RowMeterialSource;
              cmd.Parameters.AddWithValue("@PracticalProjectReport", SqlDbType.VarChar).Value = Objform.PracticalProjectReport;
              cmd.Parameters.AddWithValue("@HelpFromForegin", SqlDbType.NVarChar).Value = Objform.HelpFromForegin;

              cmd.Parameters.AddWithValue("@Mode", SqlDbType.VarChar).Value = Objform.Mode;
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
    }
}