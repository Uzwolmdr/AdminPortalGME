using System.Collections.Generic;
namespace Repository.Entities.Tv
{


    public class CountryInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
    }

    public class ProvinceInfo
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Details
    {
        public string user_id { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public CountryInfo country_info { get; set; }
        public object district_info { get; set; }
        public ProvinceInfo province_info { get; set; }
    }

    public class UserInfo
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Details details { get; set; }
    }

    public class ResponseData
    {
        public UserInfo UserInfo { get; set; }
        public List<string> UserSTBS { get; set; }
    }

    public class GetUserDetailsData
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public ResponseData ResponseData { get; set; }
    }
    public class TopupPackage
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string duration { get; set; }
        public string package_sales_id { get; set; }
        public string amount_id { get; set; }
        public string package_id { get; set; }
        public string billing { get; set; }
        public string discount { get; set; }
        public string package_price { get; set; }
        public string price { get; set; }
        public string total_amount { get; set; }
    }

    public class PackageDetails
    {
        public object sername { get; set; }
        public string stb { get; set; }
        public bool new_system { get; set; }
        public string stb_type { get; set; }
        public string user_id { get; set; }
        public List<TopupPackage> topup_packages { get; set; }
    }

    public class StbPackageDetails
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public PackageDetails ResponseData { get; set; }
    }

   
    public class NetTvPaymentModel:ProductInsertLogRequest
    {
        public string CustomerAddress { get; set; }
        public string PackagePlanName { get; set; }
        public string PackageId { get; set; }
        public string DeviceId { get; set; }
        public string Json { get; set; }

    }
}
