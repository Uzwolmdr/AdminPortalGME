using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{

    public class WaterCounterList
    {
        public string CounterName { get; set; }
        public string CounterCode { get; set; }
        public string Product { get; set; }
        public string MerchantCode { get; set; }

    }
    public class CommunityWaterBillDetailRequest
    {
        public string Product { get; set; }
        public string CustomerId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CounterCode { get; set; }
    }

    public class DiyaloKhanepani
    {
        public class DiyaloKhanePaniBillResponse
        {
            public string code { get; set; }
            public string message { get; set; }
            public List<Statement> Statement { get; set; }
            public List<PartialStatement> PartialStatement { get; set; }

            public double grandTotal { get; set; }
            public string lastPaidMessage { get; set; }
            public string customerName { get; set; }
            public string address { get; set; }
            public string meterno { get; set; }
            public string zone { get; set; }
            public string ward { get; set; }
            public string type { get; set; }
            public int ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public string FromDate { get; set; }
            public string TillDate { get; set; }
            public string TotalPenaltyRebateAmount { get; set; }
            public string TotalBillAmount { get; set; }
        }
        public class Statement
        {
            public string desc { get; set; }
            public double amt { get; set; }
            public string date { get; set; }
        }
        public class PartialStatement
        {
            public string desc { get; set; }
            public string date { get; set; }
            public double BillAmount { get; set; }
            public double PenaltyRebateAmount { get; set; }

        }
    }
    public class H2OKhanepani
    {
        public class H2ODueBillResponse
        {
            public int ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public object Message { get; set; }
            public H2ODueBilData[] Data { get; set; }
        }
        public class H2ODueBilData
        {
            public string StatusCode { get; set; }
            public string StatusMessage { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string MobileNumber { get; set; }
            public string CurrentMonthDues { get; set; }
            public string CurrentMonthDisocunt { get; set; }
            public string CurrentMonthFine { get; set; }
            public string TotalCreditSalesAmount { get; set; }
            public string TotalAdvanceAmount { get; set; }
            public string PreviousDues { get; set; }
            public string TotalDues { get; set; }
        }
    }
    public class HeatudaKhanepani
    {
        public class HetaudaKhanepaniResponse
        {
            public int ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public string message { get; set; }
            public int sn { get; set; }
            public int accountNo { get; set; }
            public Detail detail { get; set; }
        }

        public class Detail
        {
            public string customerName { get; set; }
            public string meterType { get; set; }
            public string meterNumber { get; set; }
            public string areaNumber { get; set; }
            public string advanceAvaliable { get; set; }
            public string amountPayable { get; set; }
            public string totalBillAmount { get; set; }
            public string totalFine { get; set; }
            public string totalDiscount { get; set; }
            public Monthlydetail[] monthlyDetails { get; set; }
        }

        public class Monthlydetail
        {
            public string date { get; set; }
            public string cons { get; set; }
            public string amount { get; set; }
            public string rebate { get; set; }
            public string total { get; set; }
            public string sn { get; set; }
        }
    }
    public class HeartsunKhanepani
    {
        
        public class HeartsunDetails
        {
            public string MemID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Due { get; set; }
            public string Discount { get; set; }
            public string Charge { get; set; }
        }
        public class HeartsunResponse
        {
            public int ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public string request_id { get; set; }
            public string token { get; set; }
            public int response_code { get; set; }
            public string response_message { get; set; }
            public string amount { get; set; }
            public HeartsunDetails properties { get; set; }
        }
       
    }

    public class NepalKhanePani
    {
        public class NSWCResponse
        {
            public int ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public NSWCData Data { get; set; }
            public string PayAmount { get; set; }
            public string DueFromMonth { get; set; }
            public string DueToMonth { get; set; }
            public string SessionId { get; set; }
            public decimal ServiceCharge { get; set; }
        }

        public class NSWCData
        {
            public NSWCConsumerdetailfield consumerDetailField { get; set; }
            public NSWCDuepaymentfield[] duePaymentField { get; set; }
        }

        public class NSWCConsumerdetailfield
        {
            public string consumerIDField { get; set; }
            public string consumerNameField { get; set; }
            public string addressField { get; set; }
            public string areaCodeField { get; set; }
        }

        public class NSWCDuepaymentfield
        {
            public string yearMonthField { get; set; }
            public string billYearField { get; set; }
            public string billMonthField { get; set; }
            public string amountField { get; set; }
            public string rebateField { get; set; }
            public float penaltyField { get; set; }
        }
    }

    public class CommunityWaterPaymentRequest : ProductInsertLogRequest
    {
        public string Json { get; set; }
        public string CounterCode { get; set; }
        public string CounterName { get; set; }
    }
    public class CommunityWaterPaymentDbRequestLog: CommunityWaterPaymentRequest
    {

        public string MeterNo { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string lastPaidMessage { get; set; }
        public string organization { get; set; }

        public string PenaltyAmount { get; set; }
        public string BillAmount { get; set; } // Amount 
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Channel { get; set; }


        //H2O payment Request
        public string CurrentMonthDues { get; set; }
        public string CurrentMonthDisocunt { get; set; }
        public string CurrentMonthFine { get; set; }
        public string TotalCreditSalesAmount { get; set; }
        public string TotalAdvanceAmount { get; set; }
        public string PreviousDues { get; set; }
        public string TotalDues { get; set; }
        public string MonthSN { get; set; }


       // Heartsun
         public string RequestId { get; set; }
        public string TokenId { get; set; }

        //Hetauda
        public string AreaNumber { get; set; }
        public string MeterType { get; set; }
        public string AdvanceAvaliable { get; set; }
        public string AmountPayable { get; set; }
        public string TotalBillAmount { get; set; }
        public string TotalFine { get; set; }
        public string TotalDiscount { get; set; }
        public string PayAmount { get; set; }
        public string MonthCount { get; set; }


        //Nepal Khanepani

        public string DueFromMonth { get; set; }
        public string DueToMonth { get; set; }
        public string SessionId { get; set; }
    }
}
