using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string isRawMaterialavalible { get; set; }

        public string IsDrinkingWaterAvailable { get; set; }

        public string  IsWaterOutletAvailable { get; set; }
        public string IsLightAvailble { get; set; }

        public string IsRoadAvailable { get; set; }

        public string DistanceFromTrain { get; set; }

        public string DistanceFromBus { get; set; }

    }

    public class AddMYSY
    {
        public IList<SelectListItem> DistrictNames { get; set; }
        public int DistCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillCode { get; set; }
        public DateTime dob { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string applicant_name { get; set; }
        public string adhar_no { get; set; }
        public string father_name { get; set; }
        public string current_address { get; set; }
        public string bank_office_address { get; set; }
        public string permanent_address { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public string pansion_card_No { get; set; }
        public Decimal family_income { get; set; }
        public string project_name { get; set; }
        public Decimal project_cost { get; set; }
        public Decimal machinery_cost { get; set; }
        public Decimal working_capital { get; set; }
        public string bank_name { get; set; }
        public Decimal self_share { get; set; }
        public Decimal deposit_amt { get; set; }
        public string branch_name { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc_code { get; set; }
        public string Mode { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public DateTime c_time_stamp { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public DateTime u_time_stamp { get; set; }
        public string u_mac { get; set; }

        public string FamilyPersonName { get; set; }
        public int FamilyPerson_Age { get; set; }
        public string FamilyPerson_work { get; set; }
        public string FamilyPerson_Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public int Mother_Age { get; set; }
        public string Mother_work { get; set; }
        public string Family_brother_sister { get; set; }
        public int Family_brother_sister_Age { get; set; }
        public string Family_brother_sister_Work { get; set; }
        public string children_Name { get; set; }
        public int children_Age { get; set; }
        public string children_Work { get; set; }
    }

    public class Yojona_Master
    {
        public int yojana_code { get; set; }
        public string yojana_name { get; set; }
        public string yojana_name_u { get; set; }
        public string status { get; set; }
        public string Mode { get; set; }
    }


}