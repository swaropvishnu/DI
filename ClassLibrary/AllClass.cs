using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class M01_Location
    {
        public long LocationID { get; set; }
        public string Location { get; set; }
        public int Establishment { get; set; }
        public string Adress { get; set; }
        public int DisID { get; set; }
        public int TehSilID { get; set; }
        public int BlocID { get; set; }
        public int ThanaID { get; set; }
        public string PinCode { get; set; }
        public decimal SqFeet { get; set; }
        public decimal DisSqFeet { get; set; }
        public string LandNo { get; set; }
        public string ShadeNo { get; set; }
        public decimal NearRailway { get; set; }
        public decimal NearBus { get; set; }
        public string ISStreet { get; set; }
        public string IsElect { get; set; }
        public string IsHarvest { get; set; }
        public string IsPure { get; set; }
        public string IsRaw { get; set; }
        public string @IsIndust { get; set; }
    }
}
