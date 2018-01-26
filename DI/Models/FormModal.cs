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
        public string ApplicationFee { get; set; }
        public string ApplicationFeedetails { get; set; }
        public string RegistraionNo { get; set; }
        public string RegistrationDate { get; set; }
        public string Product { get; set; }
        public string FiananceDetails { get; set; }
        public string RowMeterialSource { get; set; }
      


    }
    
}