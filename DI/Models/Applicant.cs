using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Models
{
    public class CMYSS_Applicant
    {
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> Scheme { get; set; }
        public int DistCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillCode { get; set; }
        public string inputdob { get; set; }
        public DateTime dob { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string applicant_name { get; set; }
        public long @applicant_code { get; set; }
        public short @yojana_code { get; set; }
        public string adhar_no { get; set; }
        public string Husband_father_name { get; set; }
        public string current_address { get; set; }
        public string proposed_office_address { get; set; }
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
        public string Father_Name { get; set; }
        public int Father_Age { get; set; }
        public string Father_work{ get; set; }
      
        public string Mother_Name { get; set; }
        public int Mother_Age { get; set; }
        public string Mother_work { get; set; }
        public string brother_sister_Name { get; set; }
        public int brother_sister_Age { get; set; }
        public string brother_sister_Work { get; set; }
        public string children_Name { get; set; }
        public int children_Age { get; set; }
        public string children_Work { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string manufacturing { get; set; }
        public string services { get; set; }
        public string steps { get; set; }
        public string stepsActive { get; set; }
    }


    public class CMYSS_Applicant_Family
    {
        public long @applicant_code { get; set; }
        public string  @relation_code { get; set; }
        public string @person_name { get; set; }
        public int @age { get; set; }
        public int @workingfield { get; set; }
       
    }

    public class CMYSS_Applicant_Doc
    {
        public long @applicant_code { get; set; }
        public string @doc_path { get; set; }
        public string @doc_type { get; set; }
       
    }
}