using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.PurseMgt
{
   public class PurseMgtRequestModel
    {
        public string OrganisationCode { get; set; }
        public string DrCr      { get; set; }
        public string TransactionId { get; set; }
        public string ThirdPartyTxnId { get; set; }
        public string Amount { get; set; }
        public string TxnType { get; set; }
        public string Flag { get; set; }
        public string UserName { get; set; }
        public string Remarks { get;  set; }
        public string Reference { get; set; }
        public string ProductId { get; set; }
        public string ServiceProvider { get; set; }
    }
    public class AddWithdrawRequestModel
    {
        public string OrganisationCode { get; set; }
        public string DrCr { get; set; }
        public string Amount { get; set; }
        public string UserName { get; set; }
    }

    public class PurseDrCrResponse
    {
        

        public string RowId { get; set; }
        public string OrganisationCode { get; set; }
        public string Amount { get; set; }
        public string DrCr { get; set; }
        public string TransactionId { get; set; }
        public string CreatedBy { get;  set; }
        public string CreatedDate { get;  set; }
        public string Reference { get; set; }
    }
    public class PurseDrCrResponseWrapper
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public List<PurseDrCrResponse> PendingList { get; set; }
    }
    public class DBResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class SoaDetails
    {
        public string Amount { get; set; }
        public string DrCr { get; set; }
        public string TransactionId { get; set; }
        public string TpTransactionId { get; set; }
        public string Balanace { get; set; }
        public string TransactionDate { get;  set; }
        public string TxnType { get; set; }
        public string ProductId { get; set; }
    }

    public class SoaWrapperResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string OpeningBalance { get; set; }
        public string ClosingBalance { get; set; }
        public List<SoaDetails> SoaDetails { get; set; }

    }
    public class SoaRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrganisationCode { get; set; }
    }
    public class PurseBalanceResponse : DBResponse
    {
        public string CurrentBalance { get; set; }
    }

    public class PurseBalanceRequest
    {
        public string OrganisationCode { get; set; }
    }

    public class AllOrgPurseBalance
    {
        public string OrganisationCode { get; set; }
        public string OrganisationName { get; set; }
        public string CurrentBalance { get; set; }
        public bool Status { get; set; }
    }
    public class AllOrgPurseBlncWrapper
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public List<AllOrgPurseBalance> Data { get; set; }
    }
}
