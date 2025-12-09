using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.BulkSMS
{
    public class JsonDetails
    {
        public string ReceiverNo { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string BatchId { get; set; }
        public string SmsLength { get; set; }
    }
}
