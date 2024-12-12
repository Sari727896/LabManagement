using Newtonsoft.Json;

namespace Service.DTOs
{
    public class ClientInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("custom_client_id")]
        public string CustomClientId { get; set; }

        [JsonProperty("vat_id")]
        public string VatId { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("lname")]
        public string Lname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("apt_no")]
        public string AptNo { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("bus_city")]
        public string BusCity { get; set; }

        [JsonProperty("bus_country")]
        public string BusCountry { get; set; }

        [JsonProperty("bus_no")]
        public string BusNo { get; set; }

        [JsonProperty("bus_state")]
        public object BusState { get; set; }

        [JsonProperty("bus_street")]
        public string BusStreet { get; set; }

        [JsonProperty("bus_zip")]
        public string BusZip { get; set; }

        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("cc_tokens")]
        public string CcTokens { get; set; }

        [JsonProperty("central_client_id")]
        public object CentralClientId { get; set; }

        [JsonProperty("client_type")]
        public string ClientType { get; set; }

        [JsonProperty("client_type_id")]
        public string ClientTypeId { get; set; }

        [JsonProperty("contactperson")]
        public string Contactperson { get; set; }

        [JsonProperty("crm_client_status_id")]
        public string CrmClientStatusId { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("digsig")]
        public string Digsig { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("employee_assigned")]
        public string EmployeeAssigned { get; set; }

        [JsonProperty("entrance_code")]
        public string EntranceCode { get; set; }

        [JsonProperty("exclude_nagger")]
        public string ExcludeNagger { get; set; }

        [JsonProperty("faccount")]
        public string Faccount { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("foreign_account_id")]
        public string ForeignAccountId { get; set; }

        [JsonProperty("home_city")]
        public string HomeCity { get; set; }

        [JsonProperty("home_country")]
        public object HomeCountry { get; set; }

        [JsonProperty("home_no")]
        public string HomeNo { get; set; }

        [JsonProperty("home_state")]
        public object HomeState { get; set; }

        [JsonProperty("home_street")]
        public string HomeStreet { get; set; }

        [JsonProperty("home_zip")]
        public string HomeZip { get; set; }

        [JsonProperty("hour_quota")]
        public string HourQuota { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }

        [JsonProperty("id_no")]
        public string IdNo { get; set; }

        [JsonProperty("img_url")]
        public string ImgUrl { get; set; }

        [JsonProperty("is_lead")]
        public string IsLead { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("open_credit")]
        public string OpenCredit { get; set; }

        [JsonProperty("open_credit_date")]
        public string OpenCreditDate { get; set; }

        [JsonProperty("partnershipORcompNum")]
        public string PartnershipORcompNum { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("pipeline_value")]
        public string PipelineValue { get; set; }

        [JsonProperty("po_box")]
        public string PoBox { get; set; }

        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }

        [JsonProperty("ptor_percent")]
        public string PtorPercent { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("spouse_dob")]
        public string SpouseDob { get; set; }

        [JsonProperty("spouse_name")]
        public string SpouseName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("swift_code")]
        public string SwiftCode { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("the_name")]
        public string TheName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("web_password")]
        public string WebPassword { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
    }

}
