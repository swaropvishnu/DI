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
using System.Net.NetworkInformation;

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
                if (dt != null && dt.Rows.Count > 0)
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
                    objdd.ProductName = reader["ProductName"].ToString();
                    objdd.FiananceDetails = reader["FiananceDetails"].ToString();
                    objdd.RowMeterialSource = reader["RowMeterialSource"].ToString();
                    objdd.PracticalProjectReport = reader["PracticalProjectReport"].ToString();
                    objdd.HelpFromForegin = reader["HelpFromForegin"].ToString();
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

        internal String UpdateUserDetail(LoginModal objUserData)
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



        internal string InsertUpdateApplicationFormDetail(FormModal Objform)
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

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public static string GetIpAddress()
        {
            string ipaddress;

            ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipaddress == "" || ipaddress == null)

                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }
        string IpAddress = GetIpAddress();
        string MacAddress = GetMACAddress();


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
                cmd.CommandText = "[Proc_InsertUpdateIndustrialEstate]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("IndustrialEstateCode", m06.IndustrialEstateCode);
                cmd.Parameters.AddWithValue("IndustrialEstateName", m06.IndustrialEstateName);
                cmd.Parameters.AddWithValue("Establishment", m06.Establishment);
                cmd.Parameters.AddWithValue("Address", m06.Address);
                cmd.Parameters.AddWithValue("DisID", m06.DistrictCode);
                cmd.Parameters.AddWithValue("TehSilID", m06.TehsilCode);
                cmd.Parameters.AddWithValue("BlocID", m06.BlockCode);
                cmd.Parameters.AddWithValue("@VillageID", m06.VillageCode);
                cmd.Parameters.AddWithValue("PinCode", m06.PinCode == null ? "" : m06.PinCode);
                cmd.Parameters.AddWithValue("@AreaPerSqfeet", m06.AreaPerSqfeet);
                cmd.Parameters.AddWithValue("@RatePerSqFeet", m06.RatePerSqFeet);
                cmd.Parameters.AddWithValue("PlotNo", m06.PlotNo);
                cmd.Parameters.AddWithValue("ShadeNo", m06.ShadeNo);
                cmd.Parameters.AddWithValue("NearestRailwayStationKm", m06.NearestRailwayStationKm);
                cmd.Parameters.AddWithValue("NearestBusStationKM", m06.NearestBusStationKM);
                cmd.Parameters.AddWithValue("ISStreet", m06.IsStreet == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsElectriccity", m06.IsElectriccity == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsDrainage", m06.IsDrainage == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsDrinkingWater", m06.IsDrinkingWater == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsRawMaterialsSiding", m06.IsRawMaterialsSiding == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsIndustrialPark", m06.IsIndustrialPark == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("CUserID", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("@CUserIP", this.IpAddress);
                cmd.Parameters.AddWithValue("@CMac", this.MacAddress);
                cmd.Parameters.AddWithValue("@UMac", this.MacAddress);
                cmd.Parameters.AddWithValue("@UUserID", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("@UUserIP", this.IpAddress);
                cmd.Parameters.AddWithValue("IsDel", IsDel);//sptype
                cmd.Parameters.AddWithValue("industrytype_code_diff", m06.industrytype_code_diff);
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

        public string InsertUpdateEstateAllotee(IndustrialEstateAllotee IEA, List<IndustrialEstateAlloteePlot> L01, List<IndustrialEstateAlloteeShed> L02, bool @Sptype)
        {
            string message = "";
            int allotee_code = -1;
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdateEstateAllotee]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("allotee_code", IEA.allotee_code);
                cmd.Parameters["allotee_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["allotee_code"].Size = 256;
                cmd.Parameters.AddWithValue("allotee_name", IEA.@allotee_name);
                cmd.Parameters.AddWithValue("company_name", IEA.@company_name);
                cmd.Parameters.AddWithValue("panno", IEA.@panno);
                cmd.Parameters.AddWithValue("cinno", IEA.@cinno);
                cmd.Parameters.AddWithValue("address", IEA.@address);
                cmd.Parameters.AddWithValue("district_code_census", IEA.@district_code_census);
                cmd.Parameters.AddWithValue("tehsil_code_census", IEA.@tehsil_code_census);
                cmd.Parameters.AddWithValue("block_code", IEA.@block_code);
                cmd.Parameters.AddWithValue("village_code", IEA.@village_code);
                cmd.Parameters.AddWithValue("mobile", IEA.@mobile);
                cmd.Parameters.AddWithValue("email", IEA.@email);
                cmd.Parameters.AddWithValue("industrytype_code_diff", IEA.industrytype_code_diff);
                cmd.Parameters.AddWithValue("companytype_code", IEA.@companytype_code);
                cmd.Parameters.AddWithValue("estimated_cost", IEA.@estimated_cost);
                cmd.Parameters.AddWithValue("projected_employment", IEA.@projected_employment);
                cmd.Parameters.AddWithValue("estimate_time", IEA.@estimate_time);
                cmd.Parameters.AddWithValue("related_work_experience", IEA.@related_work_experience);
                cmd.Parameters.AddWithValue("covered_area", IEA.@covered_area);
                cmd.Parameters.AddWithValue("uncovered_area", IEA.@uncovered_area);
                cmd.Parameters.AddWithValue("land_investment", IEA.@land_investment);
                cmd.Parameters.AddWithValue("other_investment", IEA.@other_investment);
                cmd.Parameters.AddWithValue("building_investment", IEA.@building_investment);
                cmd.Parameters.AddWithValue("equipment_investment", IEA.@equipment_investment);
                cmd.Parameters.AddWithValue("property_cost", IEA.property_cost);
                cmd.Parameters.AddWithValue("is_produce_smoke", IEA.is_produce_smoke);
                cmd.Parameters.AddWithValue("is_electricity", IEA.is_electricity);
                cmd.Parameters.AddWithValue("is_telephone", IEA.is_telephone);
                cmd.Parameters.AddWithValue("grossprofit", IEA.grossprofit);
                cmd.Parameters.AddWithValue("c_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("c_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("c_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("u_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("u_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("u_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("Sptype", @Sptype);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                allotee_code = int.Parse(cmd.Parameters["allotee_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();

                if (allotee_code != -1)
                {
                    if (L01 != null)
                    {
                        if (L01.Count > 0)
                        {
                            for (int i = 0; i < L01.Count; i++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertEstateAlloteePlot]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("allotee_code", allotee_code);
                                cmd.Parameters.AddWithValue("plot_code", L01[i].@plot_code);
                                cmd.Parameters.AddWithValue("@industrial_estate_code", L01[i].@industrial_estate_code);
                                cmd.Parameters.AddWithValue("@allotmentno", L01[i].allotmentno);
                                cmd.Parameters.AddWithValue("IsFirst", i == 0 ? true : false);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }


                        }
                    }
                    if (L02 != null)
                    {
                        if (L02.Count > 0)
                        {
                            for (int j = 0; j < L02.Count; j++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertEstateAlloteeShed]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("allotee_code", allotee_code);
                                cmd.Parameters.AddWithValue("shed_code", L02[j].shed_code);
                                cmd.Parameters.AddWithValue("@industrial_estate_code", L02[j].@industrial_estate_code);
                                cmd.Parameters.AddWithValue("@allotmentno", L02[j].@allotmentno);
                                cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                transaction.Commit();
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
                cmd.CommandText = "[Proc_InsertUpdatePlot]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("PlotCode", AP.PlotCode);
                cmd.Parameters.AddWithValue("@tbl_IndustrialEstateCode", AP.IndustrialEstateCode);
                cmd.Parameters.AddWithValue("PlotSerial", AP.PlotSerial);
                cmd.Parameters.AddWithValue("PlotName", AP.PlotName);
                cmd.Parameters.AddWithValue("@PlotArea", AP.PlotArea);
                cmd.Parameters.AddWithValue("IsPlot_Assigned", AP.IsPlot_Assigned == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsPlot_Disputed", AP.IsPlot_Disputed == true ? 'Y' : 'N');
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
                cmd.Parameters.AddWithValue("@IndustrialEstateCode", IndustrialAreaCode);
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
                cmd.Parameters.AddWithValue("@IndustrialEstateCode", IndustrialAreaCode);
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

        public DataTable GetDashBord()
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetDashBord]";
                cmd.CommandType = CommandType.StoredProcedure;

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

        public DataSet GetEstateeAllotee(long allotee_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_GetEstateeAllotee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@allotee_code", allotee_code);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            con.Close();
            return ds;
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
                cmd.Parameters.AddWithValue("tbl_IndustrialEstateCode", AS.IndustrialEstateCode);
                cmd.Parameters.AddWithValue("ShedSerial", AS.ShedSerial);
                cmd.Parameters.AddWithValue("ShedName", AS.ShedName);
                cmd.Parameters.AddWithValue("ShedArea", AS.ShedArea);
                cmd.Parameters.AddWithValue("IsShed_Assigned", AS.IsShed_Assigned == true ? 'Y' : 'N');
                cmd.Parameters.AddWithValue("IsShed_Disputed", AS.IsShed_Disputed == true ? 'Y' : 'N');
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

        public DataTable GetIndustrialEstateInfo(int @IndustrialEstateCode, string @IndustrialEstateName, int @DistrictID, int @TehSilID, int @BlocID, int VillageID, string @PinCode, string @PlotNo, string @ShadeNo, DateTime @FromDate, DateTime @ToDate, DateTime @FromEstablishment, DateTime @ToEstablishment, string IndustryType)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetIndustrialEstateInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialEstateCode", IndustrialEstateCode);
                cmd.Parameters.AddWithValue("@IndustrialEstateName", @IndustrialEstateName);
                cmd.Parameters.AddWithValue("@DistrictID", @DistrictID);
                cmd.Parameters.AddWithValue("@TehSilID", @TehSilID);
                cmd.Parameters.AddWithValue("@BlocID", @BlocID);
                cmd.Parameters.AddWithValue("@VillageID", @VillageID);
                cmd.Parameters.AddWithValue("@PinCode", @PinCode);
                cmd.Parameters.AddWithValue("@PlotNo", @PlotNo);
                cmd.Parameters.AddWithValue("@ShadeNo", @ShadeNo);
                cmd.Parameters.AddWithValue("@FromDate", @FromDate);
                cmd.Parameters.AddWithValue("@ToDate", @ToDate);
                cmd.Parameters.AddWithValue("@FromEstablishment", @FromEstablishment);
                cmd.Parameters.AddWithValue("@ToEstablishment", @ToEstablishment);
                cmd.Parameters.AddWithValue("@IndustrialType", IndustryType);
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

        public DataTable GetShedInfo(int @IndustrialEstateCode, Int64 @ShedCode, string @ShedSerialNo, string @ShedName, DateTime @FromDate, DateTime @ToDate, string @IsShed_Disputed, string @IsShed_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetShed]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialEstateCode", IndustrialEstateCode);
                cmd.Parameters.AddWithValue("@ShedCode", @ShedCode);
                cmd.Parameters.AddWithValue("@ShedSerialNo", @ShedSerialNo);
                cmd.Parameters.AddWithValue("@ShedName", @ShedName);
                cmd.Parameters.AddWithValue("@FromDate", @FromDate);
                cmd.Parameters.AddWithValue("@ToDate", @ToDate);
                cmd.Parameters.AddWithValue("@IsShed_Disputed", @IsShed_Disputed);
                cmd.Parameters.AddWithValue("@IsShed_Assigned", @IsShed_Assigned);
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


        public DataTable GetPlotInfo(int IndustrialEstateCode, Int64 @PlotCode, string @PlotSerialNo, string @PlotName, DateTime @FromDate, DateTime @ToDate, string @IsPlot_Disputed, string @IsPlot_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlot]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialEstateCode ", IndustrialEstateCode);
                cmd.Parameters.AddWithValue("@PlotCode", @PlotCode);
                cmd.Parameters.AddWithValue("@PlotSerialNo", @PlotSerialNo);
                cmd.Parameters.AddWithValue("@PlotName", @PlotName);
                cmd.Parameters.AddWithValue("@FromDate", @FromDate);
                cmd.Parameters.AddWithValue("@ToDate", @ToDate);
                cmd.Parameters.AddWithValue("@IsPlot_Disputed", @IsPlot_Disputed);
                cmd.Parameters.AddWithValue("@IsPlot_Assigned", @IsPlot_Assigned);
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
        public DataTable GetPlotforSal(int IndustrialEstateCode)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlotforSale]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IndustrialEstateCode ", IndustrialEstateCode);
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
        #region Form
        internal string InsertUpdateMYSY(AddMYSY Objform)
        {
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("Proc_InsertUpdate_Swarozgar_yojana", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@district_code_census", SqlDbType.Int).Value = Objform.DistCode;
                cmd.Parameters.AddWithValue("@tehsil_code_census", SqlDbType.Int).Value = Objform.TehsilCode;
                cmd.Parameters.AddWithValue("@block_code", SqlDbType.Int).Value = Objform.BlockCode;
                cmd.Parameters.AddWithValue("@village_code", SqlDbType.Int).Value = Objform.VillCode;
                cmd.Parameters.AddWithValue("@applicant_name", SqlDbType.NVarChar).Value = Objform.applicant_name;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.DateTime).Value = Objform.dob;
                cmd.Parameters.AddWithValue("@adhar_no", SqlDbType.VarChar).Value = Objform.adhar_no;
                cmd.Parameters.AddWithValue("@father_name", SqlDbType.NVarChar).Value = Objform.father_name;
                cmd.Parameters.AddWithValue("@current_address", SqlDbType.NVarChar).Value = Objform.current_address;
                cmd.Parameters.AddWithValue("@bank_office_address", SqlDbType.NVarChar).Value = Objform.bank_office_address;
                cmd.Parameters.AddWithValue("@permanent_address", SqlDbType.NVarChar).Value = Objform.permanent_address;
                cmd.Parameters.AddWithValue("@mobile_no", SqlDbType.VarChar).Value = Objform.mobile_no;
                cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = Objform.email;
                cmd.Parameters.AddWithValue("@pansion_card_no", SqlDbType.VarChar).Value = Objform.pansion_card_No;
                cmd.Parameters.AddWithValue("@family_income", SqlDbType.Decimal).Value = Objform.family_income;
                cmd.Parameters.AddWithValue("@project_name", SqlDbType.NVarChar).Value = Objform.project_name;
                cmd.Parameters.AddWithValue("@project_cost", SqlDbType.Decimal).Value = Objform.project_cost;
                cmd.Parameters.AddWithValue("@machinery_cost", SqlDbType.Decimal).Value = Objform.machinery_cost;
                cmd.Parameters.AddWithValue("@working_capital", SqlDbType.Decimal).Value = Objform.working_capital;

                cmd.Parameters.AddWithValue("@bank_name", SqlDbType.NVarChar).Value = Objform.bank_name;
                cmd.Parameters.AddWithValue("@self_share", SqlDbType.NVarChar).Value = Objform.self_share;
                cmd.Parameters.AddWithValue("@deposit_amt", SqlDbType.Decimal).Value = Objform.deposit_amt;
                //cmd.Parameters.AddWithValue("@Depositbankbranch", SqlDbType.NVarChar).Value = Objform.branch_name;
                cmd.Parameters.AddWithValue("@branch_name", SqlDbType.NVarChar).Value = Objform.branch_name;
                cmd.Parameters.AddWithValue("@bank_account_no", SqlDbType.NVarChar).Value = Objform.bank_account_no;
                cmd.Parameters.AddWithValue("@ifsc_code", SqlDbType.NVarChar).Value = Objform.@ifsc_code;
                cmd.Parameters.AddWithValue("@c_user_id", SqlDbType.VarChar).Value = Objform.c_user_id;
                cmd.Parameters.AddWithValue("@c_user_ip", SqlDbType.VarChar).Value = Objform.c_user_ip;
                cmd.Parameters.AddWithValue("@c_mac", SqlDbType.NVarChar).Value = Objform.c_mac;

                //cmd.Parameters.AddWithValue("@FamilyPersonName", SqlDbType.NVarChar).Value = Objform.FamilyPersonName;
                //cmd.Parameters.AddWithValue("@FamilyPerson_Age", SqlDbType.Int).Value = Objform.FamilyPerson_Age;
                //cmd.Parameters.AddWithValue("@FamilyPerson_work", SqlDbType.Int).Value = Objform.FamilyPerson_work;
                //cmd.Parameters.AddWithValue("@FamilyPerson_Father_Name", SqlDbType.NVarChar).Value = Objform.FamilyPerson_Father_Name;
                //cmd.Parameters.AddWithValue("@Mother_Name", SqlDbType.NVarChar).Value = Objform.Mother_Name;
                //cmd.Parameters.AddWithValue("@Mother_Age", SqlDbType.Int).Value = Objform.Mother_Age;
                //cmd.Parameters.AddWithValue("@Mother_work", SqlDbType.NVarChar).Value = Objform.Mother_work;
                //cmd.Parameters.AddWithValue("@Family_brother_sister", SqlDbType.NVarChar).Value = Objform.Family_brother_sister;
                //cmd.Parameters.AddWithValue("@Family_brother_sister_Age", SqlDbType.Int).Value = Objform.Family_brother_sister_Age;
                //cmd.Parameters.AddWithValue("@Family_brother_sister_Work", SqlDbType.NVarChar).Value = Objform.Family_brother_sister_Work;
                //cmd.Parameters.AddWithValue("@children_Name", SqlDbType.NVarChar).Value = Objform.children_Name;
                //cmd.Parameters.AddWithValue("@children_Age", SqlDbType.Int).Value = Objform.children_Age;
                //cmd.Parameters.AddWithValue("@children_Work", SqlDbType.Int).Value = Objform.children_Work;


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


        public DataTable GetMYSY(AddMYSY Objform)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_InsertUpdate_Swarozgar_yojana]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "select");

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

        public string InsertYojona_Master(Yojona_Master Objform)
        {
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("Proc_Yojana_master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yojana_code", SqlDbType.Int).Value = Objform.yojana_code;
                cmd.Parameters.AddWithValue("@yojana_name", SqlDbType.VarChar).Value = Objform.yojana_name;
                cmd.Parameters.AddWithValue("@yojana_name_u", SqlDbType.NVarChar).Value = Objform.yojana_name_u;
                cmd.Parameters.AddWithValue("@status", SqlDbType.Char).Value = Objform.status;
                cmd.Parameters.AddWithValue("c_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("c_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("c_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("u_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("u_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("u_mac", this.MacAddress);
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


        public DataTable GetYojona_Master(Yojona_Master Objform)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_Yojana_master]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "select");

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

        #region Applicant

        public static IDataReader GetApplicantMenuData(string UserId,short yojanacode)
        {
            try
            {
                IDataReader ds;//= new IDataReader();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@applicant_user_name", UserId));
                parameters.Add(new SqlParameter("@Yojana_Code", yojanacode));

                ds = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, "Proc_GetApplicantMenu", parameters.ToArray());
                return ds;
            }


            catch
            {
                return null;
            }
        }
        public string InsertUpdateCMYSS_Applicantdoc(List<CMYSS_Applicant_Doc> objdoc)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (objdoc != null)
                {
                    if (objdoc.Count > 0)
                    {
                        for (int j = 0; j < objdoc.Count; j++)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertApplicantDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.AddWithValue("applicant_code", objdoc[j].applicant_code);
                            cmd.Parameters.AddWithValue("@doc", objdoc[j].doc);
                            cmd.Parameters.AddWithValue("@doc_type", objdoc[j].doc_type);
                            cmd.Parameters.AddWithValue("@doc_content_type", objdoc[j].@doc_content_type);

                            cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                            cmd.Parameters.AddWithValue("@user_Id", @UserSession.LoggedInUser.UserName);
                            cmd.Parameters.AddWithValue("@user_ip", this.IpAddress);
                            cmd.Parameters.AddWithValue("@user_mac", this.MacAddress);
                            //cmd.Parameters.AddWithValue("@UMac", this.MacAddress);
                            // cmd.Parameters.AddWithValue("@UUserID", @UserSession.LoggedInUser.UserName);
                            // cmd.Parameters.AddWithValue("@UUserIP", this.IpAddress);
                            cmd.Parameters.AddWithValue("Msg", "");
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            if (j == 0)
                            {
                                message = cmd.Parameters["Msg"].Value.ToString();
                            }

                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }

        public string InsertUpdateCMYSS_Applicantdoc2(DataTable dt)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertApplicantDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.AddWithValue("applicant_code", dt.Rows[j]["applicant_code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@doc", dt.Rows[j]["doc"]);// 
                            cmd.Parameters.AddWithValue("@doc_type", dt.Rows[j]["doc_type"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@doc_content_type", dt.Rows[j]["doc_content_type"].ToString().Trim());

                            cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                            cmd.Parameters.AddWithValue("@user_Id", @UserSession.LoggedInUser.UserName);
                            cmd.Parameters.AddWithValue("@user_ip", this.IpAddress);
                            cmd.Parameters.AddWithValue("@user_mac", this.MacAddress);
                            //cmd.Parameters.AddWithValue("@UMac", this.MacAddress);
                            // cmd.Parameters.AddWithValue("@UUserID", @UserSession.LoggedInUser.UserName);
                            // cmd.Parameters.AddWithValue("@UUserIP", this.IpAddress);
                            cmd.Parameters.AddWithValue("Msg", "");
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            if (j == 0)
                            {
                                message = cmd.Parameters["Msg"].Value.ToString();
                            }

                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }



        public string InsertUpdateCMYSS_Applicantdoc3(DataTable dt)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertApplicantDoc2]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("applicant_code", dt.Rows[0]["applicant_code"].ToString().Trim());
                cmd.Parameters.AddWithValue("@tbl_applicant_doc", dt);

                // cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                cmd.Parameters.AddWithValue("@user_Id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("@user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("@user_mac", this.MacAddress);
                //cmd.Parameters.AddWithValue("@UMac", this.MacAddress);
                // cmd.Parameters.AddWithValue("@UUserID", @UserSession.LoggedInUser.UserName);
                // cmd.Parameters.AddWithValue("@UUserIP", this.IpAddress);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                message = cmd.Parameters["Msg"].Value.ToString();

                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }

        public string InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Doc> objdoc, List<CMYSS_Applicant_Family> objFamily, bool @sptype)
        {
            string message = "";
            long applicant_code = -1;
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdateApplicant]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("applicant_code", Objform.@applicant_code);
                cmd.Parameters["applicant_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["applicant_code"].Size = 256;
                cmd.Parameters.AddWithValue("@yojana_code", Objform.@yojana_code);
                cmd.Parameters.AddWithValue("@applicant_name", Objform.@applicant_name);
                cmd.Parameters.AddWithValue("@adhar_no", Objform.@adhar_no);
                cmd.Parameters.AddWithValue("@dob", Objform.@dob);
                cmd.Parameters.AddWithValue("@father_name", Objform.@Husband_father_name);
                cmd.Parameters.AddWithValue("@current_address", Objform.@current_address);
                cmd.Parameters.AddWithValue("@proposed_office_address", Objform.proposed_office_address == null ? "" : Objform.proposed_office_address);
                cmd.Parameters.AddWithValue("@permanent_address", Objform.@permanent_address == null ? "" : Objform.permanent_address);
                cmd.Parameters.AddWithValue("@mobile_no", Objform.@mobile_no);
                cmd.Parameters.AddWithValue("@email", Objform.@email);
                cmd.Parameters.AddWithValue("@pansion_card_no", Objform.pansion_card_No == null ? "" : Objform.pansion_card_No);
                cmd.Parameters.AddWithValue("@project_name", Objform.@project_name == null ? "" : Objform.project_name);
                cmd.Parameters.AddWithValue("@family_income", Objform.@family_income);
                cmd.Parameters.AddWithValue("@project_cost", Objform.@project_cost);
                cmd.Parameters.AddWithValue("@machinery_cost", Objform.@machinery_cost);
                cmd.Parameters.AddWithValue("@working_capital", Objform.@working_capital);
                cmd.Parameters.AddWithValue("@bank_name", Objform.@bank_name == null ? "" : Objform.bank_name);
                cmd.Parameters.AddWithValue("@self_share", Objform.@self_share);
                cmd.Parameters.AddWithValue("@deposit_amt", Objform.@deposit_amt);
                cmd.Parameters.AddWithValue("@branch_name", Objform.@branch_name == null ? "" : Objform.branch_name);
                cmd.Parameters.AddWithValue("@bank_account_no", Objform.@bank_account_no == null ? "" : Objform.bank_account_no);
                cmd.Parameters.AddWithValue("@ifsc_code", Objform.@ifsc_code == null ? "" : Objform.ifsc_code);
                cmd.Parameters.AddWithValue("@district_code_census", Objform.@DistCode);
                cmd.Parameters.AddWithValue("@tehsil_code_census", Objform.@TehsilCode);
                cmd.Parameters.AddWithValue("@block_code", Objform.@BlockCode);
                cmd.Parameters.AddWithValue("@village_code", Objform.@VillCode);
                cmd.Parameters.AddWithValue("UserName", "");
                cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["UserName"].Size = 256;
                cmd.Parameters.AddWithValue("@Password", Objform.@Password);
                cmd.Parameters.AddWithValue("c_user_id", "");
                cmd.Parameters.AddWithValue("c_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("c_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("u_user_id", "");
                cmd.Parameters.AddWithValue("u_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("u_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("Sptype", @sptype);
                cmd.Parameters.AddWithValue("@Steps", Objform.steps);
                cmd.Parameters.AddWithValue("@manufacturing", Objform.manufacturing);
                cmd.Parameters.AddWithValue("@services", Objform.services);
                cmd.Parameters.AddWithValue("@Is_village_bank", Objform.Is_village_bank);
                cmd.Parameters.AddWithValue("@service_branch", Objform.service_branch);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                applicant_code = int.Parse(cmd.Parameters["applicant_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();
                if (message.Contains("Save"))
                {
                    message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                }

                if (applicant_code != -1)
                {
                    if (objFamily != null)
                    {
                        if (objFamily.Count > 0)
                        {
                            for (int i = 0; i < objFamily.Count; i++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertApplicantFamilyDetails]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("@applicant_code", applicant_code);
                                cmd.Parameters.AddWithValue("@relation_code", objFamily[i].@relation_code);
                                cmd.Parameters.AddWithValue("@person_name", objFamily[i].@person_name);
                                cmd.Parameters.AddWithValue("@age", objFamily[i].@age);
                                cmd.Parameters.AddWithValue("@workingfield", objFamily[i].@workingfield);
                                cmd.Parameters.AddWithValue("IsFirst", i == 0 ? true : false);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    if (objdoc != null)
                    {
                        if (objdoc.Count > 0)
                        {
                            for (int j = 0; j < objdoc.Count; j++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertApplicantDoc]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("applicant_code", applicant_code);
                                cmd.Parameters.AddWithValue("doc_path", objdoc[j].doc);
                                cmd.Parameters.AddWithValue("@doc_type", objdoc[j].doc_type);

                                cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();


                            }
                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }


        public string InsertUpdateCMYSS_Applicantfamily(List<CMYSS_Applicant_Family> objFamily)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                {
                    if (objFamily != null)
                    {
                        if (objFamily.Count > 0)
                        {
                            for (int i = 0; i < objFamily.Count; i++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertApplicantFamilyDetails]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("@applicant_code", objFamily[i].applicant_code);
                                cmd.Parameters.AddWithValue("@relation_code", objFamily[i].@relation_code);
                                cmd.Parameters.AddWithValue("@person_name", objFamily[i].@person_name);
                                cmd.Parameters.AddWithValue("@age", objFamily[i].@age);
                                cmd.Parameters.AddWithValue("@workingfield", objFamily[i].@workingfield);
                                cmd.Parameters.AddWithValue("IsFirst", i == 0 ? true : false);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                                if (i == 0)
                                {
                                    message = cmd.Parameters["Msg"].Value.ToString();
                                }
                            }
                        }
                    }
                    //if (objdoc != null)
                    //{
                    //    if (objdoc.Count > 0)
                    //    {
                    //        for (int j = 0; j < objdoc.Count; j++)
                    //        {
                    //            cmd = new SqlCommand();
                    //            cmd.Connection = con;
                    //            cmd.CommandType = CommandType.StoredProcedure;
                    //            cmd.CommandText = "[Proc_InsertApplicantDoc]";
                    //            cmd.CommandTimeout = 3600;
                    //            cmd.Parameters.AddWithValue("applicant_code", applicant_code);
                    //            cmd.Parameters.AddWithValue("doc_path", objdoc[j].doc);
                    //            cmd.Parameters.AddWithValue("@doc_type", objdoc[j].doc_type);

                    //            cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                    //            cmd.Parameters.AddWithValue("Msg", "");
                    //            cmd.Parameters["Msg"].Size = 256;
                    //            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    //            cmd.Transaction = transaction;
                    //            cmd.ExecuteNonQuery();
                    //        }
                    //    }
                    //}
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }

        public DataSet GetApplicantinfo(long applicant_code, short yojana_code, string applicant_user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetApplicant]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("applicant_code ", applicant_code);
                cmd.Parameters.AddWithValue("@yojana_code", yojana_code);
                cmd.Parameters.AddWithValue("@applicant_user_name", applicant_user_name);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            con.Close();
            return ds;
        }

        public DataSet GetApplicant(long applicant_code, short yojana_code, string applicant_user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetApplicant]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("applicant_code ", applicant_code);
                cmd.Parameters.AddWithValue("@yojana_code", yojana_code);
                cmd.Parameters.AddWithValue("@applicant_user_name", applicant_user_name);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            con.Close();
            return ds;
        }

        public string InsertApplicantDoc(List<CMYSS_Applicant_Doc> objdoc)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (objdoc != null)
                {
                    if (objdoc.Count > 0)
                    {
                        for (int j = 0; j < objdoc.Count; j++)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertApplicantDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.AddWithValue("applicant_code", objdoc[j].applicant_code);
                            cmd.Parameters.AddWithValue("doc_path", objdoc[j].doc);
                            cmd.Parameters.AddWithValue("@doc_type", objdoc[j].doc_type);
                            cmd.Parameters.AddWithValue("IsFirst", j == 0 ? true : false);
                            cmd.Parameters.AddWithValue("Msg", "");
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            if (j == 0)
                            {
                                message = cmd.Parameters["Msg"].Value.ToString();
                            }

                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }

        public string Plot_Applicant(estate_request objER, List<requested_plot> objRP, IndustrialEstateApplicant objApp, bool Sptype)
        {
            string message = "";
            long request_code = -1;
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertestateRequest]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("request_code", objER.request_code);
                cmd.Parameters["request_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["request_code"].Size = 256;
               
                cmd.Parameters.AddWithValue("division_code", objER.division_code);
                cmd.Parameters.AddWithValue("@industrial_estate_code", objER.@industrial_estate_code);
                cmd.Parameters.AddWithValue("request_no", "");
                cmd.Parameters.AddWithValue("@c_user_id", "");
                cmd.Parameters.AddWithValue("@c_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("@c_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("@u_user_id", "");
                cmd.Parameters.AddWithValue("@u_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("@u_mac", this.MacAddress);
               
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                request_code = long.Parse(cmd.Parameters["request_code"].Value.ToString());
                if (request_code != -1)
                {
                    if (objRP != null)
                    {
                        if (objRP.Count > 0)
                        {
                            for (int i = 0; i < objRP.Count; i++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertestateRequestedplot]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.AddWithValue("@request_code", request_code);
                                cmd.Parameters.AddWithValue("@plot_code", objRP[i].plot_code);
                                cmd.Parameters.AddWithValue("IsFirst", i == 0 ? true : false);
                                cmd.Parameters.AddWithValue("@user_id", "");
                                cmd.Parameters.AddWithValue("@mac", this.MacAddress);
                                cmd.Parameters.AddWithValue("@user_ip", this.IpAddress);
                                cmd.Parameters.AddWithValue("Msg", "");
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_InsertUpdateEstateApplicant]";
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@request_code", request_code);
                    cmd.Parameters.AddWithValue("estate_applicant_code", objApp.estate_applicant_code);
                    cmd.Parameters["estate_applicant_code"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["estate_applicant_code"].Size = 256;
                    cmd.Parameters.AddWithValue("@estate_applicant_name", objApp.@estate_applicant_name);
                    cmd.Parameters.AddWithValue("company_name", objApp.company_name == null ? "" : objApp.company_name);
                    cmd.Parameters.AddWithValue("panno", objApp.panno == null ? "" : objApp.panno);
                    cmd.Parameters.AddWithValue("cinno", objApp.cinno == null ? "" : objApp.cinno);
                    cmd.Parameters.AddWithValue("address", objApp.address);
                    cmd.Parameters.AddWithValue("district_code_census", objApp.district_code_census);
                    cmd.Parameters.AddWithValue("tehsil_code_census", objApp.tehsil_code_census);
                    cmd.Parameters.AddWithValue("block_code", objApp.block_code);
                    cmd.Parameters.AddWithValue("village_code", objApp.village_code);
                    cmd.Parameters.AddWithValue("mobile", objApp.mobile);
                    cmd.Parameters.AddWithValue("email", objApp.email);
                    cmd.Parameters.AddWithValue("industrytype_code_diff", objApp.industrytype_code_diff == null ? "" : objApp.industrytype_code_diff);
                    cmd.Parameters.AddWithValue("companytype_code", objApp.companytype_code);
                    cmd.Parameters.AddWithValue("estimated_cost", objApp.estimated_cost);
                    cmd.Parameters.AddWithValue("estimate_time", objApp.estimate_time);
                    cmd.Parameters.AddWithValue("related_work_experience", objApp.related_work_experience);
                    cmd.Parameters.AddWithValue("projected_employment", objApp.projected_employment);
                    cmd.Parameters.AddWithValue("equipment_investment", objApp.equipment_investment);
                    cmd.Parameters.AddWithValue("covered_area", objApp.covered_area);
                    cmd.Parameters.AddWithValue("uncovered_area", objApp.uncovered_area);
                    cmd.Parameters.AddWithValue("land_investment", objApp.land_investment);
                    cmd.Parameters.AddWithValue("other_investment", objApp.other_investment);
                    cmd.Parameters.AddWithValue("building_investment", objApp.building_investment);
                    cmd.Parameters.AddWithValue("property_cost", objApp.property_cost);
                    cmd.Parameters.AddWithValue("is_produce_smoke", objApp.is_produce_smoke);
                    cmd.Parameters.AddWithValue("is_electricity", objApp.is_electricity);
                    cmd.Parameters.AddWithValue("is_telephone", objApp.is_telephone);
                    cmd.Parameters.AddWithValue("grossprofit", objApp.grossprofit);
                    cmd.Parameters.AddWithValue("@c_user_id", "");
                    cmd.Parameters.AddWithValue("@c_user_ip", this.IpAddress);
                    cmd.Parameters.AddWithValue("@c_mac", this.MacAddress);
                    cmd.Parameters.AddWithValue("@u_user_id","");
                    cmd.Parameters.AddWithValue("@u_user_ip", this.IpAddress);
                    cmd.Parameters.AddWithValue("@u_mac", this.MacAddress);
                    cmd.Parameters.AddWithValue("Sptype", Sptype);
                    cmd.Parameters.AddWithValue("@dob", objApp.@dob);
                    cmd.Parameters.AddWithValue("@father_name", objApp.@father_name);
                    cmd.Parameters.AddWithValue("@aadhar_no", objApp.@aadhar_no);
                    cmd.Parameters.AddWithValue("UserName", "");
                    cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["UserName"].Size = 256;
                    cmd.Parameters.AddWithValue("@Password", objApp.@Password);
                    cmd.Parameters.AddWithValue("Msg", "");
                    cmd.Parameters["Msg"].Size = 256;
                    cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    message = cmd.Parameters["Msg"].Value.ToString();
                    if (message.Contains("Save"))
                    {
                        message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                    }
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                    message = "Transaction Fail..";
                }
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            con.Close();
            return message;
        }


        #endregion
        #region Master
        internal string InsertUpdate_doc_type(Doc_type DI)
        {
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("Proc_InsertUpdate_doc_type", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doc_code_census", DI.doc_code_census);
                cmd.Parameters.AddWithValue("@doc_type", DI.Docment);
                cmd.Parameters.AddWithValue("@doc_code", DI.doc_code);
                cmd.Parameters.AddWithValue("@status", "A");
                cmd.Parameters.AddWithValue("@u_doc_type", DI.u_doc_type);
                cmd.Parameters.AddWithValue("@desciption", DI.desciption);
                cmd.Parameters.AddWithValue("@u_description", DI.u_description);
                cmd.Parameters.AddWithValue("@c_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("@c_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("@c_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("@u_user_id", @UserSession.LoggedInUser.UserName);
                cmd.Parameters.AddWithValue("@u_user_ip", this.IpAddress);
                cmd.Parameters.AddWithValue("@u_mac", this.MacAddress);
                cmd.Parameters.AddWithValue("@Mode", SqlDbType.VarChar).Value = DI.Mode;
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0][0].ToString();
                }
                else
                {
                    result = "Success";
                }

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

        public DataTable Getdoc_type(Doc_type Objform)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_InsertUpdate_doc_type]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "select");

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

        public DataSet GetPramarPatar(long applicant_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_Get_PramarPatar";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@applicant_code", applicant_code);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            con.Close();
            return ds;
        }

        public DataSet Getapplicant_Letter(long applicant_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_applicant_Letter";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@applicant_code", applicant_code);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            con.Close();
            return ds;
        }

    }
}