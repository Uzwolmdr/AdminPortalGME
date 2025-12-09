using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Utility
{
    public class StatusRequest: BasicCore
    {
        public string TxnId { get; set; }
    }
    public class StatusResponse : CoreResponse
    {
        //public string Status { get; set; }
        public string Date { get; set; }
        public string Operation { get; set; }
    }
    public class BasicCore
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
    }
}
