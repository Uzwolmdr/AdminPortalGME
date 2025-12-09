using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class DishHomeCoreRequest
    {
        public string Msisdn { get; set; }
        public string Product { get; set; }
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
    public class DishomePaymentResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PackageName { get; set; }
    }
    public class DishomePayment
    {
        public string Msisdn { get; set; }
        public string CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public int Status { get; set; }
        public string Flag { get; set; }
        public string Response { get; set; }
    }
    public class DishomeResponse
    {
        public string responseCodeField { get; set; }
        public string responseDescriptionField { get; set; }
        public string tokenField { get; set; }
        public string rTN1Field { get; set; }
        public string rTN2Field { get; set; }
        public string rTN3Field { get; set; }
        public string customerNameField { get; set; }
        public string languageField { get; set; }
        public string packageField { get; set; }
        public string balanceField { get; set; }
        public string expiryDateField { get; set; }
        public string regionField { get; set; }
        public string zoneField { get; set; }
        public string districtField { get; set; }
        public string municipalityorVDCField { get; set; }
        public string customerTypeField { get; set; }
        public string customerStatusField { get; set; }
        public string customerIdField { get; set; }
        public string isRTNField { get; set; }
        public string isDealerField { get; set; }
        public string moilebNumberField { get; set; }
        public string quantityField { get; set; }
        public string boxId { get; set; }
    }

   
    public class PackageAndListPrice
    {
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class DishomeV2Response
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Token { get; set; }
        public string CustomerName { get; set; }
        public string Package { get; set; }
        public List<PackageAndListPrice> PackageAndListPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string District { get; set; }
        public string MunicipalityorVDC { get; set; }
        public string CustomerType { get; set; }
        public string CustomerStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerClass { get; set; }
        public string IspCustomerID { get; set; }
        public double? YearlyPrice { get; set; }
    }


}
