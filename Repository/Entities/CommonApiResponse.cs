using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class CommonBillResponse
    {
        public object BillData { get; set; }
        public string BillJsonString { get; set; }
    }
    public class CommonOrgListResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public object Data { get; set; }
    }
    
}
