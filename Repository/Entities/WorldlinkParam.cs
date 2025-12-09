using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{



    public class WorldlinkRequest
    {
        [Required]
        public string CustomerId { get; set; }
    }

    public class WorldlinkPaymentRequest : ProductInsertLogRequest
    {
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageType { get; set; }
        public string Json { get; set; }
        public string RefundedAmount { get; set; }
        public string PackageAmount { get; set; }
        public string InvoiceAmount { get; set; }
        public string AccountDisabled { get; set; }
    }


    public class WorldlinkPaymentResponseV2
    {
        public WorllinkPaymentInvoiceV2 invoice { get; set; }
        public WorldlinkPaymentRefundabledepositV2[] refundableDeposits { get; set; }
    }

    public class WorllinkPaymentInvoiceV2
    {
        public string invoiceNumber { get; set; }
        public string taxableAmount { get; set; }
        public string vatAmount { get; set; }
    }

    public class WorldlinkPaymentRefundabledepositV2
    {
        public string particular { get; set; }
        public string serialNumber { get; set; }
        public string receiptNumber { get; set; }
        public string amount { get; set; }
    }

    public class WorldlinkBillDetailResponseV2
    {
        public string fullName { get; set; }
        public string username { get; set; }
        public bool accountDisabled { get; set; }
        public string subscribedPackageId { get; set; }
        public string subscribedPackageName { get; set; }
        public string subscribedPackageType { get; set; }
        public string branch { get; set; }
        public string daysRemaining { get; set; }
        public string amount { get; set; }
        public WorldlinkAmountdetail[] amountDetail { get; set; }
        public WorldlinkRenewoption renewOption { get; set; }
        public WordlinkPackageoption[] packageOptions { get; set; }
        public string refundableAmount { get; set; }
        public WorldlinkRefundableamountdetailV2[] refundableAmountDetail { get; set; }
        public string message { get; set; }
        public string WorldlinkPaymentType { get; set; }
    }
    public class WorldlinkAmountdetail
    {
        public string particular { get; set; }
        public string amount { get; set; }
    }

    public class WorldlinkRenewoption
    {
        public string dueAmount { get; set; }
        public string remarks { get; set; }
        public WordlinkPackageoption[] packageOptions { get; set; }
    }

    public class WordlinkPackageoption
    {
        public string packageId { get; set; }
        public string packageName { get; set; }
        public string packageRate { get; set; }
        public string packageLabel { get; set; }
    }

    public class WorldlinkRefundableamountdetailV2
    {
        public string particular { get; set; }
        public string serialNumber { get; set; }
        public string amount { get; set; }
    }

    public class WorldLinkPaymentResponse
    {

        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

        public string ExtraId { get; set; }
        public string ClientTxnId { get; set; }

        public WorldlinkPaymentResponseV2 ResponseData { get; set; }

    }
}
