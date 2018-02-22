using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DI.Models
{
    public class FormModal
    {
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserNameHindi { get; set; }
        [Required]
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public byte[] UserImage { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public string IndrustryName { get; set; }
        public Decimal ApplicationFee { get; set; }
        public string ApplicationFeedetails { get; set; }
        public DateTime ApplicationFeeDate { get; set; }
        public bool IsPreRegistered { get; set; }
        public Int64 OldRegistraionNo { get; set; }
        public DateTime OldRegistrationDate { get; set; }
        public string ProductName { get; set; }
        public string FiananceDetails { get; set; }
        public string RowMeterialSource { get; set; }
        public string PracticalProjectReport { get; set; }
        public string HelpFromForegin { get; set; }
        public string Mode { get; set; }

        public string Districtcodecensus { get; set; }
        public string Industrycodecensus { get; set; }

        public string Stablishyear { get; set; }
        public string Area { get; set; }
        public string CurrentRate { get; set; }
    }




}