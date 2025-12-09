using Repository.Connection;
using Repository.Entities;
using Repository.Entities.PurseMgt;
using Repository.Helper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repo.PurseMgt
{
    public class PurseMgtRepo : IPurseMgtRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<PurseMgtRepo>();

        SwiftDao dao;
        ReportDao reportDao;
        public PurseMgtRepo()
        {
            dao = new SwiftDao();
            reportDao = new ReportDao();
        }


        public DBResponse DoPurseTransaction(PurseMgtRequestModel request)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_DO_PURSE_TRAN @Flag=" + dao.FilterString(request.Flag);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@DrCr=" + dao.FilterString(request.DrCr);
                sql += ",@TransactionId=" + dao.FilterString(request.TransactionId);
                sql += ",@ThirdPartyTxnId=" + dao.FilterString(request.ThirdPartyTxnId);
                sql += ",@TxnType=" + dao.FilterQuote(Convert.ToString(request.TxnType));
                sql += ",@Amount=" + dao.FilterString(request.Amount);
                sql += ",@ProductId=" + dao.FilterString(request.ProductId);
                Log.Information("DoPurseTransaction with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("DoPurseTransaction Exception occurred with error message :{0}", ex.Message);
                return Response;
            }
        }

        public DBResponse AddWithdrawPurseMoney(PurseMgtRequestModel request)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_DO_PURSE_TRAN @Flag=" + dao.FilterString("PurseDrCr");
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@DrCr=" + dao.FilterString(request.DrCr);
                sql += ",@User=" + dao.FilterString(request.UserName);
                sql += ",@Amount=" + dao.FilterQuote(Convert.ToString(request.Amount));
                sql += ",@TransactionId=" + dao.FilterQuote(Convert.ToString(request.TransactionId));
                sql += ",@Reference=" + dao.FilterString(Convert.ToString(request.Reference));
                Log.Information("AddWithdrawPurseMoney with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("AddWithdrawPurseMoney execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }
        public PurseDrCrResponseWrapper GetPendingAddWithdrawPurseMoney(PurseMgtRequestModel requsetModel)
        {

            var purseResponse = new PurseDrCrResponseWrapper
            {
                ResponseCode = 101,
                ResponseDescription = "No List Found"
            };
            var responseParam = new List<PurseDrCrResponse>();

            var sql = "SW_PROC_DO_PURSE_TRAN @Flag ='GetPendingList'";
            sql += " ,@User= " + dao.FilterString(requsetModel.UserName);
            sql += " ,@OrganisationCode= " + dao.FilterString(requsetModel.OrganisationCode);


            Log.Information("GetPendingAddWithdrawPurseMoney with  Query :{0}", sql);

            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new PurseDrCrResponse
                        {
                            RowId = dr["RowId"].ToString(),
                            OrganisationCode = dr["OrganisationCode"].ToString(),
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            DrCr = Convert.ToString(dr["DrCr"]),
                            TransactionId = dr["TransactionId"].ToString(),
                            CreatedBy = dr["CreatedBy"].ToString(),
                            CreatedDate = Utilities.FormatDateTime(Convert.ToString(dr["CreatedDate"])),
                            Reference = Convert.ToString(dr["Reference"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("AddWithdrawPurseMoney execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                purseResponse.ResponseCode = 100;
                purseResponse.ResponseDescription = "Success";
            }
            purseResponse.PendingList = responseParam;
            return purseResponse;
        }
        public DBResponse ApproveAddWithdrawPurseMoney(PurseMgtRequestModel request)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_DO_PURSE_TRAN @Flag=" + dao.FilterString("Approve");
                sql += ",@RowId=" + dao.FilterString(request.TransactionId);
                sql += ",@User=" + dao.FilterString(request.UserName);
                Log.Information(string.Format("ApproveAddWithdrawPurseMoney with Query :{0}", sql));
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("ApproveAddWithdrawPurseMoney execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public DBResponse RejectAddWithdrawPurseMoney(PurseMgtRequestModel request)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_DO_PURSE_TRAN @Flag=" + dao.FilterString("Reject");
                sql += ",@RowId=" + dao.FilterString(request.TransactionId);
                sql += ",@User=" + dao.FilterString(request.UserName);
                sql += ",@Remarks=" + dao.FilterString(request.Remarks);
                Log.Information("RejectAddWithdrawPurseMoney with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("RejectAddWithdrawPurseMoney execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public string CreateTransactionId()
        {
            string tranId = "";
            try
            {
                string sql = "EXEC SW_PROC_CREATE_TRAN_ID";
                Log.Information("CreateTransactionId with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    tranId = Convert.ToString(dbRes.Rows[0]["TranId"].ToString());
                }

            }
            catch (Exception ex)
            {
                Log.Error("CreateTransactionId execption occured with Message:{0}", ex.Message);

            }
            return tranId;
        }

        public SoaWrapperResponse GetSoaDetails(SoaRequest request)
        {
            SoaWrapperResponse soaResponse = new SoaWrapperResponse();
            string sql = "SW_PROC_DO_PURSE_TRAN @Flag ='GetSOA'";
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            Log.Information("GetSoaDetails with  Query :{0}", sql);

            List<SoaDetails> responseParam = new List<SoaDetails>();
            try
            {
                var dataset = reportDao.ExecuteDataset(sql);

                if (dataset != null)
                {
                    if (dataset.Tables[0] != null)
                    {
                        soaResponse.OpeningBalance = Utilities.FormatCurrency(Convert.ToDecimal(dataset.Tables[0].Rows[0]["Balance"]));
                    }



                    foreach (DataRow dr in dataset.Tables[1].Rows)
                    {
                        responseParam.Add(new SoaDetails
                        {
                            Amount = Utilities.FormatCurrency(Convert.ToDecimal(dr["Amount"])),
                            TransactionDate = Utilities.FormatDateTime(Convert.ToString(dr["TxnDate"])),
                            DrCr = Convert.ToString(dr["DrCr"]),
                            TransactionId = Convert.ToString(dr["TxnId"]),
                            TpTransactionId = dr["TpTxnId"].ToString(),
                            TxnType = dr["TxnType"].ToString(),
                            Balanace = Utilities.FormatCurrency(Convert.ToDecimal(dr["Balance"])),
                            ProductId = Convert.ToString(dr["ProductId"])
                        });
                    }

                    if (dataset.Tables[2] != null)
                    {
                        soaResponse.ClosingBalance = Utilities.FormatCurrency(Convert.ToDecimal(dataset.Tables[2].Rows[0]["ClosingBalance"]));
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("GetSoaDetails execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                soaResponse.ResponseCode = 100;
                soaResponse.ResponseDescription = "Success";
                soaResponse.SoaDetails = responseParam;
            }
            else
            {
                soaResponse.ResponseCode = 101;
                soaResponse.ResponseDescription = "Success";
            }
            return soaResponse;
        }


     public PurseBalanceResponse GetPurseBalance(PurseBalanceRequest request)
        {
            PurseBalanceResponse Response = new PurseBalanceResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error",
                CurrentBalance = ""

            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'GetPurseBalance'";
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                Log.Information("GetPurseBalance with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                    Response.CurrentBalance = Utilities.FormatCurrency(Convert.ToDecimal(dbRes.Rows[0]["PurseAmount"]));
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("GetPurseBalance execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public AllOrgPurseBlncWrapper GetAllOrgPurseBalance()
        {
            var purseResponse = new AllOrgPurseBlncWrapper
            {
                ResponseCode = 101,
                ResponseDescription = "No List Found",
               Data = new List<AllOrgPurseBalance>()
            };
            var responseParam = new List<AllOrgPurseBalance>();

            var sql = "SW_PROC_USER_MGT @Flag ='GetAllOrgBalance'";
            Log.Information("GetAllOrgPurseBalance with  Query :{0}", sql);

            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new AllOrgPurseBalance
                        {
                            OrganisationCode = dr["OrganisationCode"].ToString(),
                            CurrentBalance = Utilities.FormatCurrency(Convert.ToDecimal(dr["PurseAmount"])),
                            OrganisationName = dr["Organisation"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetAllOrgPurseBalance execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                purseResponse.ResponseCode = 100;
                purseResponse.ResponseDescription = "Success";
            }
            purseResponse.Data = responseParam;
            return purseResponse;
        }

      
    }
}

