using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Models
{
    public class AddShed
    {
        public string house_Name { get; set; }
        public Int32 shed_SNo { get; set;}
        public string shed_Name { get; set; }
        public Decimal Area { get; set; } 
        public string ShedDetail { get; set; }
        public string Name_Allottee { get; set; }
        public bool Shadedisputed { get; set; }
        public bool Alloted_shade { get; set; }       
        public Int32 SNo { get; set; }
        public string houseName_Grid { get; set; }
        public string Plotname_Grid { get; set; }
        public Int32 Plot_Number_Grid { get; set; }
        public Decimal Area_Grid { get; set; }
        public string DisputeStatus_Grid { get; set; }
        public string ShedDetail_Grid { get; set; }
        public string AllotedDetail_Grid { get; set; }
    }
}