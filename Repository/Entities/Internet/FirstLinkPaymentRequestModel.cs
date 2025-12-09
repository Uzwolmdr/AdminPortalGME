using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
   public class FirstLinkPaymentRequestModel : ProductInsertLogRequest
    {
        public string PackagePlanName { get; set; }
        public string PackageId { get; set; }
        public string UserId { get; set; }
        public string Json { get; set; }
    }
}
