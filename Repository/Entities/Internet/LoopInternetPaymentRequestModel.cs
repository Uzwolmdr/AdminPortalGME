using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
   public class LoopInternetPaymentRequestModel : ProductInsertLogRequest
    {
        public string Json { get; set; }
        public string UserId { get; set; }
    }
}
