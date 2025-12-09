using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
   public  class SmsSendRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string TextCode { get; set; }
        public string Branch { get; set; }
        public string ReceiverNo { get; set; }
        public string Message { get; set; }
        public string Operator { get; set; }
        public string Flag { get; set; }
        public string ReferenceId { get; set; }
        public string Response { get; set; }
        public  int Status { get; set; }
    }
}
