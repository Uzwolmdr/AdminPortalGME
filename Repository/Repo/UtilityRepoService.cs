using Newtonsoft.Json;
using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.Deno;
using Repository.Entities.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Repo
{
    public class UtilityRepoService : IUtilityRepoService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<UtilityRepoService>();
        SwiftDao dao;
        ImePayConnection _imeDao;
        public UtilityRepoService()
        {
            dao = new SwiftDao();
            _imeDao = new ImePayConnection();
        }

        public CoreResponse DishhomeRequestLog(DishHomeCoreRequest request)
        {
            CoreResponse Response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_SAVE_DISHHOME_LOG @Flag=" + dao.FilterString(request.Flag);
                sql += ",@UserName=" + dao.FilterString(request.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@Branch=" + dao.FilterString(request.Branch);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@ThirdPartyTxnId=" + dao.FilterString(request.TxnId);
                sql += ",@ReferenceId=" + dao.FilterString(request.ReferenceId);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(request.Status));
                sql += ",@Response=" + dao.FilterString(request.Response);
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                sql += ",@Msisdn=" + dao.FilterString(request.Msisdn);

                
                Log.Information("Insert Dishhome log with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.ExtraId = dbRes.Rows[0]["ID"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Dishhome Log Exception occured while inserting log. Message:{0}", ex.Message);
                return Response;
            }
        }
        public CoreResponse NEARequestLog(NEAServiceRequest request)
        {
            CoreResponse Response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_SAVE_NEA_LOG @Flag=" + dao.FilterString(request.Flag);
                sql += ",@UserName=" + dao.FilterString(request.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@Branch=" + dao.FilterString(request.Branch);
                sql += ",@Msisdn=" + dao.FilterString(request.Msisdn);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@ThirdPartyTxnId=" + dao.FilterString(request.TxnId);
                sql += ",@ReferenceId=" + dao.FilterString(request.ReferenceId);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(request.Status));
                sql += ",@Response=" + dao.FilterString(request.Response);
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                Log.Information("Insert NEARequestLog  with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.ExtraId = dbRes.Rows[0]["ID"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("NEARequestLog Exception occured while inserting log. Message:{0}", ex.Message);
                return Response;
            }
        }
      
        public ApiResponse GetDenos()
        {
            var response = new ApiResponse();
            try
            {
                var sql = "EXEC SW_PROC_APIPRODUCT_DENO";               
                Log.Information("Get GetMobileMenu SQL:{0}", sql);
                var dataSet = _imeDao.ExecuteDataset(sql);

                if (dataSet != null)
                {
                    var neaOffices = new List<NeaOfficeCodeModel>();
                    var topupDenos = new List<TopupDenos>();
                    var minmaxDenos = new List<TopupMinMax>();
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        topupDenos.Add(new TopupDenos
                        {
                            Product = Convert.ToString(dr["Product"]),
                            DenoAmount = Convert.ToString(dr["Deno_Amount"])
                        });
                    }
                    foreach (DataRow dr in dataSet.Tables[1].Rows)
                    {
                        minmaxDenos.Add(new TopupMinMax
                        {
                            Product = Convert.ToString(dr["Product"]),
                            MinAmount = Convert.ToString(dr["Min_Amount"]),
                            MaxAmount = Convert.ToString(dr["Max_Amount"])
                        });
                    }
                    foreach (DataRow dr in dataSet.Tables[2].Rows)
                    {
                        neaOffices.Add(new NeaOfficeCodeModel
                        {
                            Officecode = Convert.ToInt32(dr["Officecode"]),
                            OfficeName = Convert.ToString(dr["OfficeName"])
                        });
                    }
                    var responseDenos = new List<TopupDenosResponse>();
                    foreach (var product in topupDenos.Select(x => x.Product).Distinct().ToArray())
                    {
                        responseDenos.Add(new TopupDenosResponse
                        {
                            Product = product,
                            DenoAmounts = string.IsNullOrEmpty(topupDenos.Where(x => x.Product == product).Select(x => x.DenoAmount).FirstOrDefault()) ? null : topupDenos.Where(x => x.Product == product).Select(x => x.DenoAmount).ToArray()
                        });
                    }
                    response.ResponseData = new
                    {
                        TopupDenos = responseDenos,
                        MinMax = minmaxDenos,
                        NeaOffices = neaOffices
                    };
                    response.ResponseDescription = "Success";
                    response.ResponseCode = "100";
                }
            }
            catch (Exception ex)
            {
                Log.Error("RequestRepository.GetDenoByProduct: Exception{0}", ex.Message);
                response.ResponseDescription = "Error occured";
                response.ResponseCode = "101";
            }
            return response;
        }

        public CoreResponse Reconcile(ReconcileRequestCore request)
        {
            CoreResponse Response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_RECONCILE @Flag=" + dao.FilterString(request.Flag);
                sql += ",@UserName=" + dao.FilterString(request.Username);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@Service=" + dao.FilterString(request.Service);
                sql += ",@Provider=" + dao.FilterString(request.Provider);
                Log.Information("Reconcile Update sql:{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.ExtraId = dbRes.Rows[0]["ID"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Reconcile Update| Exception occured while Reconcilation Process. Message:{0}", ex.Message);
                return Response;
            }
        }
    
        public DbLogResponse InsertGatewayProductPaymentLog(ProductInsertLogRequest request)
        {
            DbLogResponse Response = new DbLogResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_GATEWAY_PRODUCT_PAYMENT_LOG @Flag=" + dao.FilterString(request.Flag);
                sql += ",@UserName=" + dao.FilterString(request.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@Branch=" + dao.FilterString(request.Branch);
                sql += ",@Product=" + dao.FilterString(request.Product);
                sql += ",@ProductType=" + dao.FilterString(request.ProductType);
                sql += ",@Msisdn=" + dao.FilterString(request.Msisdn);
                sql += ",@CustomerId=" + dao.FilterString(request.CustomerId);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@ThirdPartyTxnId=" + dao.FilterString(request.ClientTransactionId);
                sql += ",@ReferenceId=" + dao.FilterString(request.ReferenceId);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(request.Status));
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                sql += ",@Request=" + dao.FilterString(JsonConvert.SerializeObject(request));
                sql += ",@Response=" + dao.FilterString(request.Response);
                sql += ",@MerchantCode=" + dao.FilterString(request.MerchantCode);
                Log.Information("InsertGatewayProductPaymentLog with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.ReferenceId = dbRes.Rows[0]["ID"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("InsertGatewayProductPaymentLog  Exception occured while inserting log. Message:{0}", ex.Message);
               
            }
            return Response;
        }

        public CoreResponse MultipleReconcile(ReconcileMultipleRequest request)
        {
            CoreResponse Response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_MULTIPLE_RECONCILE @Flag=" + dao.FilterString(request.Flag);
                sql += ",@UserName=" + dao.FilterString(request.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@JsonData=" + dao.FilterString(request.JsonData);
                Log.Information("MultipleReconcile Update sql:{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.ExtraId = dbRes.Rows[0]["ID"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("MultipleReconcile Update| Exception occured while Reconcilation Process. Message:{0}", ex.Message);
                return Response;
            }
        }

        public CoreResponse CancelDomesticRemittance(Entities.LoadWallet.CancelDomesticRemittance request)
        {
            CoreResponse response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {

                string sql = "EXEC SW_PROC_MIDDLEWARE_CANCEL_SEND_MONEY";
                sql += " @User=" + dao.FilterString(request.UserName);
                sql += ",@Transaction_ID=" + dao.FilterString(request.TransactionId);
                sql += ",@Flag=" + dao.FilterString("CancelTran");
                Log.Information("CancelDomesticRemittance Update sql:{0}", sql);
                var dbRes = _imeDao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"]);
                    response.ResponseDescription =Convert.ToString(dbRes.Rows[0]["Msg"]);
                    response.ExtraId = Convert.ToString( dbRes.Rows[0]["ID"]);
                    response.Amount =  Convert.ToString(dbRes.Rows[0]["Amount"]);
                }

                
            }
            catch (Exception ex)
            {
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("CancelDomesticRemittance Exception occured  Message:{0}", ex.Message);
                return response;
            }
            return response;
        }

        public CoreResponse InsertGatewayRemittancePaymentLog(Entities.LoadWallet.SendDomesticRemittanceRequestModel request)
        {
            CoreResponse response = new CoreResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Failed"
            };
            try
            {
                string sql = "EXEC SW_PROC_REMITTANCE_TXN @Flag=" + dao.FilterString(request.Flag);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@Msisdn=" + dao.FilterString(request.Msisdn);
                sql += ",@SenderMobileNo=" + dao.FilterString(request.SenderMobileNo);
                sql += ",@SenderName=" + dao.FilterString(request.SenderName);
                sql += ",@ReceiverMobileNo=" + dao.FilterString(request.ReceiverMobileNo);
                sql += ",@ReceiverName=" + dao.FilterString(request.ReceiverName);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@ThirdPartyTxnId=" + dao.FilterString(request.ClientTransactionId);
                sql += ",@Purpose=" + dao.FilterString(request.Purpose);
                sql += ",@Relationship=" + dao.FilterString(request.Relationship);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(request.Status));
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                sql += ",@Request=" + dao.FilterString(JsonConvert.SerializeObject(request));
                sql += ",@Response=" + dao.FilterString(request.Response);
                sql += ",@User=" + dao.FilterString(request.UserName);
                sql += ",@ControlNo=" + dao.FilterString(request.ControlNo);

                Log.Information("InsertGatewayRemittancePaymentLog with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("InsertGatewayRemittancePaymentLog  Exception occured while inserting log. Message:{0}", ex.Message);

            }
            return response;
        }

        public Entities.LoadWallet.SendMoneyEditWrapper GetDomesticRemittanceDetails(string senderMobileNo, string tranId)
        {
            var sql = "EXEC SW_PROC_SEND_MONEY_EDIT";
            sql += " @Flag=" + _imeDao.FilterString("GetDetails");
            sql += ",@SenderMobileNo=" + _imeDao.FilterString(senderMobileNo);
            sql += " ,@SendTransactionId=" + _imeDao.FilterString(tranId);
            Log.Information("GetSendMoneyDetail  sql:{0}", sql);
            var dataTable = _imeDao.ExecuteDataset(sql);
            var res = dataTable.Tables[0].Rows[0];
            Entities.LoadWallet.SendMoneyEditWrapper wrapper = new Entities.LoadWallet.SendMoneyEditWrapper();

            List<Entities.LoadWallet.RemarksDetails> remark = new List<Entities.LoadWallet.RemarksDetails>();
            Entities.LoadWallet.RemitTransaction remitDetail = new Entities.LoadWallet.RemitTransaction();
            if (res != null)
            {
                remitDetail.SenderName = res["Sender_Name"].ToString();
                remitDetail.SenderMsisdn = res["Sender_Msisdn"].ToString();
                remitDetail.ReceiverName = res["Receiver_Name"].ToString();
                remitDetail.ReceiverMsisdn = res["Receiver_Msisdn"].ToString();
                remitDetail.TransactionId = res["Send_Transaction_Id"].ToString();
                remitDetail.ControlNo = res["Otp"].ToString();
                remitDetail.Amount = res["Amount"].ToString();
                wrapper.ResponseCode = res["Code"].ToString();
                wrapper.ResponseDescription = res["Msg"].ToString();
            }
            if (dataTable.Tables.Count > 1)
            {
                foreach (DataRow dr in dataTable.Tables[1].Rows)
                {
                    Entities.LoadWallet.RemarksDetails detail = new Entities.LoadWallet.RemarksDetails();
                    detail.Remarks = dr["Message"].ToString();
                    detail.UpdatedBy = dr["UpdatedBy"].ToString();
                    detail.UpdatedDate = dr["UpdateDate"].ToString();
                    remark.Add(detail);
                }
            }
            wrapper.RemitTransaction = remitDetail;
            wrapper.Details = remark;
            return wrapper;
        }

        public DbLogResponse UpdateDomesticRemittanceDetails(Entities.LoadWallet.EditRemitTransaction request)
        {
            var pin = Repository.Config.ConfigurationManager.AppSettings["Pin"];
            var dbResponse = new DbLogResponse
            {
                ResponseCode = 107,
                ResponseDescription = "Invalid Input"
            };

            var sql = "EXEC SW_PROC_SEND_MONEY_EDIT";
            sql += " @Flag=" + _imeDao.FilterString("DirectUpdate");
            sql += ",@SenderMobileNo=" + _imeDao.FilterString(request.SenderMsisdn);
            sql += ",@SenderName=" + _imeDao.FilterString(request.SenderName);
            sql += ",@ReceiverMobileNo=" + _imeDao.FilterString(request.ReceiverMsisdn);
            sql += ",@ReceiverName=" + _imeDao.FilterString(request.ReceiverName);
            sql += ",@User=" + _imeDao.FilterString("middleware");
            sql += ",@Remarks=" + _imeDao.FilterString(request.Remarks);
            sql += ",@ImePayNo=" + _imeDao.FilterString(request.ControlNo);
            sql += ",@SendTransactionId=" + _imeDao.FilterString(request.TransactionId);
            sql += ",@Pin=" + _imeDao.FilterString(pin);
            Log.Information("UpdateSendMoney Data SQL:{0}", sql);
            var res = _imeDao.ExecuteDataRow(sql);
            if (res != null)
            {

                dbResponse.ResponseCode = Convert.ToInt16(res["Code"]);
                dbResponse.ResponseDescription = res["Msg"].ToString();
            }

            if(dbResponse.ResponseCode==100)
            {
                var sql2 = "EXEC SW_PROC_REMITTANCE_TXN";
                sql2 += " @Flag=" + dao.FilterString("UpdateSendMoney");
                sql2 += ",@SenderMobileNo=" + dao.FilterString(request.SenderMsisdn);
                sql2 += ",@SenderName=" + dao.FilterString(request.SenderName);
                sql2 += ",@ReceiverMobileNo=" + dao.FilterString(request.ReceiverMsisdn);
                sql2 += ",@ReceiverName=" + dao.FilterString(request.ReceiverName);
                sql2 += ",@Remarks=" + dao.FilterString(request.Remarks);
                sql2 += ",@ImePayNo=" + dao.FilterString(request.ControlNo);
                sql2 += ",@SendTransactionId=" + dao.FilterString(request.TransactionId);
                Log.Information("UpdateSendMoney Data To Midddleware SQL:{0}", sql);
            }
            return dbResponse;
        }
        public StatusResponse GetDomesticRemitTransactionStatus(StatusRequest request)
        {
            StatusResponse _resp = new StatusResponse();
            var sql = "SW_PROC_MIDDLEWARE_CANCEL_SEND_MONEY @Flag ='TxnStatus'";
            sql += " ,@Transaction_ID= " + dao.FilterString(request.TxnId);

            Log.Information("GetTransactionStatus with  Query :{0}", sql);

            try
            {
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    _resp.ExtraId = dr["TransactionId"].ToString();
                    _resp.ResponseCode = Convert.ToInt32(dr["Code"].ToString());
                    _resp.ResponseDescription = dr["Msg"].ToString();
                    _resp.Date = String.IsNullOrEmpty(Convert.ToString(dr["CreatedDate"])) ? "" : Convert.ToDateTime(dr["CreatedDate"]).ToString("yyyy-MM-dd hh:mm:ss tt");
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
       
    }
}
