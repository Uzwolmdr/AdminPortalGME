using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class SmsGateWayRequestParam
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string SmscId { get; set; }
        public string Message { get; set; }
        public string Operator { get; set; }
        public string OperatorUrl { get; set; }
        public  int  ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Charset { get; set; }
        public bool DlrRequired { get; set; }
        public string SmsRowId { get; set; }
    }
   
}
