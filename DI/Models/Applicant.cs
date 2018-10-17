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
        public IList<SelectListItem> bank_name { get; set; }
        public IList<SelectListItem> branch_name { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> Scheme { get; set; }
        public IList<SelectListItem> gender { get; set; }
        public IList<SelectListItem> category { get; set; }
        public IList<SelectListItem> sp_category { get; set; }
        public int DistCode { get; set; }
        public int TehsilCode { get; set; }
        public int BlockCode { get; set; }
        public int VillCode { get; set; }
        public int current_district_code_census { get; set; }
        public int current_tehsil_code_census { get; set; }
        public int current_block_code { get; set; }
        public int current_village_code { get; set; }
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
        public short bank_code { get; set; }
        public Decimal self_share { get; set; }
        public Decimal deposit_amt { get; set; }
        public int branch_code { get; set; }
        public string branch_Address { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc { get; set; }
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
        public string Father_work { get; set; }

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
        public string Is_village_bank { get; set; }
        public string service_branch { get; set; }
        public string OtherRelation { get; set; }
        public bool chkfamily { get; set; }
        public string industry_activity { get; set; }
        public int @proposed_office_district { get; set; }
        public int @proposed_office_tehsil { get; set; }
        public int @proposed_office_block { get; set; }
        public int @proposed_office_village { get; set; }
        public int @qualification_code { get; set; }
        public string @category_suffix { get; set; }
        public string @sp_category_suffix { get; set; }
        public string @gender_suffix { get; set; }
        public int @sponsoring_office_code { get; set; }
        public string @sponsoring_office { get; set; }
        public string @legaltype { get; set; }
        public string pancard { get; set; }
        public string @Is_edp_training { get; set; }
        public int @industry_code { get; set; }
        public string @edp_training_ins { get; set; }
        public string is_address_same { get; set; }
    }
    public class vhpp_Applicant
    {
        public IList<SelectListItem> bank_name { get; set; }
        public IList<SelectListItem> branch_name { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> Scheme { get; set; }
        public IList<SelectListItem> artwork_place { get; set; }
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
        public string handicraft_id_no { get; set; }
        public string Husband_father_name { get; set; }
        public string current_address { get; set; }
        public string permanent_address { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public short bank_code { get; set; }
        public int branch_code { get; set; }
        public string branch_Address { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc { get; set; }
        public string teacher { get; set; }
        public string Artwork_name { get; set; }
        public string craft_name { get; set; }
        public string Artwork_subject { get; set; }
        public decimal Artwork_price { get; set; }
        public DateTime Artwork_start_date { get; set; }
        public DateTime Artwork_to_date { get; set; }
        public DateTime architect_experience { get; set; }
        public string input_Artwork_start_date { get; set; }
        public string input_Artwork_to_date { get; set; }
        public string input_architect_experience { get; set; }
        public int Award_count { get; set; }
        public int participation_count { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public DateTime c_time_stamp { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public DateTime u_time_stamp { get; set; }
        public string u_mac { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string steps { get; set; }
        public string Description { get; set; }
        public string other_art_work_name { get; set; }
        public int @sponsoring_office_code { get; set; }
        public int current_district_code_census { get; set; }
        public int current_tehsil_code_census { get; set; }
        public int current_block_code { get; set; }
        public int current_village_code { get; set; }
        public int qualification_code { get; set; }
        public string category_suffix { get; set; }
        public string sp_category_suffix { get; set; }
        public string gender_suffix { get; set; }
        public string is_address_same { get; set; }
    }
    public class SPY_Applicant
    {
        public IList<SelectListItem> bank_name { get; set; }
        public IList<SelectListItem> branch_name { get; set; }
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
        public string working_craftwork { get; set; }
        public short work_exp { get; set; }
      
        public long @applicant_code { get; set; }
        public short @yojana_code { get; set; }
        public string adhar_no { get; set; }
        public string handicraft_id_no { get; set; }
        public string Husband_father_name { get; set; }
        public string current_address { get; set; }
        public string permanent_address { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public short bank_code { get; set; }
        public int branch_code { get; set; }
        public string branch_Address { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc { get; set; }
        public string @artisian_no { get; set; }
        public string @awards { get; set; }
        public string inputawardyear { get; set; }
        public DateTime awardyear { get; set; }
        public string @handicraft_work { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public DateTime c_time_stamp { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public DateTime u_time_stamp { get; set; }
        public string u_mac { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string steps { get; set; }
        public int @sponsoring_office_code { get; set; }
        public int current_district_code_census { get; set; }
        public int current_tehsil_code_census { get; set; }
        public int current_block_code { get; set; }
        public int current_village_code { get; set; }
        public int qualification_code { get; set; }
        public string category_suffix { get; set; }
        public string sp_category_suffix { get; set; }
        public string gender_suffix { get; set; }
        public string is_address_same { get; set; }
    }
    public class MHPY_Applicant
    {
        public IList<SelectListItem> bank_name { get; set; }
        public IList<SelectListItem> branch_name { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> Scheme { get; set; }
        public int DistCode { get; set; }
        public string @architect_name { get; set; }
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
        public string handicraft_id_no { get; set; }
        public decimal @anual_income { get; set; }
        public string Husband_father_name { get; set; }
        public string current_address { get; set; }
        public string permanent_address { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public short bank_code { get; set; }
        public int branch_code { get; set; }
        public string branch_Address { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc { get; set; }
        public string @artisian_no { get; set; }
      //  public string @awards { get; set; }
        public string @handicraft_work { get; set; }
        public string c_user_id { get; set; }
        public string c_user_ip { get; set; }
        public DateTime c_time_stamp { get; set; }
        public string c_mac { get; set; }
        public string u_user_id { get; set; }
        public string u_user_ip { get; set; }
        public DateTime u_time_stamp { get; set; }
        public string u_mac { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string steps { get; set; }
        public int @sponsoring_office_code { get; set; }
        public int current_district_code_census { get; set; }
        public int current_tehsil_code_census { get; set; }
        public int current_block_code { get; set; }
        public int current_village_code { get; set; }
        public int qualification_code { get; set; }
        public string category_suffix { get; set; }
        public string sp_category_suffix { get; set; }
        public string gender_suffix { get; set; }
        public string is_address_same { get; set; }
    }
    public class CMYSS_Applicant_Family
    {
        public long @applicant_code { get; set; }
        public string @relation_code { get; set; }
        public string @person_name { get; set; }
        public int @age { get; set; }
        public string @workingfield { get; set; }

    }
    public class CMYSS_Applicant_Doc
    {
        public long @applicant_code { get; set; }
        public byte[] @doc { get; set; }
        public string @doc_content_type { get; set; }
        public string @doc_type { get; set; }

    }
    public class estate_request
    {
        public IList<SelectListItem> zone { get; set; }
        public IList<SelectListItem> Industrialarea { get; set; }
        public long request_code { get; set; }
        public string request_no { get; set; }
        public short division_code { get; set; }
        public int industrial_estate_code { get; set; }

    }
    public class requested_plot
    {
        public long plot_code { get; set; }
        public long request_code { get; set; }
    }
    public class IndustrialEstateApplicant
    {
        public IList<SelectListItem> zone { get; set; }
        public IList<SelectListItem> Industrialarea { get; set; }
        public IList<SelectListItem> ComapnyType { get; set; }
        public IList<SelectListItem> DistrictNames { get; set; }
        public IList<SelectListItem> TehsilName { get; set; }
        public IList<SelectListItem> BlockName { get; set; }
        public IList<SelectListItem> VillageName { get; set; }
        public IList<SelectListItem> IndustrialEstate { get; set; }
        public IList<SelectListItem> PlotName { get; set; }
        public IList<SelectListItem> ShedName { get; set; }
        public IList<SelectListItem> IndustryType { get; set; }
        public long @estate_applicant_code { get; set; }
        public string @estate_applicant_name { get; set; }
        public string company_name { get; set; }
        public string panno { get; set; }
        public string cinno { get; set; }
        public string address { get; set; }
        public int district_code_census { get; set; }
        public int tehsil_code_census { get; set; }
        public int block_code { get; set; }
        public int village_code { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string industrytype_code_diff { get; set; }
        public int companytype_code { get; set; }
        public decimal estimated_cost { get; set; }
        public int projected_employment { get; set; }
        public int estimate_time { get; set; }
        public int related_work_experience { get; set; }
        public decimal covered_area { get; set; }
        public decimal uncovered_area { get; set; }
        public decimal land_investment { get; set; }
        public decimal other_investment { get; set; }
        public decimal building_investment { get; set; }
        public decimal equipment_investment { get; set; }
        public decimal property_cost { get; set; }
        public bool is_produce_smoke { get; set; }
        public bool is_electricity { get; set; }
        public bool is_telephone { get; set; }
        public decimal grossprofit { get; set; }
        public string @father_name { get; set; }
        public DateTime @dob { get; set; }
        public string inputdob { get; set; }
        public string @aadhar_no { get; set; }
        public string @UserName { get; set; }
        public string @Password { get; set; }

    }
    public class IndustrialEstateApplicant_Family
    {
        public long @applicant_code { get; set; }
        public string @relation_code { get; set; }
        public string @person_name { get; set; }
        public int @age { get; set; }
        public int @workingfield { get; set; }

    }
    public class IndustrialEstateApplicant_Doc
    {
        public long @applicant_code { get; set; }
        public byte[] @doc { get; set; }
        public string @doc_content_type { get; set; }
        public string @doc_type { get; set; }

    }
    public class Plant_Machinery
    {
        public string applicant_name { get; set; }
        public string registration_code { get; set; }
        public string adhar_no { get; set; }
        public string father_name { get; set; }
        public string office_address { get; set; }
        public string address { get; set; }
        public string mobile_no { get; set; }
        public string manufacturing { get; set; }
        public string @steps { get; set; }
        public string @machine_name { get; set; }
        public string machinetable { get; set; }
        public string @supplier { get; set; }
        public string Marketingsystem { get; set; }
        public decimal fixed_deposite { get; set; }
        public decimal working_capital { get; set; }
        public decimal project_cost { get; set; }
        public decimal total_Production { get; set; }
        public decimal approx_sale { get; set; }
        public decimal profit { get; set; }
        public decimal @price { get; set; }
        public decimal @self_share { get; set; }
        public decimal @bank_loan { get; set; }
        public decimal @margin_money { get; set; }

    }
    public class ifsc
    {
        public string ifsc_code { get; set; }
        public string bank_code { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string address { get; set; }

    }
    public class uploadform
    {
        public string page { get; set; }
    }
    public class vhpp_artwork_details
    {
        public int artwork_place_code { get; set; }
        public int registration_code { get; set; }
        public string artwork_place_name { get; set; }
        public string Description { get; set; }
        public string art_work_name { get; set; }
        public string user_id { get; set; }
        public string user_ip { get; set; }
        public DateTime  time_stamp { get; set; }

    }
}