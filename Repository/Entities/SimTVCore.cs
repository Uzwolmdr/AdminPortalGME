using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class SimTvCustomerDetail
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public int status_code { get; set; }
        public string status_txt { get; set; }
        public SimTvDetails data { get; set; }
    }
    public class SimTVRequest
    {
        public string CustomerId { get; set; }
        public string Msisdn { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        public string Flag { get; set; }
        public string Response { get; set; }
    }

    public class SimTvDetails
    {
        public object cas_id { get; set; }
        public string customer_id { get; set; }
        public string customer { get; set; }
        public string status { get; set; }
    }

    public class SimTVPaymentRequest :  ProductInsertLogRequest
    {
        public string Json { get; set; }

    }


}
