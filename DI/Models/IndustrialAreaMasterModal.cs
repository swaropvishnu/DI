using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Models
{
    public class IndustrialAreaMasterModal
    {
        public int IndustrialEstateCode { get; set; }
        public string IndustrialEstateName { get; set; }
        public DateTime Establishment { get; set; }
        public string  EditEstablishment { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillageCode { get; set; }
        public string PinCode { get; set; }
        public decimal AreaPerSqfeet { get; set; }
        public decimal RatePerSqFeet { get; set; }
        public int PlotNo { get; set; }
        public int ShadeNo { get; set; }
        public decimal NearestRailwayStationKm { get; set; }
        public decimal NearestBusStationKM { get; set; }
        public bool IsStreet { get; set; }
        public bool IsElectriccity { get; set; }
        public bool IsDrainage { get; set; }
        public bool IsDrinkingWater { get; set; }
        public bool IsRawMaterialsSiding { get; set; }
        public bool IsIndustrialPark { get; set; }
        public string industrytype_code_diff { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> TehsilName { get; set; }
        public IList<SelectListItem> BlockName { get; set; }
        public IList<SelectListItem> VillageName { get; set; }
        public IList<SelectListItem> IndustryType { get; set; }
        public IndustrialAreaMasterModal()
        {
            Establishment = DateTime.Now;
        }
    }
    public class AddShed
    {
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> IndustrialEstate { get; set; }
        public long ShedCode { get; set; }
        public string IndustrialEstateCode { get; set; }
        public string ShedSerial { get; set; }
        public string ShedName { get; set; }
        public decimal ShedArea { get; set; }
        public bool IsShed_Assigned { get; set; }
        public bool IsShed_Disputed { get; set; }
        public string Shed_Desc { get; set; }
        public string Shed_Allottee_Desc { get; set; }
    }
    public class AddPlot
    {
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> IndustrialEstate { get; set; }
        public long PlotCode { get; set; }
        public long SrNo { get; set; }
        public string IndustrialEstateCode { get; set; }
        public string PlotSerial { get; set; }
        public string PlotName { get; set; }
        public decimal PlotArea { get; set; }
        public bool IsPlot_Assigned { get; set; }
        public bool IsPlot_Disputed { get; set; }
        public string Plot_Desc { get; set; }
        public string Plot_Allottee_Desc { get; set; }
        public string EntryDate { get; set; }
        //public static List<AddPlot> getplot()
        //{
        //    DataTable dt = new DAL.CommonDA().Getplot(-1, -1, "", "", BLL.CommonBL.Setdate("01/01/1990"), DateTime.Now, "", "");
        //    List<AddPlot> plot = new List<AddPlot>();
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {


        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                AddPlot U = new AddPlot();
        //                U.PlotCode = long.Parse(dt.Rows[i]["PlotCode"].ToString().Trim());
        //                U.tbl_IndustrialAreaCode = dt.Rows[i]["tbl_IndustrialAreaCode"].ToString().Trim();
        //                U.PlotSerial = dt.Rows[i]["PlotSerial"].ToString().Trim();
        //                U.PlotName = dt.Rows[i]["PlotName"].ToString().Trim();
        //                U.PlotArea = decimal.Parse(dt.Rows[i]["Plot_Area"].ToString().Trim());
        //                U.SrNo = i + 1;
        //                U.IsPlot_Assigned = dt.Rows[i]["IsPlot_Assigned"].ToString().Trim() == "Y" ? true : false;
        //                U.IsPlot_Disputed = dt.Rows[i]["IsPlot_Assigned"].ToString().Trim() == "Y" ? true : false;
        //                U.Plot_Desc = dt.Rows[i]["Plot_Desc"].ToString().Trim();
        //                U.Plot_Allottee_Desc = dt.Rows[i]["Plot_Allottee_Desc"].ToString().Trim();
        //                U.EntryDate = dt.Rows[i]["EntryDate1"].ToString().Trim();

        //                plot.Add(U);
        //            }

        //        }

        //    }
        //    return plot;
        //}
    }
    public class IndustrialEstateAllotee 
    {
        public IList<SelectListItem> ComapnyType { get; set; }
       public IList<SelectListItem> DistrictNames { get; set; }
       public IList<SelectListItem> TehsilName { get; set; }
      public IList<SelectListItem> BlockName { get; set; }
      public IList<SelectListItem> VillageName { get; set; }
        public IList<SelectListItem> IndustrialEstate { get; set; }
        public IList<SelectListItem> PlotName { get; set; }
        public IList<SelectListItem> ShedName { get; set; }
        public IList<SelectListItem> IndustryType { get; set; }
        public long allotee_code { get; set; }
        public string allotee_name { get; set; }
        public string company_name { get; set; }
        public string panno { get; set; }
        public string cinno { get; set; }
        public string address { get; set; }
        public int district_code_census { get; set; }
        public int tehsil_code_census { get; set; }
        public int block_code { get; set; }
        public int village_code { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string industrytype_code_diff { get; set; }
        public int companytype_code { get; set; }
        public decimal estimated_cost { get; set; }
        public int projected_employment { get; set; }
        public int estimate_time { get; set; }
        public int related_work_experience { get; set; }
        public decimal covered_area { get; set; }
        public decimal uncovered_area { get; set; }
        public decimal land_investment { get; set; }
        public decimal other_investment { get; set; }
        public decimal building_investment { get; set; }
        public decimal equipment_investment { get; set; }
        public decimal property_cost { get; set; }
        public bool is_produce_smoke { get; set; }
        public bool is_electricity { get; set; }
        public bool is_telephone { get; set; }
        public decimal grossprofit { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public string u_mac { get; set; }
        public string tablestring { get; set; } // Use for edit
        public int industrial_estate_code { get; set; }
        public int industrial_estate_code_distict { get; set; }// for edit purpose
    }
    public class  IndustrialEstateAlloteePlot 
    {
        public long plot_code { get; set; }
        public int industrial_estate_code { get; set; }
        public string allotmentno { get; set; }
    }
    public class IndustrialEstateAlloteeShed
    {
        public long shed_code { get; set; }
        public int industrial_estate_code { get; set; }
        public string allotmentno { get; set; }
    }

    public class Doc_type
    {
        public int doc_code_census { get; set; }
        public string Docment { get; set; }
        public string doc_type { get; set; }
        public string doc_code { get; set; }
        public string status { get; set; }
        public string u_doc_type { get; set; }
        public string desciption { get; set; }
        public string u_description { get; set; }
        public string Mode { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public string u_mac { get; set; }
    }

}
