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

        //internal string Details_Established_industrialDal(IndustrialAreaMasterModal Objform)
        //{
        //    string result = "";
        //    try
        //    {
        //        con.Open();
        //        cmd = new SqlCommand("Proc_InsertUpdate_Established_industrial", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@DistName", SqlDbType.NVarChar).Value = Objform.DistrictNames;
        //        cmd.Parameters.AddWithValue("@Established_industrial", SqlDbType.NVarChar).Value = Objform.Established_industrial;
        //        // cmd.Parameters.AddWithValue("@Indrustry_start_year", SqlDbType.DateTime).Value = Objform.Indrustry_start_year;  
        //        cmd.Parameters.AddWithValue("@Area", SqlDbType.Decimal).Value = Objform.Area;
        //        cmd.Parameters.AddWithValue("@Currentprevailingrate", SqlDbType.NVarChar).Value = Objform.Currentprevailingrate;
        //        cmd.Parameters.AddWithValue("@UserCurrentlynumbertotaldevelopedlandareaImage", SqlDbType.NVarChar).Value = Objform.UserCurrentlynumbertotaldevelopedlandareaImage;
        //        cmd.Parameters.AddWithValue("@Currentlynumbertotaldevelopedlandarea", SqlDbType.VarChar).Value = Objform.Currentlynumbertotaldevelopedlandarea;
        //        cmd.Parameters.AddWithValue("@Details_Each_plot_Available_Area", SqlDbType.VarChar).Value = Objform.Details_Each_plot_Availablenoncontroversial_Available_Area;
        //        cmd.Parameters.AddWithValue("@Accessibility_location_available_not", SqlDbType.VarChar).Value = Objform.Accessibility_location_available_not;
        //        cmd.Parameters.AddWithValue("@Whether_electricity_Available_not", SqlDbType.VarChar).Value = Objform.Whether_electricity_Available_not;
        //        cmd.Parameters.AddWithValue("@Whether_Drainage_facility_Available", SqlDbType.VarChar).Value = Objform.Whether_Drainage_facility_Available;
        //        cmd.Parameters.AddWithValue("@totaldevelopedlandarea_Number_sheds", SqlDbType.Int).Value = Objform.totaldevelopedlandarea_Number_sheds;
        //        cmd.Parameters.AddWithValue("@No_area_developed_plots", SqlDbType.Int).Value = Objform.No_area_developed_plots;
        //        cmd.Parameters.AddWithValue("@area_developed_plots_Number_sheds", SqlDbType.Int).Value = Objform.area_developed_plots_Number_sheds;
        //        cmd.Parameters.AddWithValue("@Number_Allocated_plots", SqlDbType.Int).Value = Objform.Number_Allocated_plots;
        //        cmd.Parameters.AddWithValue("@Allocated_plotsNumber_sheds", SqlDbType.Int).Value = Objform.Allocated_plotsNumber_sheds;
        //        cmd.Parameters.AddWithValue("@NodisputedAvailable_empty_plots_allotment", SqlDbType.Int).Value = Objform.NodisputedAvailable_empty_plots_allotment;
        //        cmd.Parameters.AddWithValue("@Access_distancenet_bus_stand", SqlDbType.Int).Value = Objform.Access_distancenet_bus_stand;
        //        cmd.Parameters.AddWithValue("@Distancekilometers_station", SqlDbType.Int).Value = Objform.Distancekilometers_station;
        //        cmd.Parameters.AddWithValue("@Whether_drinking_water_system_available", SqlDbType.VarChar).Value = Objform.Whether_drinking_water_system_available;
        //        cmd.Parameters.AddWithValue("@Whether_material_available", SqlDbType.VarChar).Value = Objform.Whether_material_available;
        //        cmd.Parameters.AddWithValue("@Whether_industrial_park_available", SqlDbType.VarChar).Value = Objform.Whether_industrial_park_available;

        //        cmd.Parameters.AddWithValue("@Mode", SqlDbType.VarChar).Value = Objform.Mode;
        //        cmd.ExecuteNonQuery();
        //        result = "Success";

        //    }
        //    catch
        //    {
        //        result = "Failed";
        //        throw;

        //    }
        //    finally
        //    {

        //        con.Close();
        //        con.Dispose();
        //    }

        //    return result;
        //}

        #region Industrial
        public string InsertLoc(IndustrialAreaMasterModal m06, bool IsDel)
        {
            string message = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdateIndustrialArea]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("IndustrialAreaCode", m06.IndustrialAreaCode);
                cmd.Parameters.AddWithValue("IndustrialAreaName", m06.IndustrialAreaName);
                cmd.Parameters.AddWithValue("Establishment", m06.Establishment);
                cmd.Parameters.AddWithValue("Address", m06.Address);
                cmd.Parameters.AddWithValue("DisID", m06.tbl_DistrictID);
                cmd.Parameters.AddWithValue("TehSilID", m06.tbl_TehsilID);
                cmd.Parameters.AddWithValue("BlocID", m06.tbl_BlockID);
                cmd.Parameters.AddWithValue("ThanaID", m06.tbl_ThanaID);
                cmd.Parameters.AddWithValue("PinCode", m06.PinCode);
                cmd.Parameters.AddWithValue("@AreaPerSqfeet", m06.DistancePerSqFeet);
                cmd.Parameters.AddWithValue("@DistancePerSqFeet", m06.DistancePerSqFeet);
                cmd.Parameters.AddWithValue("PlotNo", m06.PlotNo);
                cmd.Parameters.AddWithValue("ShadeNo", m06.ShadeNo);
                cmd.Parameters.AddWithValue("NearestRailwayStationKm", m06.NearestRailwayStationKm);
                cmd.Parameters.AddWithValue("NearestBusStationKM", m06.NearestBusStationKM);
                cmd.Parameters.AddWithValue("ISStreet", m06.IsStreet);
                cmd.Parameters.AddWithValue("IsElectriccity", m06.IsElectriccity);
                cmd.Parameters.AddWithValue("IsDrainage", m06.IsDrainage);
                cmd.Parameters.AddWithValue("IsDrinkingWater", m06.IsDrinkingWater);
                cmd.Parameters.AddWithValue("IsRawMaterialsSiding", m06.IsRawMaterialsSiding);
                cmd.Parameters.AddWithValue("IsIndustrialPark", m06.IsIndustrialPark);
                cmd.Parameters.AddWithValue("CUserID", "");
                cmd.Parameters.AddWithValue("@CUserIP", "");
                cmd.Parameters.AddWithValue("@CMac", "");
                cmd.Parameters.AddWithValue("@UMac", "");
                cmd.Parameters.AddWithValue("@UUserID", "");
                cmd.Parameters.AddWithValue("@UUserIP", "");
                cmd.Parameters.AddWithValue("IsDel", IsDel);//sptype
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = cmd.Parameters["Msg"].Value.ToString();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }
        public string InsertUpdatePlot(AddPlot AP, bool IsDel)
        {
            string message = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdatePlote]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("PlotCode", AP.PlotCode);
                cmd.Parameters.AddWithValue("tbl_IndustrialAreaCode", AP.tbl_IndustrialAreaCode);
                cmd.Parameters.AddWithValue("PlotSerial", AP.PlotSerial);
                cmd.Parameters.AddWithValue("PlotName", AP.PlotName);
                cmd.Parameters.AddWithValue("@PlotArea", AP.PlotArea);
                cmd.Parameters.AddWithValue("IsPlot_Assigned", AP.IsPlot_Assigned);
                cmd.Parameters.AddWithValue("IsPlot_Disputed", AP.IsPlot_Disputed);
                cmd.Parameters.AddWithValue("Plot_Desc", AP.Plot_Desc);
                cmd.Parameters.AddWithValue("Plot_Allottee_Desc", AP.Plot_Allottee_Desc);
                cmd.Parameters.AddWithValue("IsDel", IsDel);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = cmd.Parameters["Msg"].Value.ToString();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }
        public DataTable Getplot(int IndustrialAreaCode, long PlotCode, string PlotSerialNo, string PlotName, DateTime FromDate, DateTime ToDate, string IsPlot_Disputed, string IsPlot_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlot]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialAreaCode", IndustrialAreaCode);
                cmd.Parameters.AddWithValue("PlotCode", PlotCode);
                cmd.Parameters.AddWithValue("PlotSerialNo", PlotSerialNo);
                cmd.Parameters.AddWithValue("PlotName", PlotName);
                cmd.Parameters.AddWithValue("FromDate", FromDate);
                cmd.Parameters.AddWithValue("ToDate", ToDate);
                cmd.Parameters.AddWithValue("IsPlot_Disputed", IsPlot_Disputed);
                cmd.Parameters.AddWithValue("IsPlot_Assigned", IsPlot_Assigned);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            con.Close();
            return dt;
        }

        public DataTable GetShed(int IndustrialAreaCode, long @ShedCode, string @ShedSerialNo, string @ShedName, DateTime FromDate, DateTime ToDate, string @IsShed_Disputed, string @IsShed_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetShed]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialAreaCode", IndustrialAreaCode);
                cmd.Parameters.AddWithValue("@ShedCode", @ShedCode);
                cmd.Parameters.AddWithValue("@ShedSerialNo", @ShedSerialNo);
                cmd.Parameters.AddWithValue("@ShedName", @ShedName);
                cmd.Parameters.AddWithValue("FromDate", FromDate);
                cmd.Parameters.AddWithValue("ToDate", ToDate);
                cmd.Parameters.AddWithValue("IsShed_Disputed", IsShed_Disputed);
                cmd.Parameters.AddWithValue("IsShed_Assigned", IsShed_Assigned);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            con.Close();
            return dt;
        }

        public string InsertUpdateShed(AddShed AS, bool IsDel)
        {
            string message = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdateShed]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("ShedCode", AS.ShedCode);
                cmd.Parameters.AddWithValue("tbl_IndustrialAreaCode", AS.tbl_IndustrialAreaCode);
                cmd.Parameters.AddWithValue("ShedSerial", AS.ShedSerial);
                cmd.Parameters.AddWithValue("ShedName", AS.ShedName);
                cmd.Parameters.AddWithValue("ShedArea", AS.ShedArea);
                cmd.Parameters.AddWithValue("IsShed_Assigned", AS.IsShed_Assigned);
                cmd.Parameters.AddWithValue("IsShed_Disputed", AS.IsShed_Disputed);
                cmd.Parameters.AddWithValue("Shed_Desc", AS.Shed_Desc);
                cmd.Parameters.AddWithValue("Shed_Allottee_Desc", AS.Shed_Allottee_Desc);
                cmd.Parameters.AddWithValue("IsDel", IsDel);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = cmd.Parameters["Msg"].Value.ToString();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }

        public DataTable GetIndustrialAreaInfo(int @IndustrialAreaCode, string @IndustrialAreaName, int @DistrictID, int @TehSilID, int @BlocID, int @ThanaID, string @PinCode, string @PlotNo, string @ShadeNo, DateTime @FromDate, DateTime @ToDate, DateTime @FromEstablishment, DateTime @ToEstablishment)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetIndustrialAreaInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialAreaCode", IndustrialAreaCode);
                cmd.Parameters.AddWithValue("@IndustrialAreaName", @IndustrialAreaName);
                cmd.Parameters.AddWithValue("@DistrictID", @DistrictID);
                cmd.Parameters.AddWithValue("@TehSilID", @TehSilID);
                cmd.Parameters.AddWithValue("@BlocID", @BlocID);
                cmd.Parameters.AddWithValue("@ThanaID", @ThanaID);
                cmd.Parameters.AddWithValue("@PinCode", @PinCode);
                cmd.Parameters.AddWithValue("@PlotNo", @PlotNo);
                cmd.Parameters.AddWithValue("@ShadeNo", @ShadeNo);
                cmd.Parameters.AddWithValue("@FromDate", @FromDate);
                cmd.Parameters.AddWithValue("@ToDate", @ToDate);
                cmd.Parameters.AddWithValue("@FromEstablishment", @FromEstablishment);
                cmd.Parameters.AddWithValue("@ToEstablishment", @ToEstablishment);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            con.Close();
            return dt;
        }


        #endregion

    }
}