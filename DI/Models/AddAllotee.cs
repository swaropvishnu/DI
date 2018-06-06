using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Models
{
    public class AddAllotee
    {
        public string house_Name { get; set; }
        public string Plot { get; set; }
        public string shed_Name { get; set; }
        public Decimal Area { get; set; }
        public bool Alloted_shade { get; set; }
        public bool Shadedisputed { get; set; }
        public string ShedDetail { get; set; }
        public string Name_Allottee { get; set; }
    }
}