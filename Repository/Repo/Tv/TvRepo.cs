using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Tv;
using System;

namespace Repository.Repo.Tv
{
    public class TvRepo : ITvRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<UtilityRepoService>();
        ImePayConnection _imeDao;
        SwiftDao dao;
        public TvRepo()
        {
            _imeDao = new ImePayConnection();
            dao = new SwiftDao();
        }
        public CoreResponse InsertSimTVRequest(SimTVPaymentRequest request)
        {
            CoreResponse response = new CoreResponse
            {
                ResponseCode = 999,
                ResponseDescription = "Internal Error",
                ExtraId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_SIMTV_PAYMENT";
                sql += " @Flag=" + dao.FilterString("insertRecord");
                sql += ",@Customer_Id=" + dao.FilterString(request.CustomerId);
                sql += ",@Customer_No=" + dao.FilterString(request.Msisdn);
                sql += ",@Customer_Name=" + dao.FilterString(request.CustomerName);
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                sql += ",@Created_By=" + dao.FilterString(request.Msisdn);
                sql += ",@Request_Json=" + dao.FilterString(request.Json);
                Log.Information("InsertSimTvRequest SQL:{0}", sql);
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
                Log.Error("Exception occur while sim tv insert record with error: {0}", ex.Message);
            }
            return response;
        }
        public DbLogResponse InsertMeroTvData(MeroTvPaymentRequestModel model)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error",
                ReferenceId = ""

            };
            var sql = "EXEC SW_PROC_MEROTV_PAYMENT";
            sql += " @Flag=" + _imeDao.FilterString("insertRecord");
            sql += " ,@MSISDN=" + _imeDao.FilterString(model.Msisdn);
            sql += " ,@SmartCardId=" + _imeDao.FilterString(model.CustomerId);
            sql += " ,@CheckoutId=" + _imeDao.FilterString(model.CheckoutId);
            sql += " ,@CheckoutToken=" + _imeDao.FilterString(model.CheckoutToken);

            sql += " ,@PaymentId=" + _imeDao.FilterString(model.PaymentId);
            sql += " ,@User=" + _imeDao.FilterString(model.CreatedBy);
            sql += " ,@Amount=" + _imeDao.FilterString(model.Amount);

            sql += " ,@RequestJson=" + _imeDao.FilterString(model.Json);
            Log.Information("InsertMEROTVReponse SQL:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                response.ReferenceId = Convert.ToString(dr["Id"]);
            }
            return response;
        }
        public DbLogResponse InsertWebServerData(WebServerPaymentRequestModel model)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Erorr",
                ReferenceId = ""

            };
            var sql = "EXEC SW_PROC_WEBSURFER_PAYMENT";
            sql += " @Flag=" + _imeDao.FilterString("insertRecord");
            sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(model.Msisdn);
            sql += " ,@CustomerName=" + _imeDao.FilterString(model.CustomerName);
            sql += " ,@Json=" + _imeDao.FilterString(model.Json);
            sql += " ,@Amount=" + _imeDao.FilterString(model.Amount);
            sql += " ,@PlanName=" + _imeDao.FilterString(model.Plan);
            sql += " ,@MSISDN=" + _imeDao.FilterString(model.Msisdn);
            sql += " ,@CustomerId=" + _imeDao.FilterString(model.CustomerId);
            sql += " ,@PaymentCode=" + _imeDao.FilterString(model.PaymentCode);
            sql += " ,@SmartCardId=" + _imeDao.FilterString(model.SmartCardId);
            sql += " ,@Request=" + _imeDao.FilterString(model.Request);
            Log.Information("InsertWebsurferRequest SQL:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                response.ReferenceId = Convert.ToString(dr["Id"]);
            }
            return response;
        }
        public DbLogResponse InsertParbhuTvData(ParbhuTvPaymentRequestModel model)
        {
            var res = new DbLogResponse()
            {
                ResponseCode = 999,
                ResponseDescription = "INVALID INPUT"
            };
            try
            {
                var sql = "EXEC [SW_PROC_PRABHUTV_PAYMENT]";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@CasId=" + _imeDao.FilterString(model.CasId);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(model.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(model.CustomerName);
                sql += " ,@Amount=" + _imeDao.FilterString(model.Amount);
                sql += " ,@Sessionid=" + _imeDao.FilterString(model.Sessionid);
                sql += " ,@CreatedBy=" + _imeDao.FilterString(model.CreatedBy);
                sql += " ,@CustomerId=" + _imeDao.FilterString(model.CustomerId);
                sql += " ,@Json	=" + _imeDao.FilterString(model.Json);
                sql += " ,@StockId=" + _imeDao.FilterString(model.StockId);
                sql += " ,@ProductId=" + _imeDao.FilterString(model.ProductId);
                Log.Information("Prabhu TV SQL:{0}", sql);
                var dbRes = _imeDao.ExecuteDataRow(sql);
                if (dbRes != null)
                {
                    res.ResponseCode = Convert.ToInt32(dbRes["ResponseCode"]);
                    res.ResponseDescription = Convert.ToString(dbRes["ResponseDescription"]);
                    res.ReferenceId = Convert.ToString(dbRes["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception in method:InsertPrabhuTVRequest|Error:{0}", ex.Message);
            }
            return res;
        }

        public DbLogResponse InsertNetTvData(NetTvPaymentModel request)
        {
            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                var sql = "SW_PROC_NETTV_PAYMENT @Flag=" + _imeDao.FilterString("insertRecord");
                sql += ",@CreatedBy=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@Amount=" + _imeDao.FilterString(request.Amount);
                sql += ",@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += ",@CustomerAddress=" + _imeDao.FilterString(request.CustomerAddress);
                sql += ",@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@PackagePlanName=" + _imeDao.FilterString(request.PackagePlanName);
                sql += ",@PackageSaleId=" + _imeDao.FilterString(request.PackageId);
                sql += ",@DeviceId=" + _imeDao.FilterString(request.DeviceId);
                sql += ",@UserId=" + _imeDao.FilterString(request.CustomerId);
                sql += ",@Json=" + _imeDao.FilterString(request.Json);
                Log.Debug("NetTV |InsertNetTvPayment: {0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("NetTv DbError| InsertNetTvPayment Exception Arrived of with  exception{0}", ex.Message);
            }
            return response;
        }
    }
}
