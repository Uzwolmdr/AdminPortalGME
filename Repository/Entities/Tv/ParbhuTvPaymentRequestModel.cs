using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Tv
{
   public class ParbhuTvPaymentRequestModel : ProductInsertLogRequest
    {
        public string CasId { get; set; }
        public string Sessionid { get; set; }
        public string Json { get; set; }
        public string ProductId { get; set; }
        public string StockId { get; set; }
    }
    public class PrabhuTVResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public PrabhuTVDetail ResponseData { get; set; }
    }

    public class PrabhuTVDetail
    {
        public int status_code { get; set; }
        public string status_msg { get; set; }
        public string sessionid { get; set; }
        public PrabhuTVDetailCustomerDetails customer_details { get; set; }
        public PrabhuTVDetailCurrentPackages[] current_packages { get; set; }
        public PrabhuTVAvailablePackages[] available_packages { get; set; }
    }

    public class PrabhuTVDetailCustomerDetails
    {
        public string customer_id { get; set; }
        public string caf_number { get; set; }
        public string mobile_number { get; set; }
        public string stb_count { get; set; }
        public string customer_name { get; set; }
        public string balance { get; set; }
    }

    public class PrabhuTVDetailCurrentPackages
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string service_start_date { get; set; }
        public string stock_id { get; set; }
        public string serial_number { get; set; }
        public string mac_vc_number { get; set; }
        public float bill_amount { get; set; }
        public string expiry_date { get; set; }
    }



    public class PrabhuTVAvailablePackages
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string serial_number { get; set; }
        public string stock_id { get; set; }
        public string mac_vc_number { get; set; }
        public float bill_amount { get; set; }
    }
}
