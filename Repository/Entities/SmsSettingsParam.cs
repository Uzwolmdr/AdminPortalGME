using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class SmsSettingsParam
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Url { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string TextCode { get; set; }
        public string UserName { get; set; }
        public string SmscId { get; set; }
        public string Password { get; set; }

    }

    public class OperatorDetailsRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
    }
}
