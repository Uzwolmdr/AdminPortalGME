using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.BulkSMS
{
    public class BulkSmsCore
    {
        public string JsonString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string FileName { get; set; }
    }

    public class BulkSmsCoreViaApi
    {
        public List<SaveSameMessageDetails> SmsDetails { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Date { get; set; }
        public string BatchId { get; set; }
        public string Branch { get; set; }
    }

    public class SaveSameMessageDetails
    {
        
        public  string ReceiverNo { get; set; }
        public string Message { get; set; }


    }
    public class SaveSameMessageBulkSMS
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string BatchId { get; set; }
        public string Branch { get; set; }
        public List<SaveSameMessageDetails> ReceiverDetails { get; set; }
    }

}
