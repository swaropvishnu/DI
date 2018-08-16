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
                cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value = objUserData.UserId);
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
        public string filter_bad_chars_rep(string s)
        {
            string[] sL_Char = {
            "onfocus",          "\"\"",         "=",            "onmouseover",          "prompt",           "%27",          "alert#",
            "alert",
            "'or",
            "`or",
            "`or`",
            "'or'",
            "'='",
            "`=`",
            "'",
            "`",
            "9,9,9",
            "src",
            "onload",
            "select",
            "drop",
            "insert",
            "delete",
            "xp_",
            "having",
            "union",
            "group",
            "update",
            "script",
            "<script",
            "</script>",
            "language",
            "javascript",
            "vbscript",
            "http",
            "~",
            "$",
            "<",
            ">",
            "%",
            "\\",
            ";",
            "@",
            "_",
            "{",
            "}",
            "^",
            "?",
            "[",
            "]",
            "!",
            "#",
            "&",
            "*"
        };
            int er = 0;
            int sL_Char_Length = sL_Char.Length - 1;
            while (sL_Char_Length >= 0)
            {
                if (s.Contains(sL_Char[sL_Char_Length]))
                {
                    s.Replace(sL_Char[sL_Char_Length], "");
                    break; // TODO: might not be correct. Was : Exit While
                }
                sL_Char_Length -= 1;
            }
            return s;
        }
        public List<FormModal> ReportApplicationFormDetail(FormModal objform)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Proc_InsertUpdateApplicationFormDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = objform.Mode);
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
                cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value = objUserData.UserId);
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar).Value = objUserData.UserName);
                cmd.Parameters.Add(new SqlParameter("@UserHindiName", SqlDbType.NVarChar).Value = objUserData.UserNameHindi);
                cmd.Parameters.Add(new SqlParameter("@UserMobile", SqlDbType.VarChar).Value = objUserData.UserMobile);
                cmd.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar).Value = objUserData.UserEmail);
                cmd.Parameters.Add(new SqlParameter("@UserAddress", SqlDbType.VarChar).Value = objUserData.UserAddress);
                cmd.Parameters.Add(new SqlParameter("@UserProfileImage", SqlDbType.VarBinary).Value = objUserData.UserImage);
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
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt).Value = Objform.UserId);
                cmd.Parameters.Add(new SqlParameter("@ApplicantName", SqlDbType.NVarChar).Value = Objform.ApplicantName);
                cmd.Parameters.Add(new SqlParameter("@ApplicantAddress", SqlDbType.NVarChar).Value = Objform.ApplicantAddress);
                cmd.Parameters.Add(new SqlParameter("@IndrustryName", SqlDbType.NVarChar).Value = Objform.IndrustryName);
                cmd.Parameters.Add(new SqlParameter("@ApplicationFee", SqlDbType.Decimal).Value = Objform.ApplicationFee);
                cmd.Parameters.Add(new SqlParameter("@ApplicationFeeDate", SqlDbType.DateTime).Value = Objform.ApplicationFeeDate);
                cmd.Parameters.Add(new SqlParameter("@ApplicationFeedetails", SqlDbType.NVarChar).Value = Objform.ApplicationFeedetails);
                cmd.Parameters.Add(new SqlParameter("@IsPreRegistered", SqlDbType.Bit).Value = Objform.IsPreRegistered);
                if (Objform.IsPreRegistered == false)
                {
                    cmd.Parameters.Add(new SqlParameter("@OldRegistraionNo", SqlDbType.BigInt).Value = null);
                    cmd.Parameters.Add(new SqlParameter("@OldRegistrationDate", SqlDbType.DateTime).Value = null);

                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@OldRegistraionNo", SqlDbType.BigInt).Value = Objform.OldRegistraionNo);
                    cmd.Parameters.Add(new SqlParameter("@OldRegistrationDate", SqlDbType.DateTime).Value = Objform.OldRegistrationDate);
                }

                cmd.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar).Value = Objform.ProductName);
                cmd.Parameters.Add(new SqlParameter("@FiananceDetails", SqlDbType.NVarChar).Value = Objform.FiananceDetails);
                cmd.Parameters.Add(new SqlParameter("@RowMeterialSource", SqlDbType.NVarChar).Value = Objform.RowMeterialSource);
                cmd.Parameters.Add(new SqlParameter("@PracticalProjectReport", SqlDbType.VarChar).Value = Objform.PracticalProjectReport);
                cmd.Parameters.Add(new SqlParameter("@HelpFromForegin", SqlDbType.NVarChar).Value = Objform.HelpFromForegin);

                cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = Objform.Mode);
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

        //        cmd.Parameters.Add(new SqlParameter("@DistName", SqlDbType.NVarChar).Value = Objform.DistrictNames;
        //        cmd.Parameters.Add(new SqlParameter("@Established_industrial", SqlDbType.NVarChar).Value = Objform.Established_industrial;
        //        // cmd.Parameters.Add(new SqlParameter("@Indrustry_start_year", SqlDbType.DateTime).Value = Objform.Indrustry_start_year;  
        //        cmd.Parameters.Add(new SqlParameter("@Area", SqlDbType.Decimal).Value = Objform.Area;
        //        cmd.Parameters.Add(new SqlParameter("@Currentprevailingrate", SqlDbType.NVarChar).Value = Objform.Currentprevailingrate;
        //        cmd.Parameters.Add(new SqlParameter("@UserCurrentlynumbertotaldevelopedlandareaImage", SqlDbType.NVarChar).Value = Objform.UserCurrentlynumbertotaldevelopedlandareaImage;
        //        cmd.Parameters.Add(new SqlParameter("@Currentlynumbertotaldevelopedlandarea", SqlDbType.VarChar).Value = Objform.Currentlynumbertotaldevelopedlandarea;
        //        cmd.Parameters.Add(new SqlParameter("@Details_Each_plot_Available_Area", SqlDbType.VarChar).Value = Objform.Details_Each_plot_Availablenoncontroversial_Available_Area;
        //        cmd.Parameters.Add(new SqlParameter("@Accessibility_location_available_not", SqlDbType.VarChar).Value = Objform.Accessibility_location_available_not;
        //        cmd.Parameters.Add(new SqlParameter("@Whether_electricity_Available_not", SqlDbType.VarChar).Value = Objform.Whether_electricity_Available_not;
        //        cmd.Parameters.Add(new SqlParameter("@Whether_Drainage_facility_Available", SqlDbType.VarChar).Value = Objform.Whether_Drainage_facility_Available;
        //        cmd.Parameters.Add(new SqlParameter("@totaldevelopedlandarea_Number_sheds", SqlDbType.Int).Value = Objform.totaldevelopedlandarea_Number_sheds;
        //        cmd.Parameters.Add(new SqlParameter("@No_area_developed_plots", SqlDbType.Int).Value = Objform.No_area_developed_plots;
        //        cmd.Parameters.Add(new SqlParameter("@area_developed_plots_Number_sheds", SqlDbType.Int).Value = Objform.area_developed_plots_Number_sheds;
        //        cmd.Parameters.Add(new SqlParameter("@Number_Allocated_plots", SqlDbType.Int).Value = Objform.Number_Allocated_plots;
        //        cmd.Parameters.Add(new SqlParameter("@Allocated_plotsNumber_sheds", SqlDbType.Int).Value = Objform.Allocated_plotsNumber_sheds;
        //        cmd.Parameters.Add(new SqlParameter("@NodisputedAvailable_empty_plots_allotment", SqlDbType.Int).Value = Objform.NodisputedAvailable_empty_plots_allotment;
        //        cmd.Parameters.Add(new SqlParameter("@Access_distancenet_bus_stand", SqlDbType.Int).Value = Objform.Access_distancenet_bus_stand;
        //        cmd.Parameters.Add(new SqlParameter("@Distancekilometers_station", SqlDbType.Int).Value = Objform.Distancekilometers_station;
        //        cmd.Parameters.Add(new SqlParameter("@Whether_drinking_water_system_available", SqlDbType.VarChar).Value = Objform.Whether_drinking_water_system_available;
        //        cmd.Parameters.Add(new SqlParameter("@Whether_material_available", SqlDbType.VarChar).Value = Objform.Whether_material_available;
        //        cmd.Parameters.Add(new SqlParameter("@Whether_industrial_park_available", SqlDbType.VarChar).Value = Objform.Whether_industrial_park_available;

        //        cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = Objform.Mode;
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
           // string demo = filter_bad_chars_rep(m06.IndustrialEstateName);

            string message = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            { // parameters.Add(new SqlParameter("@Parm2", parm2));
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_InsertUpdateIndustrialEstate]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode", m06.IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateName", m06.IndustrialEstateName));
                cmd.Parameters.Add(new SqlParameter("Establishment", m06.Establishment));
                cmd.Parameters.Add(new SqlParameter("Address", m06.Address));
                cmd.Parameters.Add(new SqlParameter("DisID", m06.DistrictCode==-1?0: m06.DistrictCode));
                cmd.Parameters.Add(new SqlParameter("TehSilID", m06.TehsilCode == -1 ? 0 : m06.TehsilCode));
                cmd.Parameters.Add(new SqlParameter("BlocID", m06.BlockCode == -1 ? 0 : m06.BlockCode));
                cmd.Parameters.Add(new SqlParameter("@VillageID", m06.VillageCode == -1 ? 0 : m06.VillageCode));
                cmd.Parameters.Add(new SqlParameter("PinCode", m06.PinCode == null ? "" : m06.PinCode));
                cmd.Parameters.Add(new SqlParameter("@AreaPerSqfeet", m06.AreaPerSqfeet));
                cmd.Parameters.Add(new SqlParameter("@RatePerSqFeet", m06.RatePerSqFeet));
                cmd.Parameters.Add(new SqlParameter("PlotNo", m06.PlotNo));
                cmd.Parameters.Add(new SqlParameter("ShadeNo", m06.ShadeNo));
                cmd.Parameters.Add(new SqlParameter("NearestRailwayStationKm", m06.NearestRailwayStationKm));
                cmd.Parameters.Add(new SqlParameter("NearestBusStationKM", m06.NearestBusStationKM));
                cmd.Parameters.Add(new SqlParameter("ISStreet", m06.IsStreet == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsElectriccity", m06.IsElectriccity == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsDrainage", m06.IsDrainage == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsDrinkingWater", m06.IsDrinkingWater == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsRawMaterialsSiding", m06.IsRawMaterialsSiding == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsIndustrialPark", m06.IsIndustrialPark == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("CUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@CUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@CMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("IsDel", IsDel));//sptype
                cmd.Parameters.Add(new SqlParameter("industrytype_code_diff", m06.industrytype_code_diff));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("allotee_code", IEA.allotee_code));
                cmd.Parameters["allotee_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["allotee_code"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("allotee_name", IEA.@allotee_name));
                cmd.Parameters.Add(new SqlParameter("company_name", IEA.@company_name));
                cmd.Parameters.Add(new SqlParameter("panno", IEA.@panno));
                cmd.Parameters.Add(new SqlParameter("cinno", IEA.@cinno));
                cmd.Parameters.Add(new SqlParameter("address", IEA.@address));
                cmd.Parameters.Add(new SqlParameter("district_code_census", IEA.@district_code_census==-1?0: IEA.@district_code_census));
                cmd.Parameters.Add(new SqlParameter("tehsil_code_census", IEA.@tehsil_code_census == -1 ? 0 : IEA.@tehsil_code_census));
                cmd.Parameters.Add(new SqlParameter("block_code", IEA.@block_code == -1 ? 0 : IEA.@block_code));
                cmd.Parameters.Add(new SqlParameter("village_code", IEA.@village_code == -1 ? 0 : IEA.@village_code));
                cmd.Parameters.Add(new SqlParameter("mobile", IEA.@mobile));
                cmd.Parameters.Add(new SqlParameter("email", IEA.@email));
                cmd.Parameters.Add(new SqlParameter("industrytype_code_diff", IEA.industrytype_code_diff));
                cmd.Parameters.Add(new SqlParameter("companytype_code", IEA.@companytype_code));
                cmd.Parameters.Add(new SqlParameter("estimated_cost", IEA.@estimated_cost));
                cmd.Parameters.Add(new SqlParameter("projected_employment", IEA.@projected_employment));
                cmd.Parameters.Add(new SqlParameter("estimate_time", IEA.@estimate_time));
                cmd.Parameters.Add(new SqlParameter("related_work_experience", IEA.@related_work_experience));
                cmd.Parameters.Add(new SqlParameter("covered_area", IEA.@covered_area));
                cmd.Parameters.Add(new SqlParameter("uncovered_area", IEA.@uncovered_area));
                cmd.Parameters.Add(new SqlParameter("land_investment", IEA.@land_investment));
                cmd.Parameters.Add(new SqlParameter("other_investment", IEA.@other_investment));
                cmd.Parameters.Add(new SqlParameter("building_investment", IEA.@building_investment));
                cmd.Parameters.Add(new SqlParameter("equipment_investment", IEA.@equipment_investment));
                cmd.Parameters.Add(new SqlParameter("property_cost", IEA.property_cost));
                cmd.Parameters.Add(new SqlParameter("is_produce_smoke", IEA.is_produce_smoke));
                cmd.Parameters.Add(new SqlParameter("is_electricity", IEA.is_electricity));
                cmd.Parameters.Add(new SqlParameter("is_telephone", IEA.is_telephone));
                cmd.Parameters.Add(new SqlParameter("grossprofit", IEA.grossprofit));
                cmd.Parameters.Add(new SqlParameter("c_user_id", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("u_user_id", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("Sptype", @Sptype));
                cmd.Parameters.Add(new SqlParameter("industrial_estate_code", IEA.industrial_estate_code));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                                cmd.Parameters.Add(new SqlParameter("allotee_code", allotee_code));
                                cmd.Parameters.Add(new SqlParameter("plot_code", L01[i].@plot_code));
                                cmd.Parameters.Add(new SqlParameter("@industrial_estate_code", L01[i].@industrial_estate_code));
                                cmd.Parameters.Add(new SqlParameter("@allotmentno", L01[i].allotmentno));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", i == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                                cmd.Parameters.Add(new SqlParameter("allotee_code", allotee_code));
                                cmd.Parameters.Add(new SqlParameter("shed_code", L02[j].shed_code));
                                cmd.Parameters.Add(new SqlParameter("@industrial_estate_code", L02[j].@industrial_estate_code));
                                cmd.Parameters.Add(new SqlParameter("@allotmentno", L02[j].@allotmentno));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    transaction.Rollback();
                    message = "transaction fail";
                    return message;
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("PlotCode", AP.PlotCode));
                cmd.Parameters.Add(new SqlParameter("@tbl_IndustrialEstateCode", AP.IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("PlotSerial", AP.PlotSerial));
                cmd.Parameters.Add(new SqlParameter("PlotName", AP.PlotName));
                cmd.Parameters.Add(new SqlParameter("@PlotArea", AP.PlotArea));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Assigned", AP.IsPlot_Assigned == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Disputed", AP.IsPlot_Disputed == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("Plot_Desc", AP.Plot_Desc));
                cmd.Parameters.Add(new SqlParameter("Plot_Allottee_Desc", AP.Plot_Allottee_Desc));
                cmd.Parameters.Add(new SqlParameter("IsDel", IsDel));
                cmd.Parameters.Add(new SqlParameter("CUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@CUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@CMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateCode", IndustrialAreaCode));
                cmd.Parameters.Add(new SqlParameter("PlotCode", PlotCode));
                cmd.Parameters.Add(new SqlParameter("PlotSerialNo", PlotSerialNo));
                cmd.Parameters.Add(new SqlParameter("PlotName", PlotName));
                cmd.Parameters.Add(new SqlParameter("FromDate", FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", ToDate));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Disputed", IsPlot_Disputed));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Assigned", IsPlot_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateCode", IndustrialAreaCode));
                cmd.Parameters.Add(new SqlParameter("@ShedCode", @ShedCode));
                cmd.Parameters.Add(new SqlParameter("@ShedSerialNo", @ShedSerialNo));
                cmd.Parameters.Add(new SqlParameter("@ShedName", @ShedName));
                cmd.Parameters.Add(new SqlParameter("FromDate", FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", ToDate));
                cmd.Parameters.Add(new SqlParameter("IsShed_Disputed", IsShed_Disputed));
                cmd.Parameters.Add(new SqlParameter("IsShed_Assigned", IsShed_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@allotee_code", allotee_code));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("ShedCode", AS.ShedCode));
                cmd.Parameters.Add(new SqlParameter("tbl_IndustrialEstateCode", AS.IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("ShedSerial", AS.ShedSerial));
                cmd.Parameters.Add(new SqlParameter("ShedName", AS.ShedName));
                cmd.Parameters.Add(new SqlParameter("ShedArea", AS.ShedArea));
                cmd.Parameters.Add(new SqlParameter("IsShed_Assigned", AS.IsShed_Assigned == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("IsShed_Disputed", AS.IsShed_Disputed == true ? 'Y' : 'N'));
                cmd.Parameters.Add(new SqlParameter("Shed_Desc", AS.Shed_Desc));
                cmd.Parameters.Add(new SqlParameter("Shed_Allottee_Desc", AS.Shed_Allottee_Desc));
                cmd.Parameters.Add(new SqlParameter("CUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@CUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@CMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("IsDel", IsDel));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateName", @IndustrialEstateName));
                cmd.Parameters.Add(new SqlParameter("@DistrictID", @DistrictID));
                cmd.Parameters.Add(new SqlParameter("@TehSilID", @TehSilID));
                cmd.Parameters.Add(new SqlParameter("@BlocID", @BlocID));
                cmd.Parameters.Add(new SqlParameter("@VillageID", @VillageID));
                cmd.Parameters.Add(new SqlParameter("@PinCode", @PinCode));
                cmd.Parameters.Add(new SqlParameter("@PlotNo", @PlotNo));
                cmd.Parameters.Add(new SqlParameter("@ShadeNo", @ShadeNo));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@FromEstablishment", @FromEstablishment));
                cmd.Parameters.Add(new SqlParameter("@ToEstablishment", @ToEstablishment));
                cmd.Parameters.Add(new SqlParameter("@IndustrialType", IndustryType));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@ShedCode", @ShedCode));
                cmd.Parameters.Add(new SqlParameter("@ShedSerialNo", @ShedSerialNo));
                cmd.Parameters.Add(new SqlParameter("@ShedName", @ShedName));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@IsShed_Disputed", @IsShed_Disputed));
                cmd.Parameters.Add(new SqlParameter("@IsShed_Assigned", @IsShed_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode ", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@PlotCode", @PlotCode));
                cmd.Parameters.Add(new SqlParameter("@PlotSerialNo", @PlotSerialNo));
                cmd.Parameters.Add(new SqlParameter("@PlotName", @PlotName));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@IsPlot_Disputed", @IsPlot_Disputed));
                cmd.Parameters.Add(new SqlParameter("@IsPlot_Assigned", @IsPlot_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode ", IndustrialEstateCode));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }

        #endregion
        #region Form
        //internal string InsertUpdateMYSY(AddMYSY Objform)
        //{
        //    string result = "";
        //    try
        //    {
        //        con.Open();
        //        cmd = new SqlCommand("Proc_InsertUpdate_Swarozgar_yojana", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@district_code_census", SqlDbType.Int).Value = Objform.DistCode;
        //        cmd.Parameters.Add(new SqlParameter("@tehsil_code_census", SqlDbType.Int).Value = Objform.TehsilCode;
        //        cmd.Parameters.Add(new SqlParameter("@block_code", SqlDbType.Int).Value = Objform.BlockCode;
        //        cmd.Parameters.Add(new SqlParameter("@village_code", SqlDbType.Int).Value = Objform.VillCode;
        //        cmd.Parameters.Add(new SqlParameter("@applicant_name", SqlDbType.NVarChar).Value = Objform.applicant_name;
        //        cmd.Parameters.Add(new SqlParameter("@dob", SqlDbType.DateTime).Value = Objform.dob;
        //        cmd.Parameters.Add(new SqlParameter("@adhar_no", SqlDbType.VarChar).Value = Objform.adhar_no;
        //        cmd.Parameters.Add(new SqlParameter("@father_name", SqlDbType.NVarChar).Value = Objform.father_name;
        //        cmd.Parameters.Add(new SqlParameter("@current_address", SqlDbType.NVarChar).Value = Objform.current_address;
        //        cmd.Parameters.Add(new SqlParameter("@bank_office_address", SqlDbType.NVarChar).Value = Objform.bank_office_address;
        //        cmd.Parameters.Add(new SqlParameter("@permanent_address", SqlDbType.NVarChar).Value = Objform.permanent_address;
        //        cmd.Parameters.Add(new SqlParameter("@mobile_no", SqlDbType.VarChar).Value = Objform.mobile_no;
        //        cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar).Value = Objform.email;
        //        cmd.Parameters.Add(new SqlParameter("@pansion_card_no", SqlDbType.VarChar).Value = Objform.pansion_card_No;
        //        cmd.Parameters.Add(new SqlParameter("@family_income", SqlDbType.Decimal).Value = Objform.family_income;
        //        cmd.Parameters.Add(new SqlParameter("@project_name", SqlDbType.NVarChar).Value = Objform.project_name;
        //        cmd.Parameters.Add(new SqlParameter("@project_cost", SqlDbType.Decimal).Value = Objform.project_cost;
        //        cmd.Parameters.Add(new SqlParameter("@machinery_cost", SqlDbType.Decimal).Value = Objform.machinery_cost;
        //        cmd.Parameters.Add(new SqlParameter("@working_capital", SqlDbType.Decimal).Value = Objform.working_capital;

        //        cmd.Parameters.Add(new SqlParameter("@bank_name", SqlDbType.NVarChar).Value = Objform.bank_name;
        //        cmd.Parameters.Add(new SqlParameter("@self_share", SqlDbType.NVarChar).Value = Objform.self_share;
        //        cmd.Parameters.Add(new SqlParameter("@deposit_amt", SqlDbType.Decimal).Value = Objform.deposit_amt;
        //        //cmd.Parameters.Add(new SqlParameter("@Depositbankbranch", SqlDbType.NVarChar).Value = Objform.branch_name;
        //        cmd.Parameters.Add(new SqlParameter("@branch_name", SqlDbType.NVarChar).Value = Objform.branch_name;
        //        cmd.Parameters.Add(new SqlParameter("@bank_account_no", SqlDbType.NVarChar).Value = Objform.bank_account_no;
        //        cmd.Parameters.Add(new SqlParameter("@ifsc_code", SqlDbType.NVarChar).Value = Objform.@ifsc_code;
        //        cmd.Parameters.Add(new SqlParameter("@c_user_id", SqlDbType.VarChar).Value = Objform.c_user_id;
        //        cmd.Parameters.Add(new SqlParameter("@c_user_ip", SqlDbType.VarChar).Value = Objform.c_user_ip;
        //        cmd.Parameters.Add(new SqlParameter("@c_mac", SqlDbType.NVarChar).Value = Objform.c_mac;

        //        //cmd.Parameters.Add(new SqlParameter("@FamilyPersonName", SqlDbType.NVarChar).Value = Objform.FamilyPersonName;
        //        //cmd.Parameters.Add(new SqlParameter("@FamilyPerson_Age", SqlDbType.Int).Value = Objform.FamilyPerson_Age;
        //        //cmd.Parameters.Add(new SqlParameter("@FamilyPerson_work", SqlDbType.Int).Value = Objform.FamilyPerson_work;
        //        //cmd.Parameters.Add(new SqlParameter("@FamilyPerson_Father_Name", SqlDbType.NVarChar).Value = Objform.FamilyPerson_Father_Name;
        //        //cmd.Parameters.Add(new SqlParameter("@Mother_Name", SqlDbType.NVarChar).Value = Objform.Mother_Name;
        //        //cmd.Parameters.Add(new SqlParameter("@Mother_Age", SqlDbType.Int).Value = Objform.Mother_Age;
        //        //cmd.Parameters.Add(new SqlParameter("@Mother_work", SqlDbType.NVarChar).Value = Objform.Mother_work;
        //        //cmd.Parameters.Add(new SqlParameter("@Family_brother_sister", SqlDbType.NVarChar).Value = Objform.Family_brother_sister;
        //        //cmd.Parameters.Add(new SqlParameter("@Family_brother_sister_Age", SqlDbType.Int).Value = Objform.Family_brother_sister_Age;
        //        //cmd.Parameters.Add(new SqlParameter("@Family_brother_sister_Work", SqlDbType.NVarChar).Value = Objform.Family_brother_sister_Work;
        //        //cmd.Parameters.Add(new SqlParameter("@children_Name", SqlDbType.NVarChar).Value = Objform.children_Name;
        //        //cmd.Parameters.Add(new SqlParameter("@children_Age", SqlDbType.Int).Value = Objform.children_Age;
        //        //cmd.Parameters.Add(new SqlParameter("@children_Work", SqlDbType.Int).Value = Objform.children_Work;


        //        cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = Objform.Mode;
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
                cmd.Parameters.Add(new SqlParameter("@mode", "select"));

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

        //public string InsertYojona_Master(Yojona_Master Objform)
        //{
        //    string result = "";
        //    try
        //    {
        //        con.Open();
        //        cmd = new SqlCommand("Proc_Yojana_master", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@yojana_code", SqlDbType.Int).Value = Objform.yojana_code;
        //        cmd.Parameters.Add(new SqlParameter("@yojana_name", SqlDbType.VarChar).Value = Objform.yojana_name;
        //        cmd.Parameters.Add(new SqlParameter("@yojana_name_u", SqlDbType.NVarChar).Value = Objform.yojana_name_u;
        //        cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Char).Value = Objform.status;
        //        cmd.Parameters.Add(new SqlParameter("c_user_id", @UserSession.LoggedInUser.UserName));
        //        cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
        //        cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
        //        cmd.Parameters.Add(new SqlParameter("u_user_id", @UserSession.LoggedInUser.UserName));
        //        cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
        //        cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
        //        cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = Objform.Mode;
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
                cmd.Parameters.Add(new SqlParameter("@mode", "select"));

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

        public DataSet GetScheme_Doc(string  user_name, short scheme_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetScheme_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }



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
                            cmd.Parameters.Add(new SqlParameter("applicant_code", objdoc[j].applicant_code));
                            cmd.Parameters.Add(new SqlParameter("@doc", objdoc[j].doc));
                            cmd.Parameters.Add(new SqlParameter("@doc_type", objdoc[j].doc_type));
                            cmd.Parameters.Add(new SqlParameter("@doc_content_type", objdoc[j].@doc_content_type));

                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("@user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("@user_mac", this.MacAddress));
                            //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress);
                            // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName);
                            // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress);
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }

        public string InsertUpdateCMYSS_MachininaryDetails(List<Plant_Machinery> objdoc)
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
                            cmd.CommandText = "[Proc_InsertRegistrationMachinedetails]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("@registration_code", objdoc[j].registration_code));
                            cmd.Parameters.Add(new SqlParameter("@machine_name", objdoc[j].@machine_name));
                            cmd.Parameters.Add(new SqlParameter("@supplier", objdoc[j].@supplier));
                            cmd.Parameters.Add(new SqlParameter("@fixed_deposite", objdoc[j].@fixed_deposite));
                            cmd.Parameters.Add(new SqlParameter("@price", objdoc[j].@price));
                            cmd.Parameters.Add(new SqlParameter("@working_capital", objdoc[j].@working_capital));
                            cmd.Parameters.Add(new SqlParameter("@marketing", objdoc[j].Marketingsystem==null?"": objdoc[j].Marketingsystem));
                            cmd.Parameters.Add(new SqlParameter("@project_cost", objdoc[j].@project_cost));
                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("user_mac", this.MacAddress));
                            //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress);
                            // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName);
                            // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress);
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }

        public string InsertRegistrationFinancedetails(List<Plant_Machinery> objdoc)
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
                            cmd.CommandText = "[Proc_InsertRegistrationFinancedetails]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("@registration_code", objdoc[j].registration_code));
                            cmd.Parameters.Add(new SqlParameter("@self_share", objdoc[j].self_share));
                            cmd.Parameters.Add(new SqlParameter("@bank_loan", objdoc[j].bank_loan));
                            cmd.Parameters.Add(new SqlParameter("@margin_money", objdoc[j].margin_money));
                            
                            cmd.Parameters.Add(new SqlParameter("@total_Production", objdoc[j].@total_Production));
                            cmd.Parameters.Add(new SqlParameter("@approx_sale", objdoc[j].@approx_sale));
                            cmd.Parameters.Add(new SqlParameter("@profit", objdoc[j].@profit));
                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("user_mac", this.MacAddress));
                            //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                            // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                            // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                            cmd.CommandText = "[Proc_InsertRegistrationDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("registration_code", dt.Rows[j]["applicant_code"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@doc", dt.Rows[j]["doc"]));// 
                            cmd.Parameters.Add(new SqlParameter("@doc_type", dt.Rows[j]["doc_type"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@doc_content_type", dt.Rows[j]["doc_content_type"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("@user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("@user_mac", this.MacAddress));
                            //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                            // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                            // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("applicant_code", dt.Rows[0]["applicant_code"].ToString().Trim()));
                cmd.Parameters.Add(new SqlParameter("@tbl_applicant_doc", dt));
                // cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                cmd.Parameters.Add(new SqlParameter("@user_Id", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@user_mac", this.MacAddress));
                //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateCMYSS_Applicant(CMYSS_Applicant Objform, List<CMYSS_Applicant_Doc> objdoc, List<CMYSS_Applicant_Family> objFamily, string  @sptype)
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
                cmd.CommandText = "[Proc_InsertUpdateRegistration]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("registration_code", Objform.@applicant_code));
                cmd.Parameters["registration_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["registration_code"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@yojana_code", Objform.@yojana_code));

                cmd.Parameters.Add(new SqlParameter("@applicant_name", Objform.@applicant_name));
                cmd.Parameters.Add(new SqlParameter("@adhar_no", Objform.@adhar_no));
                cmd.Parameters.Add(new SqlParameter("@dob", Objform.@dob));
                cmd.Parameters.Add(new SqlParameter("@father_name", Objform.@Husband_father_name));
                cmd.Parameters.Add(new SqlParameter("@current_address", Objform.@current_address));
                //cmd.Parameters.Add(new SqlParameter("@proposed_office_address", Objform.proposed_office_address == null ? "" : Objform.proposed_office_address));
                cmd.Parameters.Add(new SqlParameter("@permanent_address", Objform.@permanent_address == null ? "" : Objform.permanent_address));
                cmd.Parameters.Add(new SqlParameter("@mobile_no", Objform.@mobile_no));
                cmd.Parameters.Add(new SqlParameter("@email", Objform.@email));
                //cmd.Parameters.Add(new SqlParameter("@pansion_card_no", Objform.pansion_card_No == null ? "" : Objform.pansion_card_No));
                //cmd.Parameters.Add(new SqlParameter("@project_name", Objform.@project_name == null ? "" : Objform.project_name));
                //cmd.Parameters.Add(new SqlParameter("@family_income", Objform.@family_income));
                //cmd.Parameters.Add(new SqlParameter("@project_cost", Objform.@project_cost));
                //cmd.Parameters.Add(new SqlParameter("@machinery_cost", Objform.@machinery_cost));
                //cmd.Parameters.Add(new SqlParameter("@working_capital", Objform.@working_capital));
                //cmd.Parameters.Add(new SqlParameter("@bank_name", Objform.@bank_name == null ? "" : Objform.bank_name));
                //cmd.Parameters.Add(new SqlParameter("@self_share", Objform.@self_share));
                //cmd.Parameters.Add(new SqlParameter("@deposit_amt", Objform.@deposit_amt));
                //cmd.Parameters.Add(new SqlParameter("@branch_name", Objform.@branch_name == null ? "" : Objform.branch_name));
                //cmd.Parameters.Add(new SqlParameter("@bank_account_no", Objform.@bank_account_no == null ? "" : Objform.bank_account_no));
                //cmd.Parameters.Add(new SqlParameter("@ifsc_code", Objform.@ifsc_code == null ? "" : Objform.ifsc_code));
                cmd.Parameters.Add(new SqlParameter("@district_code_census", Objform.@DistCode == -1 ? 0 : Objform.@DistCode));
                cmd.Parameters.Add(new SqlParameter("@tehsil_code_census", Objform.@TehsilCode == -1 ? 0 : Objform.@TehsilCode));
                cmd.Parameters.Add(new SqlParameter("@block_code", Objform.@BlockCode == -1 ? 0 : Objform.@BlockCode));
                cmd.Parameters.Add(new SqlParameter("@village_code", Objform.@VillCode == -1 ? 0 : Objform.@VillCode));
                cmd.Parameters.Add(new SqlParameter("UserName", ""));
                cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["UserName"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@Password", Objform.@Password));
                cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("Sptype", @sptype));
               // cmd.Parameters.Add(new SqlParameter("@Steps", Objform.steps));
                //cmd.Parameters.Add(new SqlParameter("@manufacturing", Objform.manufacturing));
                //cmd.Parameters.Add(new SqlParameter("@services", Objform.services));
                //cmd.Parameters.Add(new SqlParameter("@Is_village_bank", Objform.Is_village_bank));
                //cmd.Parameters.Add(new SqlParameter("@service_branch", Objform.service_branch));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                applicant_code = int.Parse(cmd.Parameters["registration_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();
                if (message.Contains("Save"))
                {
                    message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                }

                if (applicant_code != -1)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_InsertUpdateMYSY]";
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add(new SqlParameter("@registration_code", applicant_code));
                    cmd.Parameters.Add(new SqlParameter("@yojana_code", Objform.@yojana_code));
                    cmd.Parameters.Add(new SqlParameter("@proposed_office_address", Objform.proposed_office_address == null ? "" : Objform.proposed_office_address));
                    cmd.Parameters.Add(new SqlParameter("@pansion_card_no", Objform.pansion_card_No == null ? "" : Objform.pansion_card_No));
                    cmd.Parameters.Add(new SqlParameter("@project_name", Objform.@project_name == null ? "" : Objform.project_name));
                    cmd.Parameters.Add(new SqlParameter("@family_income", Objform.@family_income));
                    cmd.Parameters.Add(new SqlParameter("@project_cost", Objform.@project_cost));
                    cmd.Parameters.Add(new SqlParameter("@machinery_cost", Objform.@machinery_cost));
                    cmd.Parameters.Add(new SqlParameter("@working_capital", Objform.@working_capital));
                    cmd.Parameters.Add(new SqlParameter("@bank_code", Objform.bank_code));
                    cmd.Parameters.Add(new SqlParameter("@self_share", Objform.@self_share));
                    cmd.Parameters.Add(new SqlParameter("@deposit_amt", Objform.@deposit_amt));
                    cmd.Parameters.Add(new SqlParameter("@branch_code", Objform.@branch_code ));
                    cmd.Parameters.Add(new SqlParameter("@bank_account_no", Objform.@bank_account_no == null ? "" : Objform.bank_account_no));
                    cmd.Parameters.Add(new SqlParameter("@ifsc", Objform.@ifsc == null ? "" : Objform.@ifsc));
                    cmd.Parameters.Add(new SqlParameter("@Steps", Objform.steps));
                    cmd.Parameters.Add(new SqlParameter("@manufacturing", Objform.manufacturing));
                    cmd.Parameters.Add(new SqlParameter("@services", Objform.services));
                    cmd.Parameters.Add(new SqlParameter("@Is_village_bank", Objform.Is_village_bank));
                    cmd.Parameters.Add(new SqlParameter("@service_branch", Objform.service_branch));
                    cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("@sptype", @sptype));
                    cmd.Parameters.Add(new SqlParameter("Msg", ""));
                    cmd.Parameters["Msg"].Size = 256;
                    cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    string  message2 = cmd.Parameters["Msg"].Value.ToString();
                    if (objFamily != null)
                    {
                        if (objFamily.Count > 0)
                        {
                            for (int i = 0; i < objFamily.Count; i++)
                            {
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "[Proc_InsertRegistrationFamilyDetails]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.Add(new SqlParameter("@registration_code", applicant_code));
                                cmd.Parameters.Add(new SqlParameter("@relation_code", objFamily[i].@relation_code));
                                cmd.Parameters.Add(new SqlParameter("@person_name", objFamily[i].@person_name));
                                cmd.Parameters.Add(new SqlParameter("@age", objFamily[i].@age));
                                cmd.Parameters.Add(new SqlParameter("@workingfield", objFamily[i].@workingfield));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", i == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                                cmd.CommandText = "[Proc_InsertRegistrationDoc]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.Add(new SqlParameter("@registration_code", applicant_code));
                                cmd.Parameters.Add(new SqlParameter("doc_path", objdoc[j].doc));
                                cmd.Parameters.Add(new SqlParameter("@doc_type", objdoc[j].doc_type));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                                cmd.Parameters["Msg"].Size = 256;
                                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    transaction.Rollback();
                    message = "Sorry transaction fail";
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateVHPP(vhpp_Applicant Objform, string @sptype)
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
                cmd.CommandText = "[Proc_InsertUpdateRegistration]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("registration_code", Objform.@applicant_code));
                cmd.Parameters["registration_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["registration_code"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@yojana_code", Objform.@yojana_code));
                cmd.Parameters.Add(new SqlParameter("@applicant_name", Objform.@applicant_name));
                cmd.Parameters.Add(new SqlParameter("@adhar_no", Objform.@adhar_no));
                cmd.Parameters.Add(new SqlParameter("@dob", Objform.@dob));
                cmd.Parameters.Add(new SqlParameter("@father_name", Objform.@Husband_father_name));
                cmd.Parameters.Add(new SqlParameter("@current_address", Objform.@current_address));
                cmd.Parameters.Add(new SqlParameter("@permanent_address", Objform.@permanent_address == null ? "" : Objform.permanent_address));
                cmd.Parameters.Add(new SqlParameter("@mobile_no", Objform.@mobile_no));
                cmd.Parameters.Add(new SqlParameter("@email", Objform.@email));
                cmd.Parameters.Add(new SqlParameter("@district_code_census", Objform.@DistCode == -1 ? 0 : Objform.@DistCode));
                cmd.Parameters.Add(new SqlParameter("@tehsil_code_census", Objform.@TehsilCode == -1 ? 0 : Objform.@TehsilCode));
                cmd.Parameters.Add(new SqlParameter("@block_code", Objform.@BlockCode == -1 ? 0 : Objform.@BlockCode));
                cmd.Parameters.Add(new SqlParameter("@village_code", Objform.@VillCode == -1 ? 0 : Objform.@VillCode));
                cmd.Parameters.Add(new SqlParameter("UserName", ""));
                cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["UserName"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@Password", Objform.@Password));
                cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("Sptype", @sptype));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                applicant_code = int.Parse(cmd.Parameters["registration_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();
                if (message.Contains("Save"))
                {
                    message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                }

                if (applicant_code != -1)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_InsertUpdateVHPP]";
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add(new SqlParameter("registration_code", applicant_code));
                    cmd.Parameters.Add(new SqlParameter("yojana_code", Objform.@yojana_code));
                    cmd.Parameters.Add(new SqlParameter("id_no", Objform.handicraft_id_no== null?"": Objform.handicraft_id_no));
                    cmd.Parameters.Add(new SqlParameter("teacher", Objform.@teacher == null ? "" : Objform.@teacher));
                    cmd.Parameters.Add(new SqlParameter("artwork_name", Objform.Artwork_name == null ? "" : Objform.Artwork_name));
                    cmd.Parameters.Add(new SqlParameter("artwork_subject", Objform.Artwork_subject == null ? "" : Objform.Artwork_subject));
                    cmd.Parameters.Add(new SqlParameter("artwork_price", Objform.Artwork_price));
                    cmd.Parameters.Add(new SqlParameter("architect_experience", Objform.architect_experience));
                    cmd.Parameters.Add(new SqlParameter("artwork_start_date", Objform.Artwork_start_date));
                    cmd.Parameters.Add(new SqlParameter("artwork_to_date", Objform.Artwork_to_date));
                    cmd.Parameters.Add(new SqlParameter("award_count", Objform.Award_count));
                    cmd.Parameters.Add(new SqlParameter("craft_name", Objform.craft_name == null ? "" : Objform.craft_name));
                    cmd.Parameters.Add(new SqlParameter("participation_count", Objform.participation_count));
                    cmd.Parameters.Add(new SqlParameter("steps", Objform.steps));
                    cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("@sptype", @sptype));
                    cmd.Parameters.Add(new SqlParameter("Msg", ""));
                    cmd.Parameters["Msg"].Size = 256;
                    cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    string message2 = cmd.Parameters["Msg"].Value.ToString();
                    
                }
                else
                {
                    transaction.Rollback();
                    message = "Sorry transaction fail";
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateSPY(SPY_Applicant Objform, string @sptype)
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
                cmd.CommandText = "[Proc_InsertUpdateRegistration]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("registration_code", Objform.@applicant_code));
                cmd.Parameters["registration_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["registration_code"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@yojana_code", Objform.@yojana_code));
                cmd.Parameters.Add(new SqlParameter("@applicant_name", Objform.@applicant_name));
                cmd.Parameters.Add(new SqlParameter("@adhar_no", Objform.@adhar_no));
                cmd.Parameters.Add(new SqlParameter("@dob", Objform.@dob));
                cmd.Parameters.Add(new SqlParameter("@father_name", Objform.@Husband_father_name));
                cmd.Parameters.Add(new SqlParameter("@current_address", Objform.@current_address));
                cmd.Parameters.Add(new SqlParameter("@permanent_address", Objform.@permanent_address == null ? "" : Objform.permanent_address));
                cmd.Parameters.Add(new SqlParameter("@mobile_no", Objform.@mobile_no));
                cmd.Parameters.Add(new SqlParameter("@email", Objform.@email));
                cmd.Parameters.Add(new SqlParameter("@district_code_census", Objform.@DistCode == -1 ? 0 : Objform.@DistCode));
                cmd.Parameters.Add(new SqlParameter("@tehsil_code_census", Objform.@TehsilCode == -1 ? 0 : Objform.@TehsilCode));
                cmd.Parameters.Add(new SqlParameter("@block_code", Objform.@BlockCode == -1 ? 0 : Objform.@BlockCode));
                cmd.Parameters.Add(new SqlParameter("@village_code", Objform.@VillCode == -1 ? 0 : Objform.@VillCode));
                cmd.Parameters.Add(new SqlParameter("UserName", ""));
                cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["UserName"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@Password", Objform.@Password));
                cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("Sptype", @sptype));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                applicant_code = int.Parse(cmd.Parameters["registration_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();
                if (message.Contains("Save"))
                {
                    message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                }

                if (applicant_code != -1)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_InsertUpdateSPY]";
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add(new SqlParameter("registration_code", applicant_code));
                    cmd.Parameters.Add(new SqlParameter("@scheme_code", Objform.@yojana_code));
                    cmd.Parameters.Add(new SqlParameter("architect_name", Objform.working_craftwork==null?"": Objform.working_craftwork));
                    cmd.Parameters.Add(new SqlParameter("@artisian_no", Objform.artisian_no == null ? "" : Objform.artisian_no));
                    cmd.Parameters.Add(new SqlParameter("@awards", Objform.awards == null ? "" : Objform.awards));
                    cmd.Parameters.Add(new SqlParameter("@handicraft_work", Objform.handicraft_work == null ? "" : Objform.handicraft_work));
                    cmd.Parameters.Add(new SqlParameter("@bank_code", Objform.bank_code));
                    cmd.Parameters.Add(new SqlParameter("@branch_code", Objform.branch_code));
                    cmd.Parameters.Add(new SqlParameter("@bank_account_no", Objform.bank_account_no == null ? "" : Objform.bank_account_no));
                    cmd.Parameters.Add(new SqlParameter("@ifsc", Objform.ifsc == null ? "" : Objform.ifsc));
                    cmd.Parameters.Add(new SqlParameter("steps", Objform.steps));
                    cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("@sptype", @sptype));
                    cmd.Parameters.Add(new SqlParameter("Msg", ""));
                    cmd.Parameters["Msg"].Size = 256;
                    cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    string message2 = cmd.Parameters["Msg"].Value.ToString();

                }
                else
                {
                    transaction.Rollback();
                    message = "Sorry transaction fail";
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateMHPY(MHPY_Applicant Objform, string @sptype)
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
                cmd.CommandText = "[Proc_InsertUpdateRegistration]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("registration_code", Objform.@applicant_code));
                cmd.Parameters["registration_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["registration_code"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@yojana_code", Objform.@yojana_code));
                cmd.Parameters.Add(new SqlParameter("@applicant_name", Objform.@applicant_name));
                cmd.Parameters.Add(new SqlParameter("@adhar_no", Objform.@adhar_no));
                cmd.Parameters.Add(new SqlParameter("@dob", Objform.@dob));
                cmd.Parameters.Add(new SqlParameter("@father_name", Objform.@Husband_father_name));
                cmd.Parameters.Add(new SqlParameter("@current_address", Objform.@current_address));
                cmd.Parameters.Add(new SqlParameter("@permanent_address", Objform.@permanent_address == null ? "" : Objform.permanent_address));
                cmd.Parameters.Add(new SqlParameter("@mobile_no", Objform.@mobile_no));
                cmd.Parameters.Add(new SqlParameter("@email", Objform.@email));
                cmd.Parameters.Add(new SqlParameter("@district_code_census", Objform.@DistCode == -1 ? 0 : Objform.@DistCode));
                cmd.Parameters.Add(new SqlParameter("@tehsil_code_census", Objform.@TehsilCode == -1 ? 0 : Objform.@TehsilCode));
                cmd.Parameters.Add(new SqlParameter("@block_code", Objform.@BlockCode == -1 ? 0 : Objform.@BlockCode));
                cmd.Parameters.Add(new SqlParameter("@village_code", Objform.@VillCode == -1 ? 0 : Objform.@VillCode));
                cmd.Parameters.Add(new SqlParameter("UserName", ""));
                cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["UserName"].Size = 256;
                cmd.Parameters.Add(new SqlParameter("@Password", Objform.@Password));
                cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("Sptype", @sptype));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                applicant_code = int.Parse(cmd.Parameters["registration_code"].Value.ToString());
                message = cmd.Parameters["Msg"].Value.ToString();
                if (message.Contains("Save"))
                {
                    message = message + "/" + cmd.Parameters["UserName"].Value.ToString();
                }

                if (applicant_code != -1)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_InsertUpdateMHPY]";
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.Add(new SqlParameter("registration_code", applicant_code));
                    cmd.Parameters.Add(new SqlParameter("scheme_code", Objform.@yojana_code));
                    cmd.Parameters.Add(new SqlParameter("anual_income", Objform.anual_income));
                    cmd.Parameters.Add(new SqlParameter("architect_name", Objform.architect_name==null ? "": Objform.architect_name ));
                    cmd.Parameters.Add(new SqlParameter("@artisian_no", Objform.artisian_no == null ? "" : Objform.artisian_no));
                    cmd.Parameters.Add(new SqlParameter("@awards", Objform.awards == null ? "" : Objform.awards));
                    cmd.Parameters.Add(new SqlParameter("@handicraft_work", Objform.handicraft_work == null ? "" : Objform.handicraft_work));
                    cmd.Parameters.Add(new SqlParameter("@bank_code", Objform.bank_code));
                    cmd.Parameters.Add(new SqlParameter("@branch_code", Objform.branch_code));
                    cmd.Parameters.Add(new SqlParameter("@bank_account_no", Objform.bank_account_no == null ? "" : Objform.bank_account_no));
                    cmd.Parameters.Add(new SqlParameter("@ifsc", Objform.ifsc == null ? "" : Objform.ifsc));
                    cmd.Parameters.Add(new SqlParameter("steps", Objform.steps));
                    cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("c_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("c_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("u_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("u_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("u_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("@sptype", @sptype));
                    cmd.Parameters.Add(new SqlParameter("Msg", ""));
                    cmd.Parameters["Msg"].Size = 256;
                    cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    string message2 = cmd.Parameters["Msg"].Value.ToString();

                }
                else
                {
                    transaction.Rollback();
                    message = "Sorry transaction fail";
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateCMYSS_Applicantfamily(List<CMYSS_Applicant_Family> objFamily, string PensionCard, decimal family_income)
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
                                cmd.CommandText = "[Proc_InsertRegistrationFamilyDetails]";
                                cmd.CommandTimeout = 3600;
                                cmd.Parameters.Add(new SqlParameter("@registration_code", objFamily[i].applicant_code));
                                cmd.Parameters.Add(new SqlParameter("@relation_code", objFamily[i].@relation_code));
                                cmd.Parameters.Add(new SqlParameter("@person_name", objFamily[i].@person_name));
                                cmd.Parameters.Add(new SqlParameter("@age", objFamily[i].@age));
                                cmd.Parameters.Add(new SqlParameter("@workingfield", objFamily[i].@workingfield));
                                cmd.Parameters.Add(new SqlParameter("@pansion_card_no", PensionCard));
                                cmd.Parameters.Add(new SqlParameter("@family_income", family_income));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", i == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                    //            cmd.Parameters.Add(new SqlParameter("applicant_code", applicant_code);
                    //            cmd.Parameters.Add(new SqlParameter("doc_path", objdoc[j].doc);
                    //            cmd.Parameters.Add(new SqlParameter("@doc_type", objdoc[j].doc_type);

                    //            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false);
                    //            cmd.Parameters.Add(new SqlParameter("Msg", "");
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
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public DataSet GetApplicantinfo_MYSY(long registration_code, short scheme_code, string user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(); 
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetRegistrationDetails_MYSY]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@registration_code ", registration_code));
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public DataSet GetApplicantinfo_spy(long registration_code, short scheme_code, string user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetRegistrationDetails_spy]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@registration_code ", registration_code));
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public DataSet GetApplicantinfo_vhpp(long registration_code, short scheme_code, string user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetRegistrationDetails_VHPP]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@registration_code ", registration_code));
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public DataSet GetApplicant(long applicant_code, string yojana_code, string applicant_user_name)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetApplicant]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("applicant_code ", applicant_code));
                cmd.Parameters.Add(new SqlParameter("@yojana_code", yojana_code));
                cmd.Parameters.Add(new SqlParameter("@applicant_user_name", applicant_user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                            cmd.CommandText = "[Proc_InsertRegistrationDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("@registration_code", objdoc[j].applicant_code));
                            cmd.Parameters.Add(new SqlParameter("doc_path", objdoc[j].doc));
                            cmd.Parameters.Add(new SqlParameter("@doc_type", objdoc[j].doc_type));
                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("request_code", objER.request_code));
                cmd.Parameters["request_code"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["request_code"].Size = 256;
               
                cmd.Parameters.Add(new SqlParameter("division_code", objER.division_code));
                cmd.Parameters.Add(new SqlParameter("@industrial_estate_code", objER.@industrial_estate_code));
                cmd.Parameters.Add(new SqlParameter("request_no", ""));
                cmd.Parameters.Add(new SqlParameter("@c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("@c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@u_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("@u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@u_mac", this.MacAddress));
               
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                                cmd.Parameters.Add(new SqlParameter("@request_code", request_code));
                                cmd.Parameters.Add(new SqlParameter("@plot_code", objRP[i].plot_code));
                                cmd.Parameters.Add(new SqlParameter("IsFirst", i == 0 ? true : false));
                                cmd.Parameters.Add(new SqlParameter("@user_id", ""));
                                cmd.Parameters.Add(new SqlParameter("@mac", this.MacAddress));
                                cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                                cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
                    cmd.Parameters.Add(new SqlParameter("@request_code", request_code));
                    cmd.Parameters.Add(new SqlParameter("estate_applicant_code", objApp.estate_applicant_code));
                    cmd.Parameters["estate_applicant_code"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["estate_applicant_code"].Size = 256;
                    cmd.Parameters.Add(new SqlParameter("@estate_applicant_name", objApp.@estate_applicant_name));
                    cmd.Parameters.Add(new SqlParameter("company_name", objApp.company_name == null ? "" : objApp.company_name));
                    cmd.Parameters.Add(new SqlParameter("panno", objApp.panno == null ? "" : objApp.panno));
                    cmd.Parameters.Add(new SqlParameter("cinno", objApp.cinno == null ? "" : objApp.cinno));
                    cmd.Parameters.Add(new SqlParameter("address", objApp.address));
                    cmd.Parameters.Add(new SqlParameter("district_code_census", objApp.district_code_census == -1 ? 0 : objApp.district_code_census));
                    cmd.Parameters.Add(new SqlParameter("tehsil_code_census", objApp.tehsil_code_census == -1 ? 0 : objApp.tehsil_code_census));
                    cmd.Parameters.Add(new SqlParameter("block_code", objApp.block_code == -1 ? 0 : objApp.block_code));
                    cmd.Parameters.Add(new SqlParameter("village_code", objApp.village_code == -1 ? 0 : objApp.village_code));
                    cmd.Parameters.Add(new SqlParameter("mobile", objApp.mobile));
                    cmd.Parameters.Add(new SqlParameter("email", objApp.email));
                    cmd.Parameters.Add(new SqlParameter("industrytype_code_diff", objApp.industrytype_code_diff == null ? "" : objApp.industrytype_code_diff));
                    cmd.Parameters.Add(new SqlParameter("companytype_code", objApp.companytype_code));
                    cmd.Parameters.Add(new SqlParameter("estimated_cost", objApp.estimated_cost));
                    cmd.Parameters.Add(new SqlParameter("estimate_time", objApp.estimate_time));
                    cmd.Parameters.Add(new SqlParameter("related_work_experience", objApp.related_work_experience));
                    cmd.Parameters.Add(new SqlParameter("projected_employment", objApp.projected_employment));
                    cmd.Parameters.Add(new SqlParameter("equipment_investment", objApp.equipment_investment));
                    cmd.Parameters.Add(new SqlParameter("covered_area", objApp.covered_area));
                    cmd.Parameters.Add(new SqlParameter("uncovered_area", objApp.uncovered_area));
                    cmd.Parameters.Add(new SqlParameter("land_investment", objApp.land_investment));
                    cmd.Parameters.Add(new SqlParameter("other_investment", objApp.other_investment));
                    cmd.Parameters.Add(new SqlParameter("building_investment", objApp.building_investment));
                    cmd.Parameters.Add(new SqlParameter("property_cost", objApp.property_cost));
                    cmd.Parameters.Add(new SqlParameter("is_produce_smoke", objApp.is_produce_smoke));
                    cmd.Parameters.Add(new SqlParameter("is_electricity", objApp.is_electricity));
                    cmd.Parameters.Add(new SqlParameter("is_telephone", objApp.is_telephone));
                    cmd.Parameters.Add(new SqlParameter("grossprofit", objApp.grossprofit));
                    cmd.Parameters.Add(new SqlParameter("@c_user_id", ""));
                    cmd.Parameters.Add(new SqlParameter("@c_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("@c_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("@u_user_id",""));
                    cmd.Parameters.Add(new SqlParameter("@u_user_ip", this.IpAddress));
                    cmd.Parameters.Add(new SqlParameter("@u_mac", this.MacAddress));
                    cmd.Parameters.Add(new SqlParameter("Sptype", Sptype));
                    cmd.Parameters.Add(new SqlParameter("@dob", objApp.@dob));
                    cmd.Parameters.Add(new SqlParameter("@father_name", objApp.@father_name));
                    cmd.Parameters.Add(new SqlParameter("@aadhar_no", objApp.@aadhar_no));
                    cmd.Parameters.Add(new SqlParameter("UserName", ""));
                    cmd.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["UserName"].Size = 256;
                    cmd.Parameters.Add(new SqlParameter("@Password", objApp.@Password));
                    cmd.Parameters.Add(new SqlParameter("Msg", ""));
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
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@doc_code_census", DI.doc_code_census));
                cmd.Parameters.Add(new SqlParameter("@doc_type", DI.Docment));
                cmd.Parameters.Add(new SqlParameter("@doc_code", DI.doc_code));
                cmd.Parameters.Add(new SqlParameter("@status", "A"));
                cmd.Parameters.Add(new SqlParameter("@u_doc_type", DI.u_doc_type));
                cmd.Parameters.Add(new SqlParameter("@desciption", DI.desciption));
                cmd.Parameters.Add(new SqlParameter("@u_description", DI.u_description));
                cmd.Parameters.Add(new SqlParameter("@c_user_id", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@c_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@c_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@u_user_id", @UserSession.LoggedInUser.UserName));
                cmd.Parameters.Add(new SqlParameter("@u_user_ip", this.IpAddress));
                cmd.Parameters.Add(new SqlParameter("@u_mac", this.MacAddress));
                cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar).Value = DI.Mode);
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
                cmd.Parameters.Add(new SqlParameter("@mode", "select"));

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@applicant_code", applicant_code));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
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
                cmd.Parameters.Add(new SqlParameter("@applicant_code", applicant_code));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }

        public DataTable  Getifsc(string @prefix)
        {
            DataTable ds = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_Getifsc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@prefix", @prefix));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }

    }
}