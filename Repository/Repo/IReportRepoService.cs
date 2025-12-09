using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.LoadWallet;
using Repository.Entities.Utility;
using System.Collections.Generic;

namespace Repository.Repo
{
    public interface IReportRepoService
    {
        
        BulkSmsReportResponse GetBulkSmsStatus(ReportRequestParam request);
        SmsBillingInfoResponse GetSmsBillingInfo(ReportRequestParam request);
        DropDownResponseWrapper GetDropDownInfo(DropDownRequest request);
        StatusResponse GetTransactionStatus(StatusRequest request);
        DashboardResponse GetCurrentTxnReport(DashboardRequestCore request);
        SummaryReportResponse GetSummaryReport(ReportRequestParam request);
        ReportResponse GetSummaryDetailReport(DetailReportRequest request);
        ReportResponse GetTransactionReport(ReportRequest request);
        ExportAllTranReportResp ExportAllTranReport(ReportRequest request);
        SmsSummaryReportResponse GetSmsSummaryReport(ReportRequestParam request);
        ExportAllSmsReport ExportGuiSmsReport(ReportRequestParam request);
        SmsReportResponse GetGuiSmsReport(ReportRequestParam request);
        ReportResponse GetReconcileReport(ReportRequest request);
        ExportAllTranReportResp ExportAllReconcileReport(ReportRequest request);
        ExportSmsBillingInfo ExportSmsBillingInfo(ReportRequestParam model);
        ProductWiseReportResponse ProductWiseReport(ProductWiseRequestModel request);
        List<AirLinesSettlemetDetails> GetAirLinesSettlementReport(ReportRequest request);
        List<ExportAirLinesSettlemetDetails> ExportAirLinesSettlementReport(ReportRequest request);
        List<SendDomesticRemittanceReportModel> GetDomesticRemittanceReport(ReportRequest request);
        List<ExportSendDomesticRemittanceReportModel> ExportDomesticRemittanceReport(ReportRequest request);
    }
}
