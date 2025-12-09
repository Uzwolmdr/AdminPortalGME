using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Insurance
{
     public class IMELifeRenewalPaymentDetail : ProductInsertLogRequest
    {
        public string Result { get; set; }
        public string PolicyNo { get; set; }
        public string Name { get; set; }
        public string PremiumAmount { get; set; }

        public string FineAmount { get; set; }
        public string TotalFine { get; set; }
        public string RebateAmount { get; set; }
        public string AmountToPay { get; set; }
        public string PaymentDate { get; set; }
        public string CommenceDate { get; set; }
        public string Mobile { get; set; }
        public string PlanName { get; set; }
        public string LastPremiumPaidDate { get; set; }
        public string DueDate { get; set; }
        public string PolicyStatus { get; set; }
        public string Term { get; set; }
        public string MaturityDate { get; set; }
        public string Token { get; set; }
        public string UniqueIdGuid { get; set; }
        public string Json { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }


    }

}
