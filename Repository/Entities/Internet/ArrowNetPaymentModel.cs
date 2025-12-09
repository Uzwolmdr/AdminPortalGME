using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
    public class ArrowNetPaymentModel : ProductInsertLogRequest
    {
        public bool HasDue { get; set; }
        public bool AcceptAdvancePayment { get; set; }
        public string PlanName { get; set; }
        public string Duration { get; set; }
        public string Json { get; set; }

    }

    public class ArrownetBillDetail
    {
        public int status { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public Payload payload { get; set; }
        public string message { get; set; }
    }

    public class Payload
    {
        public string fullClientName { get; set; }
        public string handler { get; set; }
        public bool hasDue { get; set; }
        public bool acceptAdvancePayment { get; set; }
        public string currentPlan { get; set; }
        public int daysRemaining { get; set; }
        public List<PlanDetail> planDetails { get; set; }
    }

    public class PlanDetail
    {
        public string amount { get; set; }
        public string duration { get; set; }
    }
}
