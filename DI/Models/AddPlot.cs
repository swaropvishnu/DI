using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Models
{
    public class AddPlot
    {
        //public string DistName { get; set; }
        // public string TehsilName { get; set; }
        //  public string BlockName { get; set; }
        public string house_Name { get; set; }
        public string Plotnumber { get; set; }
        public string Plot_name { get; set; }
        public Decimal Area { get; set; }
        public bool plotallotted { get; set; }
        public bool Plotdisputed { get; set; }
        public string Plotdescription { get; set; }
        public string AllotteeDetails { get; set; }        
        public Int32 SNo { get; set; }        
        public string Estates { get; set; }
        public Int32 Plot_numberGrid { get; set; }
        public Decimal AreaGrid { get; set; }    
        public string DisputeStatusGrid { get; set; }
        public string AllocationstatusGrid { get; set; }
        public string PlotdescriptionGrid { get; set; }
        
        //public string Mode { get; set; }

        //public IList<SelectListItem> DistrictNames { get; set; }
        //public IList<SelectListItem> TehsilName { get; set; }
        //public IList<SelectListItem> BlockName { get; set; }
        // public IList<SelectListItem> VillageName { get; set; }
    }
}