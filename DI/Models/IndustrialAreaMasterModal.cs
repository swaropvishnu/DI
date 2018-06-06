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
        public int IndustrialAreaCode { get; set; }
        public string IndustrialAreaName { get; set; }
        public DateTime  Establishment { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string Address { get; set; }
        public int tbl_DistrictID { get; set; }
        public int tbl_TehsilID { get; set; }
        public int tbl_BlockID { get; set; }
        public int tbl_ThanaID { get; set; }
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
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> TehsilName { get; set; }
        public IList<SelectListItem> BlockName { get; set; }
        public IList<SelectListItem> VillageName { get; set; }
        public IndustrialAreaMasterModal()
        {
            Establishment = DateTime.Now;
        }


    }
    //public class IndustrialAreaMasterModal
    //{
    //    public int DistCode { get; set; }
    //    public int TehsilCode { get; set; }
    //    public int VillCode { get; set; }
    //    public int BlockCode { get; set; }
    //    public int RegionCode { get; set; }
    //    public DateTime DOB { get; set; }
    //    public string FormType { get; set; }
    //    public string DistName { get; set; }
    //    // public string TehsilName { get; set; }
    //    //  public string BlockName { get; set; }

    //    public string Established_industrial { get; set; }
    //    [Display(Name = "Date Of Birth")]
    //    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

    //    public DateTime Indrustry_start_year { get; set; }
    //    public string Address { get; set; }
    //    public int Pincode { get; set; }
    //    public Decimal distance { get; set; }        
    //    public Decimal Area { get; set; }
    //    public string Currentprevailingrate { get; set; }
    //    public string UserCurrentlynumbertotaldevelopedlandareaImage { get; set; }
    //    public string ApplicantName { get; set; }
    //    public string ApplicantAddress { get; set; }
    //    public string Currentlynumbertotaldevelopedlandarea { get; set; }
    //    public string Details_Each_plot_Availablenoncontroversial_Available_Area { get; set; }
    //    public bool Accessibility_location_available_not { get; set; }
    //    public bool Whether_electricity_Available_not { get; set; }
    //    public bool Whether_Drainage_facility_Available { get; set; }
    //    public Int32 totaldevelopedlandarea_Number_sheds { get; set; }
    //    public Int32 No_area_developed_plots { get; set; }
    //    public Int32 area_developed_plots_Number_sheds { get; set; }
    //    public Int32 Number_Allocated_plots { get; set; }
    //    public Int32 Allocated_plotsNumber_sheds { get; set; }
    //    public Int32 NodisputedAvailable_empty_plots_allotment { get; set; }
    //    public Int32 Access_distancenet_bus_stand { get; set; }
    //    public Int32 Kilometer { get; set; }
    //    public Int32 Distancekilometers_station { get; set; }
    //    public bool Whether_drinking_water_system_available { get; set; }
    //    public bool Whether_material_available { get; set; }
    //    public bool Whether_industrial_park_available { get; set; }
    //    public string Mode { get; set; }

    //    public IList<SelectListItem> DistrictNames { get; set; }
    //    public IList<SelectListItem> TehsilName { get; set; }
    //    public IList<SelectListItem> BlockName { get; set; }
    //    public IList<SelectListItem> VillageName { get; set; }
    //    public IndustrialAreaMasterModal()
    //    {
    //        Indrustry_start_year = DateTime.Now;
    //    }

    //}
    public class AddShed
    {
        public IList<SelectListItem> IndustrialArea { get; set; }
        public long ShedCode { get; set; }
        public string tbl_IndustrialAreaCode { get; set; }
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
        public IList<SelectListItem> IndustrialArea { get; set; }
        public long PlotCode { get; set; }
        public long SrNo { get; set; }
        public string tbl_IndustrialAreaCode { get; set; }
        public string PlotSerial { get; set; }
        public string PlotName { get; set; }
        public decimal PlotArea { get; set; }
        public bool IsPlot_Assigned { get; set; }
        public bool IsPlot_Disputed { get; set; }
        public string Plot_Desc { get; set; }
        public string Plot_Allottee_Desc { get; set; }
        public string EntryDate { get; set; }
        public static List<AddPlot> getplot()
        {
            DataTable dt = new DAL.CommonDA().Getplot(-1, -1, "", "", BLL.CommonBL.Setdate("01/01/1990"), DateTime.Now, "", "");
            List<AddPlot> plot = new List<AddPlot>();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddPlot U = new AddPlot();
                        U.PlotCode = long.Parse(dt.Rows[i]["PlotCode"].ToString().Trim());
                        U.tbl_IndustrialAreaCode = dt.Rows[i]["tbl_IndustrialAreaCode"].ToString().Trim();
                        U.PlotSerial = dt.Rows[i]["PlotSerial"].ToString().Trim();
                        U.PlotName = dt.Rows[i]["PlotName"].ToString().Trim();
                        U.PlotArea = decimal.Parse(dt.Rows[i]["Plot_Area"].ToString().Trim());
                        U.SrNo = i + 1;
                        U.IsPlot_Assigned = dt.Rows[i]["IsPlot_Assigned"].ToString().Trim() == "Y" ? true : false;
                        U.IsPlot_Disputed = dt.Rows[i]["IsPlot_Assigned"].ToString().Trim() == "Y" ? true : false;
                        U.Plot_Desc = dt.Rows[i]["Plot_Desc"].ToString().Trim();
                        U.Plot_Allottee_Desc = dt.Rows[i]["Plot_Allottee_Desc"].ToString().Trim();
                        U.EntryDate = dt.Rows[i]["EntryDate1"].ToString().Trim();

                        plot.Add(U);
                    }

                }

            }
            return plot;
        }
    }

}