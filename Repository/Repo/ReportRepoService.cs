using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.LoadWallet;
using Repository.Entities.Utility;
using Repository.Helper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repo
{
    public class ReportRepoService: IReportRepoService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<ReportRepoService>();
        decimal totalAmount = 0;
        SwiftDao dao;
        ReportDao reportDao;
        public ReportRepoService()
        {
            dao = new SwiftDao();
            reportDao = new ReportDao();
        }

        public BulkSmsReportResponse GetBulkSmsStatus(ReportRequestParam model)
        {
            var bulkSmsReportResponse = new BulkSmsReportResponse();
            List<BulkSmsReport> _list = new List<BulkSmsReport>();
            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='GetBulkSmsStatus'";
            sql += " ,@OrganisationCode= " + dao.FilterString(model.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(model.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(model.ToDate);
            sql += " ,@BatchId= " + dao.FilterString(model.Batch);
            sql += " ,@DisplayStart= " + dao.FilterQuote(model.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(model.DisplayLength);
            sql += " ,@Search= " + dao.FilterString(model.Search);
            sql += " ,@SortCol= " + dao.FilterString(model.SortCol);
            sql += " ,@SortDir= " + dao.FilterString(model.SortDir);
            Log.Information("GetBulkSmsStatus  Query :{0}", sql);
            try
            {
                var dbTable = reportDao.ExecuteDataTable(sql);
                if (dbTable != null)
                {
                 
                   
                        if (dbTable != null)
                        {
                            foreach (DataRow dr in dbTable.Rows)
                            {
                                _list.Add(new BulkSmsReport
                                {
                                    BatchId = Convert.ToString(dr["BatchId"]),
                                    ScheduleDate = String.IsNullOrEmpty(Convert.ToString(dr["DueDate"])) ? string.Empty : Convert.ToDateTime(dr["DueDate"]).ToString("yyyy-MM-dd"),                                   
                                    ExecutedDate = String.IsNullOrEmpty(Convert.ToString(dr["SentDate"])) ? string.Empty : Convert.ToDateTime(dr["SentDate"]).ToString("yyyy-MM-dd"),
                                    Status = Convert.ToString(dr["Status"]),
                                    FilterCount = Convert.ToInt32(dr["Filter_Count"]),
                                    TotalSmsCount = Convert.ToInt32(dr["SmsCount"]),
                                    NoOfSms = Convert.ToInt32(dr["NoOfSms"]),
                                    UserName = Convert.ToString(dr["Username"]),

                                });

                            }
                            bulkSmsReportResponse.ReportDetails = _list;
                        }
                    
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetBulkSmsStatus SQL execution Exception Occured:{0}", ex.Message);
                bulkSmsReportResponse.ResponseCode = ResponseCodeConstant.EXCEPTION;
                bulkSmsReportResponse.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return bulkSmsReportResponse;
        }

        public SmsBillingInfoResponse GetSmsBillingInfo(ReportRequestParam model)
        {
            SmsBillingInfoResponse response = new SmsBillingInfoResponse();
            List<SmsBillingInfo> _list = new List<SmsBillingInfo>();
            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='GetSmsBillingInfo'";
            sql += " ,@OrganisationCode= " + dao.FilterString(model.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(model.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(model.ToDate);
            sql += " ,@DisplayStart= " + dao.FilterQuote(model.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(model.DisplayLength);
            sql += " ,@Search= " + dao.FilterString(model.Search);
            sql += " ,@SortCol= " + dao.FilterString(model.SortCol);
            sql += " ,@SortDir= " + dao.FilterString(model.SortDir);
            Log.Information("GetSmsBillingInfo Query :{0}", sql);
            try
            {
                var dbTable = reportDao.ExecuteDataTable(sql);
               
                        if (dbTable != null)
                        {
                            foreach (DataRow dr in dbTable.Rows)
                            {
                                _list.Add(new SmsBillingInfo
                                {
                                    OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                                    BranchCode = Convert.ToString(dr["Branch"]),
                                    SmsSum = Convert.ToString(dr["smscount"]),
                                    Date = String.IsNullOrEmpty(Convert.ToString(dr["Date"])) ? string.Empty : Convert.ToDateTime(dr["Date"]).ToString("yyyy-MM-dd"),
                                    Status = Convert.ToString(dr["Status"]),
                                    Operator = Convert.ToString(dr["Operator"]),
                                    FilterCount = Convert.ToInt32(dr["Filter_Count"])

                                });

                            }
                            response.ReportDetails = _list;
                        }
            }
            catch (Exception ex)
            {
                Log.Error("GetSmsBillingInfo SQL execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public DropDownResponseWrapper GetDropDownInfo(DropDownRequest request)
        {
            DropDownResponseWrapper response = new DropDownResponseWrapper();
            List<DropDownResponse> _list = new List<DropDownResponse>();
            var sql = "SW_PROC_GATEWAY_REPORT";
            sql += " @Flag= " + dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            Log.Information("GetDropDownInfo :{0}", sql);
            try
            {
                var dbTable = dao.ExecuteDataTable(sql);

                if (dbTable != null)
                {
                    foreach (DataRow dr in dbTable.Rows)
                    {
                        _list.Add(new DropDownResponse
                        {
                            Text = Convert.ToString(dr["Text"]),
                            Value = Convert.ToString(dr["Value"]),

                        });

                    }
                    response.DropDownDetails = _list;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetDropDownInfo sql execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public StatusResponse GetTransactionStatus(StatusRequest request)
        {
            StatusResponse _resp = new StatusResponse();
            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='TxnStatus'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@TransactionId= " + dao.FilterString(request.TxnId);

            Log.Information("GetTransactionStatus with  Query :{0}", sql);

            try
            {
                var dr = dao.ExecuteDataRow(sql);
                if (dr != null)
                {
                        _resp.ClientTxnId = request.TxnId;
                        _resp.ExtraId = dr["TxnId"].ToString();
                        _resp.ResponseCode = Convert.ToInt32(dr["Code"].ToString());
                        _resp.ResponseDescription = dr["Msg"].ToString();
                        _resp.Date =   String.IsNullOrEmpty( Convert.ToString(dr["Created_Date"]))? "" : Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt");
                        _resp.Operation = dr["Operation"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetTransactionStatus SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }
        
        #region Dashboard Report Region Start
        public DashboardResponse GetCurrentTxnReport(DashboardRequestCore request)
        {
            DashboardResponse response = new DashboardResponse();           
            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='GetCurrentTxnReport'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Month= " + dao.FilterString(request.Month);
            Log.Information("GetCurrentTxnReport Report Query :{0}", sql);

            try
            {
               
                var dataSet = reportDao.ExecuteDataset(sql);
                var dbTran = dataSet.Tables[0];
                var dbSmsSuccess = dataSet.Tables[1];
                var dbSmsFailed = dataSet.Tables[2];
                var  smsSucessList = new List<SmsDashboard>();
                var smsFailedList = new List<SmsDashboard>();
                if (dbTran != null)
                {
                    response.TotalSuccessAmount = Convert.ToDecimal(Utilities.FormatCurrency(Convert.ToDecimal(dbTran.Rows[0]["TxnAmount"])));
                    response.TotalSuccessCount = Convert.ToInt16(dbTran.Rows[0]["TxnCount"]);

                    response.TotalFailedAmount = Convert.ToDecimal(Utilities.FormatCurrency(Convert.ToDecimal(dbTran.Rows[1]["TxnAmount"])));
                    response.TotalFailedCount = Convert.ToInt16(dbTran.Rows[1]["TxnCount"]);

                    response.TotalSuspiciousAmount = Convert.ToDecimal(Utilities.FormatCurrency(Convert.ToDecimal(dbTran.Rows[2]["TxnAmount"])));
                    response.TotalSuspiciousCount = Convert.ToInt16(dbTran.Rows[2]["TxnCount"]);

                    response.TotalAmount = response.TotalSuccessAmount+ response.TotalFailedAmount + response.TotalSuspiciousAmount;
                    response.TotalCount = response.TotalSuccessCount + response.TotalFailedCount + response.TotalSuspiciousCount;
                }
                foreach(DataRow row in dbSmsSuccess.Rows)
                {
                    smsSucessList.Add(new SmsDashboard
                    {
                        Operator = Convert.ToString(row["Operator"]),
                        TotalCount = Convert.ToString(row["SmsCount"])
                    });
                }
                foreach (DataRow row in dbSmsFailed.Rows)
                {
                    smsFailedList.Add(new SmsDashboard
                    {
                        Operator = Convert.ToString(row["Operator"]),
                        TotalCount = Convert.ToString(row["SmsCount"])
                    });
                }
                response.SuccessSmsDash = smsSucessList;
                response.FailedSmsDash = smsFailedList;

            }
            catch (Exception ex)
            {
                Log.Error("GetCurrentTxnReport SQL execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }
        #endregion
        public SummaryReportResponse GetSummaryReport(ReportRequestParam request)
        {
            SummaryReportResponse response = new SummaryReportResponse();
            List<SummaryReportInfo> _list = new List<SummaryReportInfo>();
            var sql = "SW_PROC_GATEWAY_REPORT @Flag =" +dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode); 
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@Product= " + dao.FilterString(request.Product);
            Log.Information("GetSummaryReport Report Query :{0}", sql);
            try
            {
                var dbTable = reportDao.ExecuteDataTable(sql);

                if (dbTable != null)
                {
                    foreach (DataRow dr in dbTable.Rows)
                    {
                        _list.Add(new SummaryReportInfo
                        {
                            OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                            BranchCode = Convert.ToString(dr["Branch"]),
                            Count = Convert.ToString(dr["TxnCount"]),
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            Date = String.IsNullOrEmpty(Convert.ToString(dr["Date"])) ? string.Empty : Convert.ToDateTime(dr["Date"]).ToString("yyyy-MM-dd"),
                            Status = Convert.ToString(dr["Status"]),
                            Product = Convert.ToString(dr["Product"]),
                        });
                        totalAmount = totalAmount + Convert.ToDecimal(dr["Amount"]);
                        response.TotalCount = response.TotalCount + Convert.ToInt16(dr["TxnCount"]);
                    }
                    response.TotalAmount = Convert.ToDecimal(Utilities.FormatCurrency(totalAmount));
                    response.ReportDetails = _list;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetTopupSummaryReport SQL execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public ReportResponse GetSummaryDetailReport(DetailReportRequest request)
        {
            ReportResponse _resp = new ReportResponse();
            var responseParam = new List<ReportResponseDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag ="+dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Branch= " + dao.FilterString(request.Branch);
            sql += " ,@Product= " + dao.FilterString(request.Product);
            Log.Information("GetTopupDetailReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new ReportResponseDetails
                        {
                            Branch = dr["Branch"].ToString(),
                            Mobile = dr["Msisdn"].ToString(),
                            TransactionId = dr["GatewayTxnId"].ToString(),
                            clientTxnId = dr["TransactionId"].ToString(),
                            ThirdPartyTxnId = dr["ThirdPartyTranId"].ToString(),
                            Status = dr["Status"].ToString(),
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            Date = Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                        });

                        totalAmount = totalAmount + Convert.ToDecimal(dr["Amount"]);
                    }
                    _resp.TotalAmount = Convert.ToDecimal(Utilities.FormatCurrency(totalAmount));
                    _resp.ReportDetails = responseParam;
                    _resp.OrganisationCode = request.OrganisationCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetTopupDetailReport SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public ReportResponse GetTransactionReport(ReportRequest request)
        {
            ReportResponse _resp = new ReportResponse();
            var responseParam = new List<ReportResponseDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag ="+ dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Branch= " + dao.FilterString(request.Branch);
            sql += " ,@TransactionId= " + dao.FilterString(request.TransactionId);
            sql += " ,@CustomerId= " + dao.FilterString(request.Account);
            sql += " ,@DisplayStart= " + dao.FilterQuote(request.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(request.DisplayLength);
            sql += " ,@Search= " + dao.FilterString(request.Search);
            sql += " ,@SortCol= " + dao.FilterString(request.SortCol);
            sql += " ,@SortDir= " + dao.FilterString(request.SortDir);
            sql += " ,@Product= " + dao.FilterString(request.Product);
            Log.Information("GetTransactionReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new ReportResponseDetails
                        {
                            Branch = dr["Branch"].ToString(),
                            Product = dr["Product"].ToString(),
                            ServiceProvider = dr["ServiceProvider"].ToString(),
                            Mobile = dr["Msisdn"].ToString(),
                            TransactionId = dr["GatewayTxnId"].ToString(),
                            clientTxnId = dr["TransactionId"].ToString(),
                            CustomerId = dr["AccountNum"].ToString(),
                            ThirdPartyTxnId = dr["ThirdPartyTranId"].ToString(),
                            Status = dr["Status"].ToString(),
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            Date = Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileDate = string.IsNullOrEmpty(Convert.ToString(dr["ReconcileDate"])) ? "" : Convert.ToDateTime(dr["ReconcileDate"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileBy = Convert.ToString(dr["ReconcileBy"]),
                            FilterCount = Convert.ToInt32(dr["Filter_Count"]),
                            SNo = Convert.ToInt32(dr["Num"]),
                            Account = Convert.ToString(dr["AccountNum"]),
                            OrganisationCode= Convert.ToString(dr["Organisation_Code"])
                        });  

                        totalAmount = totalAmount + Convert.ToDecimal(dr["Amount"]);
                    }
                    _resp.TotalAmount = Convert.ToDecimal(Utilities.FormatCurrency(totalAmount));
                    _resp.ReportDetails = responseParam;
                    _resp.OrganisationCode = request.OrganisationCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetTransactionReport  SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public ExportAllTranReportResp ExportAllTranReport(ReportRequest request)
        {
            ExportAllTranReportResp _resp = new ExportAllTranReportResp();
            var responseParam = new List<ExportAllResponseDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag =" + dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Branch= " + dao.FilterString(request.Branch);
            sql += " ,@TransactionId= " + dao.FilterString(request.TransactionId);
            sql += " ,@CustomerId= " + dao.FilterString(request.Account);
            Log.Information("ExportAllTranReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    int i = 0; 
                    foreach (DataRow dr in response.Rows)
                    {
                        i = i + 1;
                        responseParam.Add(new ExportAllResponseDetails
                        {
                            Product = dr["Product"].ToString(),
                            Date = Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileDate = string.IsNullOrEmpty(Convert.ToString(dr["ReconcileDate"])) ? "" : Convert.ToDateTime(dr["ReconcileDate"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileBy = Convert.ToString(dr["ReconcileBy"]),
                            clientTxnId = dr["TransactionId"].ToString(),
                            TxnId = Convert.ToString(dr["GatewayTxnId"]),
                            ThirdPartyTxnId =  Convert.ToString(dr["ThirdPartyTranId"]),
                            ServiceProvider = Convert.ToString( dr["ServiceProvider"]),
                            Account = Convert.ToString(dr["AccountNum"]),
                            Status = Convert.ToString(dr["Status"]),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            SNo = i
                        }); ; 
                    }
                    _resp.ReportDetails = responseParam;
                    _resp.OrganisationCode = request.OrganisationCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ExportAllTranReport  SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public SmsReportResponse GetGuiSmsReport(ReportRequestParam request)
        {
            SmsReportResponse _resp = new SmsReportResponse();
            var responseParam = new List<SmsReportModel>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='GetGuiSmsReport'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Operator= " + dao.FilterString(request.Operator);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Branch= " + dao.FilterString(request.Branch);
            sql += " ,@DisplayStart= " + dao.FilterQuote(request.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(request.DisplayLength);
            sql += " ,@Search= " + dao.FilterString(request.Search);
            sql += " ,@SortCol= " + dao.FilterString(request.SortCol);
            sql += " ,@SortDir= " + dao.FilterString(request.SortDir);

            Log.Information("GetGuiSmsReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {

                        responseParam.Add(new SmsReportModel
                        {
                            ReceiverNo = dr["ReceiverNo"].ToString(),
                            Branch = dr["Branch"].ToString(),
                            SmsMessage = dr["SmsMessage"].ToString(),
                            Operator = dr["Operator"].ToString(),
                            SmsCount = Convert.ToInt32(dr["SmsLength"]),
                            Response = dr["Response"].ToString(),
                            Status = dr["Status"].ToString(),
                            SendDate = Utilities.FormatDateTime(dr["SentDate"].ToString()),
                            FilterCount = Convert.ToInt32(dr["FilterCount"].ToString()),
                        });

                    }
                    _resp.ReportDetails = responseParam;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetGuiSmsReport  SQL execution Exception Occured with message:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public SmsSummaryReportResponse GetSmsSummaryReport(ReportRequestParam request)
        {
            SmsSummaryReportResponse _resp = new SmsSummaryReportResponse();
            var responseParam = new List<SmsSummaryReportModel>();

            var sql = "SW_PROC_SAVE_SMS_LOG @Flag ='GetAdminSmsReport'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);

            Log.Information("GetSmsSummaryReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {

                        responseParam.Add(new SmsSummaryReportModel
                        {
                            OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                            Branch = Convert.ToString(dr["Branch"]),
                            SmsLength = Convert.ToInt32(dr["SmsLength"]),
                            Status = Convert.ToString(dr["Status"]),
                            ReceiverNo = Convert.ToString(dr["ReceiverNo"]),
                            Operator = Convert.ToString(dr["Operator"]),
                            RequestedDate = Utilities.FormatDateTime(Convert.ToString(dr["RequestedDate"])),
                            SentDate = Utilities.FormatDateTime(Convert.ToString(dr["SentDate"])),

                        });

                    }
                    _resp.ReportDetails = responseParam;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetSmsSummaryReport  SQL execution Exception Occured with message:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public ExportAllSmsReport ExportGuiSmsReport(ReportRequestParam request)
        {
            ExportAllSmsReport _resp = new ExportAllSmsReport();
            var responseParam = new List<SmsExportDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='ExportSmsReport'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@Operator= " + dao.FilterString(request.Operator);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@Status= " + dao.FilterString(request.Status);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Branch= " + dao.FilterString(request.Branch);

            Log.Information("ExportGuiSmsReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {

                        responseParam.Add(new SmsExportDetails
                        {
                            ReceiverNo = dr["ReceiverNo"].ToString(),
                            Branch = dr["Branch"].ToString(),
                            Operator = dr["Operator"].ToString(),
                            SmsCount = Convert.ToInt32(dr["SmsLength"]),
                            Response = dr["Response"].ToString(),
                            Status = dr["Status"].ToString(),
                            SendDate = Utilities.FormatDateTime(dr["SentDate"].ToString()),
                        });

                    }
                    _resp.ReportDetails = responseParam;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ExportGuiSmsReport  SQL execution Exception Occured with message:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public ReportResponse GetReconcileReport(ReportRequest request)
        {
            ReportResponse _resp = new ReportResponse();
            var responseParam = new List<ReportResponseDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag =" + dao.FilterString(request.Flag);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@DisplayStart= " + dao.FilterQuote(request.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(request.DisplayLength);
            sql += " ,@Search= " + dao.FilterString(request.Search);
            sql += " ,@SortCol= " + dao.FilterString(request.SortCol);
            sql += " ,@SortDir= " + dao.FilterString(request.SortDir);
            Log.Information("GetReconcileReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new ReportResponseDetails
                        {
                            OrganisationCode = dr["Organisation_Code"].ToString(),
                            Product = dr["Product"].ToString(),
                            ServiceProvider = dr["ServiceProvider"].ToString(),
                            Mobile = dr["Msisdn"].ToString(),
                            TransactionId = dr["GatewayTxnId"].ToString(),
                            clientTxnId = dr["TransactionId"].ToString(),
                            CustomerId = dr["AccountNum"].ToString(),
                            ThirdPartyTxnId = dr["ThirdPartyTranId"].ToString(),
                            Status = dr["Status"].ToString(),
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            Date = Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileDate = string.IsNullOrEmpty(Convert.ToString(dr["ReconcileDate"])) ? "" : Convert.ToDateTime(dr["ReconcileDate"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileBy = Convert.ToString(dr["ReconcileBy"]),
                            FilterCount = Convert.ToInt32(dr["Filter_Count"]),
                            SNo = Convert.ToInt32(dr["Num"]),
                            Account = Convert.ToString(dr["AccountNum"])
                        });

                        totalAmount = totalAmount + Convert.ToDecimal(dr["Amount"]);
                    }
                    _resp.TotalAmount = Convert.ToDecimal(Utilities.FormatCurrency(totalAmount));
                    _resp.ReportDetails = responseParam;
                    _resp.OrganisationCode = request.OrganisationCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetReconcileReport  SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }

        public ExportAllTranReportResp ExportAllReconcileReport(ReportRequest request)
        {
            ExportAllTranReportResp _resp = new ExportAllTranReportResp();
            var responseParam = new List<ExportAllResponseDetails>();

            var sql = "SW_PROC_GATEWAY_REPORT @Flag =" + dao.FilterString(request.Flag);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            Log.Information("ExportAllReconcileReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    int i = 0;
                    foreach (DataRow dr in response.Rows)
                    {
                        i = i + 1;
                        responseParam.Add(new ExportAllResponseDetails
                        {
                            Product = dr["Product"].ToString(),
                            OrganisationCode = dr["Organisation_Code"].ToString(),
                            Date = Convert.ToDateTime(dr["Created_Date"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileDate = string.IsNullOrEmpty(Convert.ToString(dr["ReconcileDate"])) ? "" : Convert.ToDateTime(dr["ReconcileDate"]).ToString("yyyy-MM-dd hh:mm:ss tt"),
                            ReconcileBy = Convert.ToString(dr["ReconcileBy"]),
                            clientTxnId = dr["TransactionId"].ToString(),
                            TxnId = Convert.ToString(dr["GatewayTxnId"]),
                            ThirdPartyTxnId = Convert.ToString(dr["ThirdPartyTranId"]),
                            ServiceProvider = Convert.ToString(dr["ServiceProvider"]),
                            Account = Convert.ToString(dr["AccountNum"]),
                            Status = Convert.ToString(dr["Status"]),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            SNo = i
                        });
                    }
                    _resp.ReportDetails = responseParam;
                    _resp.OrganisationCode = request.OrganisationCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ExportAllReconcileReport  SQL execution Exception Occured:{0}", ex.Message);
                _resp.ResponseCode = ResponseCodeConstant.EXCEPTION;
                _resp.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return _resp;
        }


        public ExportSmsBillingInfo ExportSmsBillingInfo(ReportRequestParam model)
        {
            ExportSmsBillingInfo response = new ExportSmsBillingInfo();
            List<ExportSmsBillingDetails> _list = new List<ExportSmsBillingDetails>();
            var sql = "SW_PROC_GATEWAY_REPORT @Flag ='ExportSmsBillingInfo'";
            sql += " ,@OrganisationCode= " + dao.FilterString(model.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(model.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(model.ToDate);
            Log.Information("ExportSmsBillingInfo Query :{0}", sql);
            try
            {
                var dbTable = reportDao.ExecuteDataTable(sql);

                if (dbTable != null)
                {
                    foreach (DataRow dr in dbTable.Rows)
                    {
                        _list.Add(new ExportSmsBillingDetails
                        {
                            OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                            BranchCode = Convert.ToString(dr["Branch"]),
                            SmsCount = Convert.ToString(dr["smscount"]),
                            Date = String.IsNullOrEmpty(Convert.ToString(dr["Date"])) ? string.Empty : Convert.ToDateTime(dr["Date"]).ToString("yyyy-MM-dd"),
                            Status = Convert.ToString(dr["Status"]),
                            Operator = Convert.ToString(dr["Operator"]),

                        });

                    }
                    response.ReportDetails = _list;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ExportSmsBillingInfo SQL execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public ProductWiseReportResponse ProductWiseReport(ProductWiseRequestModel model)
        {
            ProductWiseReportResponse response = new ProductWiseReportResponse();
            List<ProductWiseReportDetail> _list = new List<ProductWiseReportDetail>();
            var sql = "SW_PROC_PRODUCT_WISE_REPORT @Flag =" + dao.FilterString(model.Flag);
            sql += " ,@FromDate= " + dao.FilterString(model.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(model.ToDate);
            sql += " ,@Product= " + dao.FilterString(model.Product);
            Log.Information("ProductWiseReport Query :{0}", sql);
            try
            {
                var dbTable = reportDao.ExecuteDataTable(sql);

                if (dbTable != null)
                {
                    if (model.Flag == "DateWiseSummary")
                    {
                        foreach (DataRow dr in dbTable.Rows)
                        {

                            _list.Add(new ProductWiseReportDetail
                            {
                                TxnCount = Convert.ToString(dr["TxnCount"]),
                                Amount = Convert.ToString(dr["Amount"]),
                                Product = Convert.ToString(dr["Product"]),
                                Date = Convert.ToString(dr["Date"])

                            });

                        }
                    }
                     if (model.Flag == "ProductWiseSummary")
                    {
                        foreach (DataRow dr in dbTable.Rows)
                        {
                            _list.Add(new ProductWiseReportDetail
                            {
                                TxnCount = Convert.ToString(dr["TxnCount"]),
                                Amount = Convert.ToString(dr["Amount"]),
                                Product = Convert.ToString(dr["Product"]),

                            });
                        }
                    }
                    if (model.Flag == "DillDrownProduct")
                    {
                        

                            foreach (DataRow dr in dbTable.Rows)
                            {
                                _list.Add(new ProductWiseReportDetail
                                {
                                    OrganisationCode = Convert.ToString(dr["Organisation_Code"]),
                                    TxnCount = Convert.ToString(dr["TxnCount"]),
                                    Amount = Convert.ToString(dr["Amount"]),
                                    Product = Convert.ToString(dr["Product"]),

                                });
                            

                        }
                    }
                    response.ReportDetails = _list;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ProductWiseReport SQL execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public List<AirLinesSettlemetDetails> GetAirLinesSettlementReport(ReportRequest request)
        {
            var airLinesSettlemetDetails = new List<AirLinesSettlemetDetails>();

            var sql = "SW_PROC_AIRLINES_TICKETING_DETAIL @Flag =" + dao.FilterString("GetReport");
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@DisplayStart= " + dao.FilterQuote(request.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(request.DisplayLength);
            Log.Information("GetAirLinesSettlementReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        airLinesSettlemetDetails.Add(new AirLinesSettlemetDetails
                        {
                            FilterCount = Convert.ToInt32(dr["Filter_Count"]),
                            SNo = Convert.ToInt32(dr["Num"]),
                            FastConnectTxnId = Convert.ToString(dr["FastConnectTxnId"]),
                            BankTxnId = Convert.ToString(dr["BankTxnId"]),
                            CreatedDate = Utilities.FormatDateTime(Convert.ToString(dr["CreatedDate"])),
                            TicketDate = Utilities.FormatDateTime(Convert.ToString(dr["TicketDate"])),
                            AirlinesCode = Convert.ToString(dr["AirlinesCode"]),
                            FromSector = Convert.ToString(dr["FromSector"]),
                            ToSector = Convert.ToString(dr["ToSector"]),
                            TicketClass = Convert.ToString(dr["TicketClass"]),
                            Pnr = Convert.ToString(dr["Pnr"]),
                            TicketNumber = Convert.ToString(dr["TicketNumber"]),
                            Amount = Utilities.FormatCurrencyString(Convert.ToString(dr["Amount"])),
                            AirlinesCommission = Utilities.FormatCurrencyString(Convert.ToString(dr["AirlinesCommission"])),
                            Status = Convert.ToString(dr["Status"]),
                            SettlementDate = Utilities.FormatDateTime(Convert.ToString(dr["SettlementDate"])),
                            SettledTxnId = Convert.ToString(dr["SettledTxnId"])
                        }); 
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetAirLinesSettlementReport  SQL execution Exception Occured:{0}", ex.Message);
               
            }
            return airLinesSettlemetDetails;
        }
   
        public List<ExportAirLinesSettlemetDetails> ExportAirLinesSettlementReport(ReportRequest request)
        {
            var airLinesSettlemetDetails = new List<ExportAirLinesSettlemetDetails>();

            var sql = "SW_PROC_AIRLINES_TICKETING_DETAIL @Flag =" + dao.FilterString("ExportReport");
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            Log.Information("ExportAirLinesSettlemetDetails with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        airLinesSettlemetDetails.Add(new ExportAirLinesSettlemetDetails
                        {
                            SNo = Convert.ToInt32(dr["Num"]),
                            FastConnectTxnId = Convert.ToString(dr["FastConnectTxnId"]),
                            BankTxnId = Convert.ToString(dr["BankTxnId"]),
                            CreatedDate = Utilities.FormatDateTime(Convert.ToString(dr["CreatedDate"])),
                            TicketDate = Utilities.FormatDateTime(Convert.ToString(dr["TicketDate"])),
                            AirlinesCode = Convert.ToString(dr["AirlinesCode"]),
                            FromSector = Convert.ToString(dr["FromSector"]),
                            ToSector = Convert.ToString(dr["ToSector"]),
                            TicketClass = Convert.ToString(dr["TicketClass"]),
                            Pnr = Convert.ToString(dr["Pnr"]),
                            TicketNumber = Convert.ToString(dr["TicketNumber"]),
                            Amount = Utilities.FormatCurrencyString(Convert.ToString(dr["Amount"])),
                            AirlinesCommission = Utilities.FormatCurrencyString(Convert.ToString(dr["AirlinesCommission"])),
                            Status = Convert.ToString(dr["Status"]),
                            SettlementDate = Utilities.FormatDateTime(Convert.ToString(dr["SettlementDate"])),
                            SettledTxnId = Convert.ToString(dr["SettledTxnId"])
                        }); 
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetAirLinesSettlementReport  SQL execution Exception Occured:{0}", ex.Message);
               
            }
            return airLinesSettlemetDetails;
        }

        public List<SendDomesticRemittanceReportModel> GetDomesticRemittanceReport(ReportRequest request)
        {
            var details = new List<SendDomesticRemittanceReportModel>();

            var sql = "SW_PROC_REMITTANCE_TXN @Flag =" + dao.FilterString("GetReport");
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@DisplayStart= " + dao.FilterQuote(request.DisplayStart);
            sql += " ,@DisplayLength= " + dao.FilterQuote(request.DisplayLength);
            Log.Information("GetDomesticRemittanceReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        details.Add(new SendDomesticRemittanceReportModel
                        {
                            SNo = Convert.ToInt32(dr["Num"]),
                            FilterCount = Convert.ToInt32(dr["FilterCount"]),
                            SenderMobileNo = Convert.ToString(dr["SenderMobileNo"]),
                            SenderName = Convert.ToString(dr["SenderName"]),
                            TxnDate = Utilities.FormatDateTime(Convert.ToString(dr["CreatedDate"])),
                            ReceiverMobileNo = Convert.ToString(dr["ReceiverMobileNo"]),
                            ReceiverName = Convert.ToString(dr["ReceiverName"]),
                            ClientTransactionId = Convert.ToString(dr["ThirdPartyTxnId"]),
                            TransactionId = Convert.ToString(dr["TransactionId"]),
                            OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                            Purpose = Convert.ToString(dr["Purpose"]),
                            Relationship = Convert.ToString(dr["Relationship"]),
                            Amount = Utilities.FormatCurrencyString(Convert.ToString(dr["Amount"])),
                            Status = Convert.ToString(dr["Status"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("GetDomesticRemittanceReport  SQL execution Exception Occured:{0}", ex.Message);

            }
            return details;
        }

        public List<ExportSendDomesticRemittanceReportModel> ExportDomesticRemittanceReport(ReportRequest request)
        {
            var details = new List<ExportSendDomesticRemittanceReportModel>();

            var sql = "SW_PROC_REMITTANCE_TXN @Flag =" + dao.FilterString("ExportReport");
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            Log.Information("ExportDomesticRemittanceReport with  Query :{0}", sql);

            try
            {
                var response = reportDao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        details.Add(new ExportSendDomesticRemittanceReportModel
                        {
                            SNo = Convert.ToInt32(dr["Num"]),
                            SenderMobileNo = Convert.ToString(dr["SenderMobileNo"]),
                            SenderName = Convert.ToString(dr["SenderName"]),
                            TxnDate = Utilities.FormatDateTime(Convert.ToString(dr["CreatedDate"])),
                            ReceiverMobileNo = Convert.ToString(dr["ReceiverMobileNo"]),
                            ReceiverName = Convert.ToString(dr["ReceiverName"]),
                            ClientTransactionId = Convert.ToString(dr["ThirdPartyTxnId"]),
                            TransactionId = Convert.ToString(dr["TransactionId"]),
                            OrganisationCode = Convert.ToString(dr["OrganisationCode"]),
                            Purpose = Convert.ToString(dr["Purpose"]),
                            Relationship = Convert.ToString(dr["Relationship"]),
                            Amount = Utilities.FormatCurrencyString(Convert.ToString(dr["Amount"])),
                            Status = Convert.ToString(dr["Status"])

                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("ExportDomesticRemittanceReport  SQL execution Exception Occured:{0}", ex.Message);

            }
            return details;
        }
    }
}
