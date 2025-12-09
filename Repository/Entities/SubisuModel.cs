using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
   public class SubisuPaymentRequest : ProductInsertLogRequest
    {
        public string PaymentFor { get; set; }
        
    }
    public class SubisuRequestV2
    {

        public string CustomerId { get; set; }
        public string Amount { get; set; }
        public string Msisdn { get; set; }
    }
    public class SubisuPaymentRequestV2 : ProductInsertLogRequest
    {
        public string PaymentFor { get; set; }
        public string Token { get; set; }
        public string ObjectId { get; set; }
        public string Json { get; set; }

    }
}
