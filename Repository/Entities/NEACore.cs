using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class NEAResponseData
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public NEAResponse ResponseData { get; set; }
        public string ReferenceId { get; set; }
    }
    public class NEAResponse
    {
        public string BillNumber { get; set; }
        public string DueDate { get; set; }
        public string Amount { get; set; }
        public string ReserveInfo { get; set; }
        public List<NEAResponsePayment> Payments { get; set; }
    }
    public class NEAResponsePayment
    {
        public string Description { get; set; }
        public string BillDate { get; set; }
        public string BillAmount { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string AmountFact { get; set; }
        public string AmountMask { get; set; }
        public string AmountMin { get; set; }
        public string AmountMax { get; set; }
        public string AmountStep { get; set; }
        public string CodServ { get; set; }
        public string Comission { get; set; }
        public string CommisValue { get; set; }
        public string Destination { get; set; }
        public string Fio { get; set; }
        public string I { get; set; }
        public string Id { get; set; }
        public string J { get; set; }
        public string RequestId { get; set; }
        public string ShowCounter { get; set; }
    }
    public class NEARequest
    {
        public string SCNo { get; set; }
        public int CustomerId { get; set; }
        public int OfficeCode { get; set; }
        public long ProcessId { get; set; }
        public string  Amount { get; set; }
        public long? TransactionId { get; set; }
        public string ReferenceId { get; set; }
        public string Msisdn { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string Flag { get; set; }
        public int Status { get; set; }
        public string Response { get; set; }


    }
    /// <summary>
    ///  class for Initiating  NEA Office Code 
    /// </summary>
    public class NEAOfficeCodeModel
    {
        public int Officecode { get; set; }
        public string OfficeName { get; set; }
    }

    /// <summary>
    /// Class used for Service Request
    /// </summary>
    public class NEAServiceRequest
    {
        public string Msisdn { get; set; }
        public string Amount { get; set; }
        public string ReferenceId { get; set; }
        public string TransactionId { get; set; }
        public string TxnId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        public string Flag { get; set; }
        public string Response { get; set; }

    }
}
