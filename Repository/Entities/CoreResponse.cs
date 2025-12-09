using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class CoreResponse
    {

        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string ExtraId { get; set; }
        public string ClientTxnId { get; set; }
        public string Amount { get; set; }
        public object  ResponseData { get; set; }
        public string ResponseName { get; set; }
        public string ControlNo { get; set; }

        public override string ToString()
        {
            return string.Format("(ResponseCode:{0},ResponseDescription:{1})",
                 ResponseCode, ResponseDescription);
        }
    }

    public class BaseApiResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public object ResponseData { get; set; }
    }

    public class DbLogResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string ReferenceId { get; set; }
    }

    public  class BaseApiBillRequest
    {
        public string CustomerId { get; set; }
        public string User { get; set; }
        public string SmartCardId { get; set; }
        public string CasId { get; set; }
        public string PolicyNo { get; set; }
        public string Serial { get; set; }
    }
    public class ProductInsertLogRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string Product { get; set; }
        public string ProductType { get; set; }
        public string Msisdn { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string TransactionId { get; set; }
        public string ClientTransactionId { get; set; }
        public string ReferenceId { get; set; }
        public int Status { get; set; }
        public string Amount { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Flag { get; set; }
        public string MerchantCode { get; set; }
        public string CreatedBy { get; set; }

    }

    public  class DataPackageResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public ResponseData ResponseData { get; set; }
    }

    public  class ResponseData
    {
        public DataPackage[] Packages { get; set; }

        public string[] PackagesType { get; set; }
    }
    public class DataPackage
    {
        public string Product { get; set; }
        public string PackageCode { get; set; }
        public string PackageType { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public string PackageInfo { get; set; }
        public string Price { get; set; }
        public string Provider { get; set; }
        public string LogoUrl { get; set; }
        public string TimeDuration { get; set; }
    }

    public class WorldLinkBillResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public WorldlinkBillDetailResponseV2 ResponseData { get; set; }
    }

}
