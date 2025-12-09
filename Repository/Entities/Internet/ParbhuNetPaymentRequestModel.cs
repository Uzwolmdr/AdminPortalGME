using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
    public class ParbhuNetPaymentRequestModel : ProductInsertLogRequest
    {
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string Json { get; set; }

    }

    public class ParbhuNetResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public ParbhuInternetData ResponseData { get; set; }
    }

    public class ParbhuInternetData
    {
        public bool status { get; set; }
        public string code { get; set; }
        public object message { get; set; }
        public ParbhuInternetDetails details { get; set; }
    }

    public class ParbhuInternetDetails
    {
        public ParbhuInternetCustomer customer { get; set; }
        public ParbhuInternetCurrent_Subscription current_subscription { get; set; }
        public ParbhuInternetDue due { get; set; }
        public ParbhuInternetPackage[] packages { get; set; }
    }
    public class ParbhuInternetCustomer
    {
        public string username { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
    }

    public class ParbhuInternetCurrent_Subscription
    {
        public string package { get; set; }
        public string end_date { get; set; }
        public string status { get; set; }
    }

    public class ParbhuInternetDue
    {
        public string amount { get; set; }
        public string message { get; set; }
    }

    public class ParbhuInternetPackage
    {
        public string package_id { get; set; }
        public string package_name { get; set; }
        public int amount { get; set; }
        public bool current { get; set; }
    }
}
