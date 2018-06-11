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
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillageCode { get; set; }
        public string PinCode { get; set; }
        public decimal AreaPerSqfeet { get; set; }
        public decimal DistancePerSqFeet { get; set; }
        public string PlotNo { get; set; }
        public string ShadeNo { get; set; }
        public decimal NearestRailwayStationKm { get; set; }
        public decimal NearestBusStationKM { get; set; }
        public bool IsStreet { get; set; }
        public bool IsElectriccity { get; set; }
        public bool IsDrainage { get; set; }
        public bool IsDrinkingWater { get; set; }
        public bool IsRawMaterialsSiding { get; set; }
        public bool IsIndustrialPark { get; set; }
        public string  industrytype_code_diff { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> TehsilName { get; set; }
        public IList<SelectListItem> BlockName { get; set; }
        public IList<SelectListItem> VillageName { get; set; }
        public IndustrialAreaMasterModal()
        {
            Establishment = DateTime.Now;
        }


    }
    public class AddShed
    {
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
        public short ComapnyTypeId { get; set; }
        public string CompanyName { get; set; }
        public long EstateAlloteeCode { get; set; }
        public string PanCard { get; set; }
        public String CinNo { get; set; }
        public string AlloteeName { get; set; }
        public string Address { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> TehsilName { get; set; }
        public IList<SelectListItem> BlockName { get; set; }
        public IList<SelectListItem> VillageName { get; set; }
        public int DistrictCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillageCode { get; set; }
        public string Mobile { get; set; }
        public string email { get; set; }
        public IList<SelectListItem> IndustryType { get; set; }
        public short IndustryTypeId { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ProjectedEmployment { get; set; }
        public short EstimateTime { get; set; }
        public short RelatedWorkExperience { get; set; }
        public decimal  CoveredArea { get; set; }
        public decimal UnCoveredArea { get; set; }
        public decimal LandInvestment { get; set; }
        public decimal OtherInvestment { get; set; }
        public decimal BuildingInvestment { get; set; }
        public decimal EquipmentInvestment { get; set; }
        public decimal PropertyCost { get; set; }
        public bool Isproducesmoke { get; set; }
        public bool IsElectricity { get; set; }
        public bool IsTelephone { get; set; }
        public decimal  Grossprofit { get; set; }
        public IList<SelectListItem> IndustrialEstate { get; set; }
        public IList<SelectListItem> PlotName { get; set; }
        public IList<SelectListItem> ShedName { get; set; }
        public IList<SelectListItem> ShedArea { get; set; }
        public IList<SelectListItem> PlotArea { get; set; }
        public string AlotmentNo { get; set; }
    }
    
}