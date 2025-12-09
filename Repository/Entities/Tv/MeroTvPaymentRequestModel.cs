using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Tv
{
   public class MeroTvPaymentRequestModel : ProductInsertLogRequest
    {
        public  String  CheckoutId { get; set; }
        public string CheckoutToken { get; set; }
        public string PaymentId { get; set; }
        public string Json { get; set; }

    }

    public class MeroTvBillResponse
    {
        public string checkoutId { get; set; }
        public string checkoutToken { get; set; }
        public Subscriber subscriber { get; set; }
        public List<Option> options { get; set; }
    }

    public class Subscriber
    {
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string purse { get; set; }
        public List<string> smartcards { get; set; }
    }

    public class Option
    {
        public string type { get; set; }
        public string name { get; set; }
        public List<SubOption> subOptions { get; set; }
        public string id { get; set; }
        public string subType { get; set; }
        public string price { get; set; }
    }
    public class SubOption
    {
        public string id { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public List<SubOption2> subOptions { get; set; }
    }
    public class SubOption2
    {
        public string id { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string supportedDevices { get; set; }
    }
}
