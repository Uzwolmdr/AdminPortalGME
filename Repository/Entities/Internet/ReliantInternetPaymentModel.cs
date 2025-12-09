using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
    public class ReliantInternetPaymentModel : ProductInsertLogRequest
    {
        public string Address { get; set; }
        public string Json { get; set; }

    }
}
