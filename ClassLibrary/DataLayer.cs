using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Web;

namespace ClassLibrary
{
    public class DataLayer
    {
        
        private SqlConnection cn;
        public void Connection()
        {
            this.cn = new SqlConnection();
            if (this.cn.State == ConnectionState.Closed)
            {
                SqlConnection.ClearAllPools();
                this.cn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                this.cn.Open();

            }
        }


        public string InsertLoc(M01_Location m06, bool IsDel)
        {
            string message = "";
            this.Connection();
            new DataTable();
            SqlTransaction transaction = this.cn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[SP_InsertUpdateLocation]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("LocationID", m06.LocationID);
                cmd.Parameters.AddWithValue("Location", m06.Location);
                cmd.Parameters.AddWithValue("Establishment", m06.Establishment);
                cmd.Parameters.AddWithValue("Adress", m06.Adress);
                cmd.Parameters.AddWithValue("DisID", m06.DisID);
                cmd.Parameters.AddWithValue("TehSilID", m06.TehSilID);
                cmd.Parameters.AddWithValue("BlocID", m06.BlocID);
                cmd.Parameters.AddWithValue("ThanaID", m06.ThanaID);
                cmd.Parameters.AddWithValue("PinCode", m06.PinCode);
                cmd.Parameters.AddWithValue("SqFeet", m06.SqFeet);
                cmd.Parameters.AddWithValue("DisSqFeet", m06.DisSqFeet);
                cmd.Parameters.AddWithValue("LandNo", m06.LandNo);
                cmd.Parameters.AddWithValue("ShadeNo", m06.ShadeNo);
                cmd.Parameters.AddWithValue("NearRailway", m06.NearRailway);
                cmd.Parameters.AddWithValue("NearBus", m06.NearBus);
                cmd.Parameters.AddWithValue("ISStreet", m06.ISStreet);
                cmd.Parameters.AddWithValue("IsElect", m06.IsElect);
                cmd.Parameters.AddWithValue("IsHarvest", m06.IsHarvest);
                cmd.Parameters.AddWithValue("IsPure", m06.IsPure);
                cmd.Parameters.AddWithValue("IsRaw", m06.IsRaw);
                cmd.Parameters.AddWithValue("@IsIndust", m06.@IsIndust);
               // cmd.Parameters.AddWithValue("IsDel", IsDel);
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
            this.cn.Close();
            return message;
        }

        public DataTable GetDistict()
        {
            DataTable dt = new DataTable();
            Connection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "[SP_GetDistict]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            cn.Close();
            return dt;
        }

        public DataTable GetTehsil()
        {
            DataTable dt = new DataTable();
            Connection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "[SP_GetTehsil]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            cn.Close();
            return dt;
        }

        public DataTable GetThana()
        {
            DataTable dt = new DataTable();
            Connection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "[SP_GetThana]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            cn.Close();
            return dt;
        }

        public DataTable GetBlock()
        {
            DataTable dt = new DataTable();
            Connection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "[SP_GetBlock]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            cn.Close();
            return dt;
        }
    }
}
