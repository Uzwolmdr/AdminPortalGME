using Newtonsoft.Json;
using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Internet;
using Repository.Entities.Tv;
using System;

namespace Repository.Repo.Internet
{
    public class InternetRepo : IInternetRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<InternetRepo>();
        SwiftDao dao;
        ImePayConnection _imeDao;
        public InternetRepo()
        {
            dao = new SwiftDao();
            _imeDao = new ImePayConnection();
        }
        public DbLogResponse InsertClassicTechPaymentImePayDb(ClassicTechPaymentRequest request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Error",
                ReferenceId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_CLASSIC_TECH_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Username=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@MSISDN=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Month=" + _imeDao.FilterString(request.Month);
                sql += " ,@Package=" + _imeDao.FilterString(request.Package);
                sql += " ,@Token=" + _imeDao.FilterString(request.Token);
                sql += " ,@CreatedBy=" + _imeDao.FilterString(request.CreatedBy);

                Log.Information("InsertClassicTechPaymentToImePayDb  with Query :{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured  with error :", ex.Message.ToString());
            }
            return response;
        }

       

        public DbLogResponse InsertSubisuPaymentImePayDb(SubisuPaymentRequest request)
        {
            var res = new DbLogResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "INVALID INPUT"
            };
            try
            {
                var sql = string.Format("[SW_PROC_SUBISU_PAYMENT]");
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@CustomerId=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@PaymentFor=" + _imeDao.FilterString(request.PaymentFor);
                sql += " ,@CreatedBy	=" + _imeDao.FilterString(request.CreatedBy);
                Log.Information("Insert Subisu log with Query :{0}", sql);
                var dbRes = _imeDao.ExecuteDataRow(sql);

                if (dbRes != null)
                {
                    res.ResponseCode = Convert.ToInt32(dbRes["ResponseCode"]);
                    res.ResponseDescription = Convert.ToString(dbRes["ResponseDescription"]);
                    res.ReferenceId = Convert.ToString(dbRes["Id"]);
                }

                return res;
            }

            catch (Exception ex)
            {
                res.ResponseCode = ResponseCodeConstant.EXCEPTION;
                res.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error(string.Format("Subisu| Exception occured while inserting log. Message:{0}", ex.Message));
                return res;
            }
        }
        public DbLogResponse InsertVianetPaymentRequest(VianetPaymentRequest request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = ResponseCodeConstant.FAILED,
                ResponseDescription = "Error",
                ReferenceId = ""
            };
            var sql = "EXEC SW_PROC_VIANET_PAYMENT";
            sql += " @Flag=" + dao.FilterString("insertRecord");
            sql += " ,@MSISDN=" + dao.FilterString(request.Msisdn);
            sql += " ,@CustomerId=" + dao.FilterString(request.CustomerId);
            sql += " ,@PaymentId=" + dao.FilterString(request.PaymentId);
            sql += " ,@Amount=" + dao.FilterString(request.Amount);
            sql += " ,@RequestJson=" + dao.FilterString(request.Json);
            try
            {
                Log.Information("InsertVianetReponse SQL:{0}", sql);
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
                Log.Error("InsertVianetReponse Error occured:{0}", ex.Message);

            }
            return response;
        }

        public DbLogResponse InsertParbhuNetData(ParbhuNetPaymentRequestModel model)
        {
            var res = new DbLogResponse()
            {
                ResponseCode = 999,
                ResponseDescription = "INVALID INPUT"
            };
            try
            {
                var sql = "EXEC [SW_PROC_BARAHI_INTERNET_PAYMENT]";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Username=" + _imeDao.FilterString(model.CustomerId);
                sql += " ,@CustomerName=" + _imeDao.FilterString(model.CustomerName);
                sql += " ,@PackageId=" + _imeDao.FilterString(model.PackageId);
                sql += " ,@Amount=" + _imeDao.FilterString(model.Amount);
                sql += " ,@PackageName=" + _imeDao.FilterString(model.PackageName);
                sql += " ,@CreatedBy=" + _imeDao.FilterString(model.CreatedBy);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(model.Msisdn);
                sql += " ,@RequestJson	=" + _imeDao.FilterString(model.Json);
                Log.Information("Barahi Internet SQL:{0}", sql);
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
                Log.Error("Exception in method:InsertBarahiInternetRequest|Error:{0}", ex.Message);
            }
            return res;
        }

        public DbLogResponse InsertWebserverInternetData(WebServerPaymentRequestModel request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode     = 101,
                ResponseDescription = "Erorr",
                ReferenceId = ""

            };
            try
            {
                var sql = "EXEC SW_PROC_WEBSURFER_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Username=" + _imeDao.FilterString(request.User);
                sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@PlanName=" + _imeDao.FilterString(request.Plan);
                sql += " ,@MSISDN=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerId=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@PaymentCode=" + _imeDao.FilterString(request.BouquetId);
                sql += " ,@CheckoutId=" + _imeDao.FilterString(request.CheckoutId);
                sql += " ,@Request=" + _imeDao.FilterString(request.Request);
                Log.Information("InsertWebsurferRequest SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
            }
            catch(Exception ex)
            {
                Log.Error("Exception in method:|Error:{0}", ex.Message);
            }
            return response;
        }

        public DbLogResponse InsertPalsNetRequestData(PalsNetPaymentRequestModel request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "INVALID INPUT",
                ReferenceId = ""

            };
            try
            {
                var sql = "EXEC SW_PROC_PALSNET_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Msisdn=" + _imeDao.FilterString(request.CreatedBy);
                sql += " ,@User=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@UserName=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@UserId=" + _imeDao.FilterString(request.UserId);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                Log.Information("InsertPalsnetPaymentDetails SQL:{0}", sql);
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
                Log.Error("Exception in method:InsertPalsnetPaymentDetails|Error:{0}", ex.Message);
            }

            return response;
        }

        public DbLogResponse InsertTechMindsData(TechMindsPaymentRequestModel request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Erorr",
                ReferenceId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_TECHMINDS_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Username=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@MSISDN=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Durration=" + _imeDao.FilterString(request.Duration);
                sql += " ,@Channel=" + _imeDao.FilterString("Middleware");
                Log.Information("insertRecord SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occur insert techmind data with :{0}", ex.Message.ToString());
            }
            return response;
        }

        public DbLogResponse InsertLoopInternetData(LoopInternetPaymentRequestModel request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "INVALID INPUT",
                ReferenceId = ""

            };
            try
            {
                var sql = "EXEC SW_PROC_LOOP_NETWORK_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@MSISDN=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@User=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@UserName=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@UserId=" + _imeDao.FilterString(request.UserId);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Json=" + _imeDao.FilterString(request.Json);
                Log.Information("InsertLoopNetworkReponse SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt16(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception in method:InsertLoopNetwrokDetail|Error:{0}", ex.Message);
            }

            return response;
        }

        public DbLogResponse InsertFirstLinkData(FirstLinkPaymentRequestModel request)
        {
            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                var sql = "SW_PROC_FIRSTLINK_PAYMENT @Flag=" + _imeDao.FilterString("insertRecord");
                sql += ",@CreatedBy=" + _imeDao.FilterString(request.CreatedBy);
                sql += ",@Amount=" + _imeDao.FilterString(request.Amount);
                sql += ",@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += ",@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@PackagePlanName=" + _imeDao.FilterString(request.PackagePlanName);
                sql += ",@PackageSaleId=" + _imeDao.FilterString(request.PackageId);
                sql += ",@UserId=" + _imeDao.FilterString(request.UserId);
                sql += ",@UserName=" + _imeDao.FilterString(request.CustomerId);
                sql += ",@Json=" + _imeDao.FilterString(request.Json);
                Log.Information("FIRSTLINK |InsertPaymentRequest: {0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt16(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error("FIRSTLINK DbError| InsertPaymentRequest Exception Arrived of with  exception{0}", ex.Message);
            }
            return response;
        }

        public DbLogResponse InsertArrowNetData(ArrowNetPaymentModel request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Erorr",
                ReferenceId = ""
            };
            var sql = "EXEC SW_PROC_ARROWNET_PAYMENT";
            sql += " @Flag=" + _imeDao.FilterString("insertRecord");
            sql += " ,@Username=" + _imeDao.FilterString(request.CustomerId);
            sql += " ,@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
            sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
            sql += " ,@AdvancePayment=" + request.AcceptAdvancePayment;
            sql += " ,@HasDue=" + request.HasDue;
            sql += " ,@Json=" + _imeDao.FilterString(request.Json);
            sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
            sql += " ,@PlanName=" + _imeDao.FilterString(request.PlanName);
            sql += " ,@MSISDN=" + _imeDao.FilterString(request.Msisdn);
            sql += " ,@Durration=" + _imeDao.FilterString(request.Duration);
            Log.Information("insertRecord SQL:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                response.ReferenceId = Convert.ToString(dr["Id"]);
            }
            return response;
        }

        public DbLogResponse InsertDishHomeFiberNetRequestData(DishHomeFiberNetPaymentModel request)
        {
            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal error"
            };
            try
            {
                var sql = "SW_PROC_DISH_HOME_FIBER_NET_PAYMENT @Flag=" + _imeDao.FilterString("insertRecord");
                sql += ",@Msisdn=" + _imeDao.FilterString(request.CreatedBy);
                sql += ",@Amount=" + _imeDao.FilterString(request.Amount);
                sql += ",@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += ",@CustomerMobileNo=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@PackagePlanName=" + _imeDao.FilterString(request.PackagePlan);
                sql += ",@Cid=" + _imeDao.FilterString(request.CustomerId);
                sql += ",@Json=" + _imeDao.FilterString(request.Json);
                Log.Information("DishHomeFiberNet : {0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dr["ResponseDescription"]);
                    response.ReferenceId = Convert.ToString(dr["RefId"]);
                    
                }
            }
            catch (Exception ex)
            {
                Log.Error("DishHomeFiberNet DbError|  Exception Arrived of with  exception{0}", ex.Message);
            }
            return response;
        }

        public DbLogResponse InsertReliantInternetRequestData(ReliantInternetPaymentModel request)
        {
            var res = new DbLogResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "INVALID INPUT"
            };
            try
            {
                var sql = "EXEC [SW_PROC_RELIANT_PAYMENT]";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@Username=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@Address=" + _imeDao.FilterString(request.Address);
                sql += " ,@CreatedBy	=" + _imeDao.FilterString(request.CreatedBy);
                sql += " ,@PaymentJson	=" + _imeDao.FilterString(request.Json);
                Log.Information("Reliant Payment SQL:{0}", sql);
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
                Log.Error("Exception in method:InsertReliantRequest|Error:{0}", ex.Message);
            }
            return res;
        }

        public DbLogResponse InsertSubisuPaymentImePayDbV2(SubisuPaymentRequestV2 request)
        {
            var res = new DbLogResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "INVALID INPUT"
            };
            try
            {
                var sql = "EXEC [SW_PROC_SUBISU_PAYMENT_V2]";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += " ,@CustomerId=" + _imeDao.FilterString(request.CustomerId);
                sql += " ,@CustomerNo=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += " ,@Amount=" + _imeDao.FilterString(request.Amount);
                sql += " ,@PaymentFor=" + _imeDao.FilterString(request.PaymentFor);
                sql += " ,@CreatedBy	=" + _imeDao.FilterString(request.Msisdn);
                sql += " ,@Token	=" + _imeDao.FilterString(request.Token);
                sql += " ,@ObjectId	=" + _imeDao.FilterString(request.ObjectId);
                sql += " ,@Json	=" + _imeDao.FilterString(request.Json);
                Log.Information("InsertSubisuDetailv2 Payment SQL:{0}", sql);

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
                Log.Error("Exception in method:InsertSubisuDetailv2", ex);
            }
            return res;
        }
    }
}
