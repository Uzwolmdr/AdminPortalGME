using Repository.Connection;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.Repo.Water
{
    public class WaterRepo : IWaterRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<WaterRepo>();
        SwiftDao dao;
        ImePayConnection _imeDao;
        public WaterRepo()
        {
            dao = new SwiftDao();
            _imeDao = new ImePayConnection();
        }

        public List<WaterCounterList> GetCommunityWaterCounter()
        {
            var list = new List<WaterCounterList>();
            var sql = string.Format("EXEC SW_PROC_WATERMARK_PAYMENT @Flag='GetDiyaloCounter'");
            Log.Information("GetWaterMarkCounter SQL:{0}", sql);
            try
            {
                var res = _imeDao.ExecuteDataTable(sql);
                if (res.Rows.Count > 0)
                {
                    foreach (DataRow dr in res.Rows)
                    {
                        WaterCounterList data = new WaterCounterList();
                        data.CounterName = Convert.ToString(dr["CounterName"]);
                        data.CounterCode = Convert.ToString(dr["ApiUserName"]);
                        data.Product = Convert.ToString(dr["Product"]).ToUpper();
                        data.MerchantCode = Convert.ToString(dr["MerchantCode"]).ToUpper();
                        list.Add(data);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetWaterMarkCounter error : {0}", ex.Message);
            }

            return list;
        }

        public CoreResponse InsertDiyaloKhanepaniRequest(CommunityWaterPaymentDbRequestLog req)
        {
            CoreResponse response = new CoreResponse
            {
                ResponseCode = 999,
                ResponseDescription = "Internal Error",
                ExtraId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_WATERMARK_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@CustomerId=" + _imeDao.FilterString(req.CustomerId);
                sql += " ,@Customer_MobileNo=" + _imeDao.FilterString(req.Msisdn);
                sql += " ,@Customer_Name=" + _imeDao.FilterString(req.CustomerName);
                sql += " ,@Org_UserId=" + _imeDao.FilterString(req.CounterCode);
                sql += " ,@User=" + _imeDao.FilterString(req.UserName);
                sql += " ,@Amount=" + _imeDao.FilterString(req.Amount);
                sql += " ,@Zone=" + _imeDao.FilterString(req.Zone);
                sql += " ,@Ward=" + _imeDao.FilterString(req.Ward);
                sql += " ,@Type=" + _imeDao.FilterString(req.Type);
                sql += " ,@Json=" + _imeDao.FilterString(req.Json);
                sql += " ,@Address=" + _imeDao.FilterString(req.Address);
                sql += " ,@Org_Name=" + _imeDao.FilterString(req.organization);
                sql += " ,@PenaltyAmount=" + _imeDao.FilterString(req.PenaltyAmount);
                sql += " ,@BillAmount=" + _imeDao.FilterString(req.BillAmount);
                sql += " ,@FromDate=" + _imeDao.FilterString(req.FromDate);
                sql += " ,@ToDate=" + _imeDao.FilterString(req.ToDate);
                sql += " ,@Channel=" + _imeDao.FilterString(req.Channel);

                Log.Information("InsertDiyaloKhanepaniRequest SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ExtraId = Convert.ToString(dr["Id"]);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured at method: insertRecord with exception message: {0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.FAILED;
                response.ResponseDescription = "Exception Occured.";
                return response;
            }
        }
        public CoreResponse InsertHeartsunKhanepaniRequest(CommunityWaterPaymentDbRequestLog request)
        {
            CoreResponse response = new CoreResponse
            {
                ResponseCode = 999,
                ResponseDescription = "Internal Error",
                ExtraId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_HEARTSUN_PAYMENT @Flag='insertRecord'";
                sql += ",@CustomerCode=" + _imeDao.FilterString(request.CustomerId);
                sql += ",@CounterCode=" + _imeDao.FilterString(request.CounterCode);
                sql += ",@Amount=" + _imeDao.FilterString(request.Amount);
                sql += ",@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += ",@CustomerAddress=" + _imeDao.FilterString(request.Address);
                sql += ",@MobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@RequestId=" + _imeDao.FilterString(request.RequestId);
                sql += ",@TokenId=" + _imeDao.FilterString(request.TokenId);
                sql += ",@Json=" + _imeDao.FilterString(request.Json);
                sql += ",@CreatedBy=" + _imeDao.FilterString(request.UserName);


                Log.Information("Heartsun Payment Log Details data insertion :{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ExtraId = Convert.ToString(dr["Id"]);

                }
            }
            catch (Exception ex)
            {
                Log.Error("Heartsun Payment Exception arrived  with error : {0}", ex.Message);
                response.ResponseCode = 101;
                response.ResponseDescription = "Internal Error";
            }
            return response;
        }
        public CoreResponse InsertHetaudaKhanepaniRequest(CommunityWaterPaymentDbRequestLog request)
        {
            CoreResponse response = new CoreResponse
            {
                ResponseCode = 999,
                ResponseDescription = "Internal Error",
                ExtraId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_HETAUDA_KHANEPANI_PAYMENT @flag = 'insertRecord'";
                sql += " ,@CustomerId=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@AreaNumber=" + _imeDao.FilterString(request.AreaNumber);
                sql += " ,@AdvanceAvaliable=" + _imeDao.FilterString(request.AdvanceAvaliable);
                sql += " ,@AmountPayable=" + _imeDao.FilterString(request.AmountPayable);
                sql += " ,@TotalBillAmount=" + _imeDao.FilterString(request.TotalBillAmount);
                sql += " ,@TotalFine=" + _imeDao.FilterString(request.TotalFine);
                sql += " ,@TotalDiscount=" + _imeDao.FilterString(request.TotalDiscount);
                sql += " ,@PayAmount=" + _imeDao.FilterString(request.PayAmount);
                sql += " ,@Createdby=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                sql += " ,@LngBillCount=" + _imeDao.FilterString(request.MonthCount);
                sql += " ,@Product=" + _imeDao.FilterString(request.Product);

                Log.Information("InsertHetaudaKhanepani  sql:{0}", sql);
                var res = _imeDao.ExecuteDataRow(sql);
                if (res != null)
                {
                    response.ResponseCode = Convert.ToInt32(res["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(res["ResponseDescription"]);
                    response.ExtraId = Convert.ToString(res["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("InsertHetaudaKhanepani Exception occured with Error:{0}", ex.Message);
            }
            return response;
        }
        public CoreResponse InsertH2OKhanepaniRequest(CommunityWaterPaymentDbRequestLog request)
        {
            CoreResponse response = new CoreResponse
            {
                ResponseCode = 999,
                ResponseDescription = "Internal Error",
                ExtraId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_H2O_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += ",@Address=N" + _imeDao.FilterString(request.Address);
                sql += ",@Amount=" + _imeDao.FilterString(request.Amount);
                sql += ",@CounterCode=" + _imeDao.FilterString(request.CounterCode);
                sql += ",@CurrentMonthDisocunt=" + _imeDao.FilterString(request.CurrentMonthDisocunt);
                sql += ",@CurrentMonthDues=" + _imeDao.FilterString(request.CurrentMonthDues);
                sql += ",@CurrentMonthFine=" + _imeDao.FilterString(request.CurrentMonthFine);
                sql += ",@CustomerCode=" + _imeDao.FilterString(request.CustomerId);
                sql += ",@CustomerName=N" + _imeDao.FilterString(request.CustomerName);
                sql += ",@CustomerNumber=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@PreviousDues=" + _imeDao.FilterString(request.PreviousDues);
                sql += ",@TotalAdvanceAmount=" + _imeDao.FilterString(request.TotalAdvanceAmount);
                sql += ",@TotalCreditSalesAmount=" + _imeDao.FilterString(request.TotalCreditSalesAmount);
                sql += ",@MonthSN=" + _imeDao.FilterString(request.MonthSN);
                sql += ",@Created_By=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@TotalDues=" + _imeDao.FilterString(request.TotalDues);
                sql += ",@Json=N" + _imeDao.FilterString(request.Json);

                Log.Information("InsertH2OKhanepaniRequest  SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ExtraId = Convert.ToString(dr["Id"]);
                }
                return response;

            }
            catch (Exception ex)
            {
                Log.Error("InsertH2OKhanepaniRequest Exception occur with errror {0} ", ex.Message);
            }
            return response;
        }

        public CoreResponse InsertNepalKhanePaniRequest(CommunityWaterPaymentDbRequestLog request)
        {
            var response = new CoreResponse { ResponseCode = 999, ResponseDescription = "Internal Error" };
            try
            {
                var sql = "EXEC SW_PROC_NWSC_PAYMENT @Flag=" + _imeDao.FilterString("insertRecord") +
                            ",@ConsumerId=" + _imeDao.FilterString(request.CustomerId) +
                            ",@ConsumerName=" + _imeDao.FilterString(request.CustomerName) +
                            ",@ConsumerAddress=" + _imeDao.FilterString(request.Address) +
                            ",@BranchId=" + _imeDao.FilterString(request.CounterCode) +
                            ",@CustomerNo=" + _imeDao.FilterString(request.Msisdn) +
                            ",@Amount=" + _imeDao.FilterString(request.Amount) +
                            ",@DueFromMonth=" + _imeDao.FilterString(request.DueFromMonth) +
                            ",@DueToMonth=" + _imeDao.FilterString(request.DueToMonth) +
                             ",@Createdby=" + _imeDao.FilterString(request.Msisdn) +
                            ",@Json=" + _imeDao.FilterString(request.Json) +
                            ",@SessionId=" + _imeDao.FilterString(request.SessionId);
                Log.Information("InsertNepalWaterSupplierCorporation Data SQL:{0}", sql);
                var res = _imeDao.ExecuteDataRow(sql);
                if (res != null)
                {
                    response.ResponseCode = Convert.ToInt32(res["Code"]);
                    response.ResponseDescription = Convert.ToString(res["Msg"]);
                    response.ExtraId = Convert.ToString(res["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Execption in method:InsertNepalWaterSupplierCorporation|Error:{0}", ex.Message);
            }
            return response;
        }
    }
}
