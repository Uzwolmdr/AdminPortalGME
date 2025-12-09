using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
    public class TechMindsPaymentRequestModel : ProductInsertLogRequest
    {
        public string Json { get; set; }
        public string UserId { get; set; }
        public string Duration { get; set; }


    }

    public class Properties
    {
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string mobilephone { get; set; }
        public string expiration { get; set; }
        public int previousbalance { get; set; }
        public string monthlycharge { get; set; }
    }

    public class TechMindsResponseFromEcom
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string requestId { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public Dictionary<string, string> Amount { get; set; }
        public Properties properties { get; set; }
    }
    public class TechMindsResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string requestId { get; set; }
        public List<Plan> PlanList { get; set; }
        public Properties properties { get; set; }
    }
    public class Plan
    {
        public string Name { get; set; }
        public string Amount { get; set; }

    }
    public class Amount
    {
        public string text { get; set; }
        public string value { get; set; }

    }
}
