using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Ticketing;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repo.Ticketing
{
    public class TicketingRepo : ITicketingRepo
    {

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<TicketingRepo>();
        ImePayConnection _imeDao;
        SwiftDao  swiftDao;
        public TicketingRepo()
        {
            _imeDao = new ImePayConnection();
            swiftDao = new SwiftDao();
        }

        

        public DbLogResponse InsertChandragiriData(CableCarPaymentRequestModel request)
        {
            var ecomRequest = new CableCarTicketPostedToEcom();
            ecomRequest.CustomerName = request.CustomerName;
            ecomRequest.CustomerPhone = request.Msisdn;
            ecomRequest.CustomerEmail = request.CustomerEmail;
            ecomRequest.SearchId = request.SearchId;
            ecomRequest.TotalPrice = request.Amount;
            ecomRequest.UserWalletId = request.CreatedBy;
            ecomRequest.TicketDetails = request.TicketDetails;

            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };

            if (request.TicketDetails.Count <= 0)
            {
                response.ResponseDescription = "Please select at least one ticket.";
                return response;
            }
            if (Convert.ToDecimal(request.Amount) != GetTotalPrice(request.TicketDetails))
            {
                response.ResponseDescription = "Total Amount and total price of all ticket not matched.";
                return response;
            }

            try
            {
                string sql = "EXEC SW_PROC_CHANDRAGIRI_TICKETING";
                sql += " @Flag='InsertBookingRequest'";
                sql += ",@Json_Data=" + _imeDao.FilterString(JsonConvert.SerializeObject(ecomRequest));
                sql += ",@User=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@Reference_Id=" + _imeDao.FilterString(request.SearchId);

                Log.Information("InsertChandragiriTicketBookRequest data sql:{0}", sql);
                var ds = _imeDao.ExecuteDataset(sql);
                if (ds != null)
                {
                    var dt1 = ds.Tables[0];
                    var dt2 = ds.Tables[1];
                    response.ResponseCode = Convert.ToInt16(dt1.Rows[0]["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dt1.Rows[0]["ResponseMessage"]);
                    response.ReferenceId = Convert.ToString(dt1.Rows[0]["RefId"]);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error | InsertChandragiriTicketBookRequest errorMessage:{0}", ex.Message);
            }
            return response;
        }

        public DbLogResponse InsertManakamanaData(CableCarPaymentRequestModel request)
        {
            var ecomRequest = new CableCarTicketPostedToEcom();
            ecomRequest.CustomerName = request.CustomerName;
            ecomRequest.CustomerPhone = request.Msisdn;
            ecomRequest.CustomerEmail = request.CustomerEmail;
            ecomRequest.SearchId = request.SearchId;
            ecomRequest.TotalPrice = request.Amount;
            ecomRequest.UserWalletId = request.CreatedBy;
            ecomRequest.TicketDetails = request.TicketDetails;
           
            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };

            if (request.TicketDetails.Count <= 0)
            {
                response.ResponseDescription = "Please select at least one ticket.";
                return response;
            }
            if (Convert.ToDecimal(request.Amount) != GetTotalPrice(request.TicketDetails))
            {
                response.ResponseDescription = "Total Amount and total price of all ticket not matched.";
                return response;
            }

            try
            {
                string sql = "EXEC SW_PROC_MANAKAMANA_TICKETING";
                sql += " @Flag='InsertBookingRequest'";
                sql += ",@Json_Data=" + _imeDao.FilterString(JsonConvert.SerializeObject(ecomRequest));
                sql += ",@User=" + _imeDao.FilterString(request.Msisdn);
                sql += ",@Reference_Id=" + _imeDao.FilterString(request.SearchId);

                Log.Information("InsertManakamanaTicketBookRequest data sql:{0}", sql);
                var ds = _imeDao.ExecuteDataset(sql);
                if (ds != null)
                {
                    var dt1 = ds.Tables[0];
                    var dt2 = ds.Tables[1];
                    response.ResponseCode = Convert.ToInt16(dt1.Rows[0]["ResponseCode"]);
                    response.ResponseDescription = Convert.ToString(dt1.Rows[0]["ResponseMessage"]);
                    response.ReferenceId = Convert.ToString(dt1.Rows[0]["RefId"]);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error | InsertManakamanaTicketBookRequest errorMessage:{0}", ex.Message);
            }
            return response;
        }

        private decimal GetTotalPrice(List<CableCarBookTicket> ticketDetails)
        {
            decimal totalPrice = 0;
            foreach (var item in ticketDetails)
            {
                totalPrice = totalPrice + Convert.ToInt32(item.PassengerCount) * Convert.ToDecimal(item.TripPrice);
            }
            return totalPrice;
        }


        public AirlineConfig GetSectorAndAirlines()
        {
            AirlineConfig config = new AirlineConfig();

            var airlinesSectors = new List<AirlineSector>();
            var sql = "EXEC SW_PROC_AIRLINES_TICKETING @Flag='getSector' ";
            sql += ",@User = '' ";
            Log.Information("GetSector SQL:{0}", sql);
            var res = _imeDao.ExecuteDataTable(sql);
            if (res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    AirlineSector sector = new AirlineSector();
                    sector.Code = dr["value"].ToString();
                    sector.Name = dr["text"].ToString();
                    airlinesSectors.Add(sector);
                }
            }

            config.Sectors = airlinesSectors;

            var airlinesList = new List<AirlineCarrier>();
            var sql1 = "EXEC SW_PROC_AIRLINES_TICKETING @Flag='getAirlines' ";
            Log.Information("GetRegisteredAirline SQL:{0}", sql1);
            var res1 = _imeDao.ExecuteDataTable(sql1);
            if (res1.Rows.Count > 0)
            {
                foreach (DataRow dr in res1.Rows)
                {
                    var airlines = new AirlineCarrier();
                    airlines.Code = dr["Code"].ToString();
                    airlines.WalletCode = dr["Wallet_Code"].ToString();
                    airlines.Name = dr["Name"].ToString();

                    airlinesList.Add(airlines);
                }
            }
            config.Carriers = airlinesList;

            return config;
        }

        public string SaveFlightSearchResponse(string searchParam, string flightData)
        {
            var id = "-1";

            var sql = "SW_PROC_DOMESTIC_FLIGHT_SEARCH @Flag=" + _imeDao.FilterString("i");

            sql += ",@searchParam='" + searchParam + "'";
            sql += ",@flightData='" + flightData + "'";
            Log.Information("SaveFlightSearchResponse data sql:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                return Convert.ToString(dr[0]);
            }
            return id;
        }

        public string GetParentAirlinesCode(string code)
        {
            var sql = "";
            Log.Information("Starting operation to retrieve the Wallet code for merchant code: {0}", code);

            sql += string.Format("SW_PROC_GET_AIRLINES_NAME_BY_CODE @Flag={0}, @Merchant_Code={1}", "getWalletCode", code);
            Log.Information("GetWalletCode Data SQL:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                return dr["Wallet_Code"].ToString();
            }
            else
            {
                Log.Error("No Wallet Merchant Code setup for the merchant code {0}", code);
                throw new System.InvalidOperationException("No Wallet Code setup for the merchant code: " + code);
            }
        }

       

        public JsonResponse InsertAirlinesTicketingData(IssueTicketModel issueTicketModel)
        {
            var response = new JsonResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            var sql = "SW_PROC_AIRLINES_TICKETING @Flag=" + _imeDao.FilterString("insertRecord");

            sql += ",@ContactName=" + _imeDao.FilterString(issueTicketModel.ContactPersonName);
            sql += ",@ContactEmail=" + _imeDao.FilterString(issueTicketModel.ContactPersonEmail);
            sql += ",@ContactPhone=" + _imeDao.FilterString(issueTicketModel.ContactPersonMobileNo);
            sql += ",@FlightId=" + _imeDao.FilterString(issueTicketModel.FlightId);
            sql += ",@ReturnFlightId=" + _imeDao.FilterString(issueTicketModel.ReturnFlightId);
            sql += ",@PassangerDetail=" + _imeDao.FilterString(JsonConvert.SerializeObject(issueTicketModel.PassangerList));
            sql += ",@User=" + _imeDao.FilterString(issueTicketModel.CreatedBy);
            sql += ",@Total_Amount=" + _imeDao.FilterString(issueTicketModel.GrandTotal.ToString());
            sql += ",@Flight_Reserve_Request=" + _imeDao.FilterString(issueTicketModel.ReserveFlightRequest.ToString());
            if (!string.IsNullOrEmpty(issueTicketModel.InboundDate))
                sql += ",@InboundDate=" + _imeDao.FilterString(Convert.ToDateTime(issueTicketModel.InboundDate).ToString());
            if (!string.IsNullOrEmpty(issueTicketModel.OutboundDate))
                sql += ",@OutboundDate=" + _imeDao.FilterString(Convert.ToDateTime(issueTicketModel.OutboundDate).ToString());
            Log.Information("IssueTicket data sql:{0}", sql);
            var dr = _imeDao.ExecuteDataRow(sql);
            if (dr != null)
            {
                response.ResponseCode = Convert.ToInt16(dr["Code"]);
                response.ResponseDescription = dr["Msg"].ToString();
                response.FlightIssueId = dr["Id"].ToString();
            }
            return response;
        }

        public object GetInvoiceDetails(string tranId, string flag)
        {
            var response = new object();
            var sql = "EXEC SW_PROC_GET_MANAKAMANA_CHANDRA_INVOICE_DETAILS";
            sql += " @Flag=" + _imeDao.FilterString(flag);
            sql += " ,@TransactionId=" + _imeDao.FilterString(tranId);

            Log.Information("GetInvoiceDetails Request SQL:{0}", sql);

            var dr = swiftDao.ExecuteDataRow(sql);
            if ( !string.IsNullOrEmpty(Convert.ToString(dr["Ticket_Invoice"])))
            {
                response = JObject.Parse( Convert.ToString(dr["Ticket_Invoice"]));
            }
            return response;
        }

        public BaseApiResponse GetMyCableCarTicketByTranId(MyTicketRequest request)
        {

            var response = new BaseApiResponse
            {
                ResponseCode = "101",
                ResponseDescription = "Ticketing details not found"
            };
            ;
            var sql = "EXEC SW_PROC_GET_MANAKAMANA_CHANDRA_INVOICE_DETAILS";
            sql += " @Flag=" + _imeDao.FilterString(request.Flag);
            sql += " ,@TransactionId=" + _imeDao.FilterString(request.TransactionId);

            Log.Information("GetInvoiceDetails Request SQL:{0}", sql);

            var ticketDetails = new CableCarTicket();
            var dr = swiftDao.ExecuteDataRow(sql);
            if (!string.IsNullOrEmpty(Convert.ToString(dr["Ticket_Invoice"])))
            {
                ticketDetails.TransactionId = Convert.ToString(dr["Transaction_Id"]);
                ticketDetails.PurchasedDate = Convert.ToString(dr["PurchasedDate"]);
                ticketDetails.ExpiryDate = Convert.ToString(dr["PurchasedDate"]);
                ticketDetails.IsExpired = Convert.ToBoolean(dr["IsExpired"]);
                ticketDetails.TicketDetails = JObject.Parse(Convert.ToString(dr["Ticket_Invoice"]));
                response.ResponseCode = "100";
                response.ResponseDescription = "Success";
                response.ResponseData = ticketDetails;
            }
            return response;


        }

        public BaseApiResponse GetMyCableCarTickets(MyTicketRequest request)
        {
            var response = new BaseApiResponse
            {
                ResponseCode = "101",
                ResponseDescription = "No tickets found",
                ResponseData = new List<CableCarTicket>()
            };
            var sql = "EXEC SW_PROC_GET_MANAKAMANA_CHANDRA_INVOICE_DETAILS";
            sql += " @Flag=" + _imeDao.FilterString(request.Flag);
            sql += " ,@Msisdn=" + _imeDao.FilterString(request.Msisdn);

            Log.Information("GetMyCableCarTickets Request SQL:{0}", sql);

            var dt = swiftDao.ExecuteDataTable(sql);

            if (dt != null)
            {
                var myTickets = new List<CableCarTicket>();
                foreach (DataRow dr in dt.Rows)
                {
                    myTickets.Add(new CableCarTicket
                    {
                        TransactionId = Convert.ToString(dr["Transaction_Id"]),
                        PurchasedDate = Convert.ToString(dr["PurchasedDate"]),
                        TicketName = Convert.ToString(dr["TicketName"]),
                        ExpiryDate = Convert.ToString(dr["PurchasedDate"]),
                        IsExpired = Convert.ToBoolean(dr["IsExpired"]),
                        TicketDetails =   JObject.Parse(Convert.ToString(dr["Ticket_Invoice"]))

                    });
                }
                if (myTickets.Count > 0)
                {
                    response.ResponseCode = "100";
                    response.ResponseDescription = "Success";
                    response.ResponseData = myTickets;
                }
            }
            return response;

        }

        public DbLogResponse AirlinesSettlementDetails(AirlinesSettlementRequest request)
        {
            var response = new DbLogResponse
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                var sql = "SW_PROC_AIRLINES_TICKETING_DETAIL @Flag=" + swiftDao.FilterString("insertRecord");
                sql += ",@OrganisationCode=" + swiftDao.FilterString(request.OrganisationCode);
                sql += ",@Request=" + swiftDao.FilterString(JsonConvert.SerializeObject(request.RequestDetails));
                Log.Information("AirlinesSettlementDetails data sql:{0}", sql);
                var dr = swiftDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt16(dr["ResponseCode"]);
                    response.ResponseDescription = dr["ResponseDescription"].ToString();
                }
            }
            catch(Exception ex)
            {
                Log.Error("AirlinesSettlementDetails execption with message:{0}", ex.Message);
                response.ResponseCode = 101;
                response.ResponseDescription = ex.Message;
            }
            return response;
        }
    }
}
