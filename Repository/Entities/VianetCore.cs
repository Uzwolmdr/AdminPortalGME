using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    
    public class VianetRequest
    {
        public string Msisdn { get; set; }
        public string CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
    }
    public class VianetResponse
    {
        public Profile Profile { get; set; }
        public string Status { get; set; }
        public List<Bill> Billresponse { get; set; }
        public string Msg { get; set; }
    }
    public class Bill
    {
        public string payment_id { get; set; }
        public string service_name { get; set; }
        public string bill_date { get; set; }
        public string service_details { get; set; }
        public string amount { get; set; }
    }
    public class Profile
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
    }
    public class VianetBills
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public VianetResponse VianetResponse { get; set; }
    }
    public class VianetPaymentRequest:ProductInsertLogRequest
    {
        public string PaymentId { get; set; }
        public string Json { get; set; }
    }
}
