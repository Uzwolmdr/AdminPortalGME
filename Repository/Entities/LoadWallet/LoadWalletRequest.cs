using Repository.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities.LoadWallet
{
    public class LoadWalletRequest : RequestCore
    {
        public string TxnId { get; set; }

        public string RefId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string PIN { get; set; }
        [Required]
        public string BankCode { get; set; }
        public string Msisdn { get; set; }
        [Required]
        public string ReceiverWallet { get; set; }
        public string TransactionId { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        public string Flag { get; set; }
        public string Response { get; set; }
        public string PayLoad { get; set; }

    }
    public class ApiResponse
    {
        public string TransactionId { get; set; }
        public string TpTxnId { get; set; }
        public string Msisdn { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string ReturnedName { get; set; }

        public string ControlNo { get; set; }
        public string EncryptedControlNo { get; set; }


        



        public override string ToString()
        {
            return string.Format("TransactionId:{0},ResponseCode:{1},ResponseDescription:{2}", TransactionId, ResponseCode, ResponseDescription);
        }
    }



    public class LtowPayLoad
    {
        public decimal Amount { get; set; }
        public string BankCode { get; set; }
        public string BankTxnId { get; set; }
        public string ReceiverWallet { get; set; }
    }

    public class PayLoadRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public byte[] PayLoad { get; set; }
    }

    public class SendDomesticRemittanceRequestModel : ProductInsertLogRequest
    {

        public string SenderMobileNo { get; set; }
        public string ReceiverMobileNo { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Purpose { get; set; }
        public string Relationship { get; set; }
        public string ControlNo { get; set; }
    }

    public class Request
    {
        public string Msisdn { get; set; }
        public string MessageBody { get; set; }
        public string Payload { get; set; }
        public string Channel { get; set; }
    }

    public class CancelDomesticRemittance
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string CancelBy { get; set; }
        public string TransactionId { get; set; }
    }

    public class SendDomesticRemittanceReportModel
    {
        public int FilterCount { get; set; }
        public int SNo { get; set; }
        public string SenderMobileNo { get; set; }
        public string ReceiverMobileNo { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Purpose { get; set; } 
        public string Relationship { get; set; }
        public string OrganisationCode { get; set; }
        public string TransactionId { get; set; }
        public string ClientTransactionId { get; set; }
        public string Amount { get; set; }
        public string TxnDate { get; set; }
        public string Status { get; set; }

    }

    public class ExportSendDomesticRemittanceReportModel
    {
        public int SNo { get; set; }
        public string SenderMobileNo { get; set; }
        public string ReceiverMobileNo { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Purpose { get; set; }
        public string Relationship { get; set; }
        public string OrganisationCode { get; set; }
        public string TransactionId { get; set; }
        public string ClientTransactionId { get; set; }
        public string Amount { get; set; }
        public string TxnDate { get; set; }
        public string Status { get; set; }
    }

    public class SendMoneyEditWrapper
    {
        
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public RemitTransaction RemitTransaction { get; set; }
        public List<RemarksDetails> Details { get; set; }


    }
    public class RemitTransaction
    {

        public string Amount { get; set; }
        public string SenderMsisdn { get; set; }
        public string SenderName { get; set; }
        public string ReceiverMsisdn { get; set; }
        public string ReceiverName { get; set; }
        public string ControlNo { get; set; }
        public string TransactionId { get; set; }
    }

    public class EditRemitTransaction
    {
        public string Amount { get; set; }
        public string SenderMsisdn { get; set; }
        public string SenderName { get; set; }
        public string ReceiverMsisdn { get; set; }
        public string ReceiverName { get; set; }
        public string ControlNo { get; set; }
        public string TransactionId { get; set; }
        public string Remarks { get; set; }
    }

    public class RemarksDetails
    {
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }

    public class  SendMoneyEditRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string TransactionId { get; set; }
    }
}


