using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Tv
{
    public class WebServerPaymentRequestModel : ProductInsertLogRequest
    {
        public string PaymentCode { get; set; }
        public string Json { get; set; }
        public string Plan { get; set; }
        public string SmartCardId { get; set; }
        public string User { get; set; }
        public string CheckoutId { get; set; }
        public string BouquetId { get; set; }

    }


    public class DigitalTvAccountResponse
    {
        public string status_code { get; set; }
        public string status_txt { get; set; }
        public DigitalTvAccountData data { get; set; }
        public List<AvailablePlan> available_plans { get; set; }
        public string request { get; set; }
        public string status { get; set; }
    }

    public class DigitalTvAccountData
    {
        public string cid { get; set; }
        public string name { get; set; }
        public string stb { get; set; }
        public List<CurrentBouque> current_bouque { get; set; }
    }

    public class AvailablePlan
    {
        public string code { get; set; }
        public string bouque { get; set; }
        public string days { get; set; }
        public double price { get; set; }
    }

    public class CurrentBouque
    {
        public string bouque { get; set; }
        public string activation_date { get; set; }
        public string deactivation_date { get; set; }
    }

    public class InternetAccountResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string status_code { get; set; }
        public string status_txt { get; set; }
        public InternetData data { get; set; }
        public List<AvailablePlan> available_plans { get; set; }
        public string request { get; set; }
        public string status { get; set; }
    }

    public class InternetData
    {
        public string cid { get; set; }
        public string name { get; set; }
        public string user { get; set; }
        public string current_bouque { get; set; }
        public string current_bouque_expire_date { get; set; }
        public string new_bouque_start_date { get; set; }
    }


    public class WebsurferV2RequestModel
    {
        public string Username { get; set; }
        public string Msisdn { get; set; }
    }

    public class WebsurferV2SubscriptionRequestModel
    {
        public string CustomerId { get; set; }
        public string Service { get; set; }
        public string RequestId { get; set; }
    }

    public class WebsurferV2TransactionStatusRequest
    {
        public string CustomerId { get; set; }
        public string CheckoutId { get; set; }
    }

    public class WebsurferV2BaseResponse
    {
        public bool success { get; set; }
        public string code { get; set; }

        public string message { get; set; }
    }
    public class WebsurferV2Response : WebsurferV2BaseResponse
    {

        public WebsurferProfile body { get; set; }
        public string service { get; set; }
        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

    }


    public class WebsurferProfile : WebsurferV2TransactionStatusRequest
    {
        public CustomerInformation customer { get; set; }

        public WebsurferInternetProfile internet { get; set; }
    }

    public class WebsurferV2TopUpRequest : WebsurferV2SubscriptionRequestModel
    {
        public string CheckoutId { get; set; }
        public string bouquetId { get; set; }
        public string Amount { get; set; }

    }

    public class WebsurferV2TransactionResponse : WebsurferV2BaseResponse
    {
        public WebsurferTransactionDetail body { get; set; }
        public string status_code { get; set; }
    }


    public class WebsurferTransactionDetail
    {
        public WebsurferV2TransactionResult result { get; set; }
    }

    public class WebsurferV2TransactionResult
    {
        public string OrderId { get; set; }
        public string TxnId { get; set; }
        public string Cuid { get; set; }
        public string ReqId { get; set; }
        public string BouquetId { get; set; }
        public string Amt { get; set; }

        public string TxnDate { get; set; }
        public string Status { get; set; }
        public string Service { get; set; }
    }

    public class CustomerInformation
    {
        public string cuid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string status { get; set; }
    }

    public class WebsurferInternetProfile
    {
        public ConnectionInfo connection { get; set; }

        public List<Subscription> subscriptions { get; set; }
        public List<Bouquet> bouquets { get; set; }
    }

    public class ConnectionInfo
    {
        public string Username { get; set; }
        public string Status { get; set; }
    }

    public class Subscription
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class Bouquet
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Amt { get; set; }
    }

    public class WebsurferV2PaymentResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

    }

    public class WebsurferV2PaymentRequest
    {
        public string TransactionId { get; set; }
        public string ReferenceId { get; set; }
    }

    public class WebsurferV2Log
    {
        public string status { get; set; }
        public string Websurfer_Transaction_Id { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceId { get; set; }
        public string Json { get; set; }
        public string paymentJson { get; set; }
        public string verifyJson { get; set; }

    }
    public struct WebsurferV2LogResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class WebsurferV2PaymentDetailRequest
    {
        public string bouquetId { get; set; }
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public string RefId { get; set; }
        public string Service { get; set; }
        public string Amount { get; set; }
        public string CheckoutId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class WebsurferV2DbRequest
    {
        public string bouquetId { get; set; }
        public string CustomerId { get; set; }
        public string Amount { get; set; }
        public string Plan { get; set; }
        public string Username { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }
        public string Service { get; set; }
        public string Msisdn { get; set; }
        public string Json { get; set; }
        public string CheckoutId { get; set; }

        public string CustomerMsisdn { get; set; }


    }



}
