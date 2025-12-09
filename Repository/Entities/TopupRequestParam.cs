using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class TopupRequestParam
    {
        public string Msisdn { get; set; }
        public string Amount { get; set; }
        public string TopupProduct { get; set; }
        public string TransactionId { get; set; }
        public string TxnId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string Flag { get; set; }
        public int Status { get; set; }
        public string Response { get; set; }
        public string ServiceProvider { get; set; }
        public string PackageCode { get; set; }
        public override string ToString()
        {
            string str = string.Format("(Msisdn:{0},Amount:{1},TopupProduct:{3},TransactionId:{2})",
                Msisdn, Amount, TransactionId, TopupProduct);
            return str;
        }
    }
    public class TopupResponseParam
    {
        public string TransactionId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherId { get; set; }
        public string TopUpAmount { get; set; }
       // public object ResponseData { get; set; }
        public string ProviderName { get; set; }
    }

    public class DataPackageRequest 
    {
        public string Provider { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
    }
}
