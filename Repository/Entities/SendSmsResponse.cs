using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
   public class SendSmsResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Response { get; set; }
        public string ReferenceId { get; set; }
        public string Operator { get; set; }
        public int Status { get;  set; }
    }
}
