using Repository.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class ReportRequest:RequestCore
    {        
        public string Branch { get; set; }
        public string Msisdn { get; set; }
        public string TransactionId { get; set; }
        public string Product { get; set; }
        public string Account { get; set; }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                switch (value)
                {
                    case "0":
                        _status = "Success";
                        break;
                    case "1":
                        _status = "Failed";
                        break;
                    case "2":
                        _status = "Pending";
                        break;
                    default:                        
                        break;
                }
            }
        }
        public string Amount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Flag { get; set; }
        
    }

    public class DetailReportRequest
    {
        public string Branch { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Product { get; set; }
        public string OrganisationCode { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
    }
    public class ReportResponseDetails
    {
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string Mobile { get; set; }
        public string clientTxnId { get; set; }
        public string TransactionId { get; set; }
        public string ThirdPartyTxnId { get; set; }
        
        public string Status { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string ? ReconcileDate { get; set; }
        public string ? ReconcileBy { get; set; }
        public string CustomerId { get; set; }
        public int FilterCount { get; set; }
        public string Product { get; set; }
        public string Account { get; set; }
        public int SNo { get;  set; }
        public string ServiceProvider { get; set; }
    }

    public class ReportResponse
    {
        public List<ReportResponseDetails> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public decimal TotalAmount { get; set; }
        public string ResponseDescription { get; set; }   
        public string OrganisationCode { get; set; }

    }

    public class ExportAllTranReportResp
    {
        public List<ExportAllResponseDetails> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string OrganisationCode { get; set; }

    }

    public class ExportAllResponseDetails
    {
        public int SNo { get; set; }
        public string Product { get; set; }
        public string Date { get; set; }
        public string clientTxnId { get; set; }
        public string  TxnId { get; set; }
        public string ThirdPartyTxnId { get; set; }
        public string ServiceProvider { get; set; }
        public string Account { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string ReconcileDate { get;  set; }
        public string ReconcileBy { get; set; }
        public string OrganisationCode { get; internal set; }
    }

    public class BulkSmsReportResponse 
    {
        public List<BulkSmsReport> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class BulkSmsReport
    {
        public string BatchId { get; set; }
        public string ScheduleDate { get; set; }
        public string Status { get; set; }
        public string ExecutedDate { get; set; }
        public int FilterCount { get; internal set; }
        public int TotalSmsCount { get; set; }
        public int NoOfSms { get; set; }
        public string UserName { get; set; }
    }

    public class SmsBillingInfo
    {
        public string OrganisationCode { get; set; }
        public string BranchCode { get; set; }
        public string SmsSum { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
        public int FilterCount { get; internal set; }
    }
    public class SmsBillingInfoResponse 
    {
        public List<SmsBillingInfo> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class SummaryReportInfo
    {
        public string OrganisationCode { get; set; }
        public string BranchCode { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
    }
    public class SummaryReportResponse
    {
        public List<SummaryReportInfo> ReportDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class DashboardResponse
    {       
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSuccessAmount { get; set; }
        public int TotalSuccessCount { get; set; }
        public decimal TotalFailedAmount { get; set; }
        public int TotalFailedCount { get; set; }
        public decimal TotalSuspiciousAmount { get; set; }
        public int TotalSuspiciousCount { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public List<SmsDashboard> SuccessSmsDash { get; set; }
        public List<SmsDashboard> FailedSmsDash { get; set; }
    }

    public class SmsDashboard
    {
        public string Operator { get; set; }
        public string TotalCount { get; set; }

    }
    public class SmsReportModel
    {
        public string SmsMessage { get; set; }
        public int SmsCount { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string SendDate { get; set; }
        public int FilterCount { get; set; }
        public string ReceiverNo { get; set; }
        public string Branch { get; set; }
        public string Operator { get; set; }
        public string OrganisationCode { get; set; }
        public string RequestedDate { get; set; }



    }

    public class SmsSummaryReportModel
    {
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }

        public int SmsLength { get; set; }
        public string Status { get; set; }
        public string ReceiverNo { get; set; }
        public string Operator { get; set; }
        public string RequestedDate { get; set; }
        public string SentDate { get; set; }
    }
    public   class SmsSummaryReportResponse
    {
        public List<SmsSummaryReportModel> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class SmsReportResponse
    {
        public List<SmsReportModel> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

    }

    public class ExportAllSmsReport
    {
        public List<SmsExportDetails> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

    }

    public class SmsExportDetails
    {
        public string ReceiverNo { get; set; }
        public int SmsCount { get; set; }

        public string SendDate { get; set; }
        public string Branch { get; set; }
        public string Operator { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }

    }

    public class ExportSmsBillingInfo
    {
        public List<ExportSmsBillingDetails> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class ExportSmsBillingDetails
    {
        public string OrganisationCode { get; set; }
        public string BranchCode { get; set; }
        public string SmsCount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
    }

    public class ProductWiseReportDetail
    {
        public string TxnCount { get; set; }
        public string Amount { get; set; }
        public string Product { get; set; }
        public string Date { get; set; }
        public string OrganisationCode { get; set; }
    }
    public class ProductWiseReportResponse
    {
        public List<ProductWiseReportDetail> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

    }
    public class ProductWiseRequestModel
    {
        public string Flag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Product { get; set; }
        public string OrganisationCode { get; set; }

    }

    public class  AirLinesSettlemetDetails
    {
        public int FilterCount { get; set; }
        public int SNo { get; set; }
        public string FastConnectTxnId { get; set; }
        public string BankTxnId { get; set; }
        public string CreatedDate { get; set; }
        public string TicketDate { get; set; }
        public string AirlinesCode { get; set; }
        public string FromSector { get; set; }
        public string ToSector { get; set; }
        public string TicketClass { get; set; }
        public string Pnr { get; set; }
        public string TicketNumber { get; set; }
        public string Amount { get; set; }
        public string AirlinesCommission { get; set; }
        public string Status { get; set; }
        public string SettlementDate { get; set; }
        public string SettledTxnId { get; set; }
    }

    public class ExportAirLinesSettlemetDetails
    {
        public int SNo { get; set; }
        public string FastConnectTxnId { get; set; }
        public string BankTxnId { get; set; }
        public string CreatedDate { get; set; }
        public string TicketDate { get; set; }
        public string AirlinesCode { get; set; }
        public string FromSector { get; set; }
        public string ToSector { get; set; }
        public string TicketClass { get; set; }
        public string Pnr { get; set; }
        public string TicketNumber { get; set; }
        public string Amount { get; set; }
        public string AirlinesCommission { get; set; }
        public string Status { get; set; }
        public string SettlementDate { get; set; }
        public string SettledTxnId { get; set; }
    }

}
