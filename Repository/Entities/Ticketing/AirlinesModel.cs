using System.Collections.Generic;

namespace Repository.Entities.Ticketing
{

    public class AirlineConfig
    {
        public List<AirlineCarrier> Carriers { get; set; }
        public List<AirlineSector> Sectors { get; set; }
    }

    public class AirlineCarrier
    {
        public string Code
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string WalletCode
        {
            get; set;
        }
    }

    public class AirlineSector
    {
        public string Code
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
    }

    public class TicketingResponseModel
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public FlightAvailability ResponseData { get; set; }
        public string Id { get; set; }
    }
    public class ReservationResponseModel
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public ReservationDetail ResponseData { get; set; }
    }

    public class FlightAvailability
    {
        public Flights[] Outbound { get; set; }
        public Flights[] Inbound { get; set; }
    }
    public class Flights
    {
        public string Airline { get; set; }
        //public string AirlineLogo { get; set; }
        public string FlightDate { get; set; }
        public string FlightNo { get; set; }
        public string Departure { get; set; }
        public string DepartureTime { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string AircraftType { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public string FlightId { get; set; }
        public string FlightClassCode { get; set; }
        public string Currency { get; set; }
        public decimal AdultFare { get; set; }
        public decimal ChildFare { get; set; }
        public decimal InfantFare { get; set; }
        public string ResFare { get; set; }
        public decimal FuelSurcharge { get; set; }
        public decimal Tax { get; set; }
        public string Refundable { get; set; }
        public string FreeBaggage { get; set; }
        public string AgencyCommission { get; set; }
        public string ChildCommission { get; set; }
        public string MerchantCode { get; set; }
        //public decimal Cashback { get; set; }
        public decimal GrandTotal { get; set; }
    }
    public class TicketingEncriptionModel
    {
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public decimal AdultFare { get; set; }
        public decimal ChildFare { get; set; }
        public decimal InfantFare { get; set; }
        public string ResFare { get; set; }
        public decimal FuelSurcharge { get; set; }
        public string Nationality { get; set; }
        public decimal Tax { get; set; }
        public string FlighId { get; set; }
        public string Airline { get; set; }
        public string Cashback { get; set; }
        public string MerchantCode { get; set; }

        public decimal GrandTotal { get; set; }

    }

    public class ReservationDetail
    {
        public ReservationDetailPNRDetail[] PNRDetail { get; set; }
    }

    public class ReservationDetailPNRDetail
    {
        public string AirlineID { get; set; }
        public string FlightId { get; set; }
        public string PNRNO { get; set; }
        public string ReservationStatus { get; set; }
        public string TTLDate { get; set; }
        public string TTLTime { get; set; }
    }


    public class Itinerary
    {
        public ItineraryPassenger[] Passenger { get; set; }
        public string Error { get; set; }
    }

    public class ItineraryPassenger
    {
        public string Airline { get; set; }
        public string PnrNo { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PaxNo { get; set; }
        public string PaxType { get; set; }
        public string Nationality { get; set; }
        public string PaxId { get; set; }
        public string IssueFrom { get; set; }
        public string AgencyName { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string Departure { get; set; }
        public string FlightTime { get; set; }
        public string TicketNo { get; set; }
        public string BarCodeValue { get; set; }
        public string BarcodeImage { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string Sector { get; set; }
        public string ClassCode { get; set; }
        public string Currency { get; set; }
        public string Fare { get; set; }
        public string Surcharge { get; set; }
        public string TaxCurrency { get; set; }
        public string Tax { get; set; }
        public string CommissionAmount { get; set; }
        public string Refundable { get; set; }
        public string ReportingTime { get; set; }
        public string FreeBaggage { get; set; }

        public string TicketStatus { get; set; }
    }


    public class AirTicketDetail
    {
        public string TransactionId { get; set; }
        public string AuxiliaryTransactionId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CurrentBalance { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string HtmlResponse { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }
        public ItineraryPassenger[] Passenger { get; set; }
    }

    public class FlightSearchModel
    {
        public string Msisdn { get; set; }
        public string SectorFrom { get; set; }
        public string SectorTo { get; set; }
        public string FlightDate { get; set; }
        public string ReturnDate { get; set; }
        public string TripType { get; set; }
        public string Nationality { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
    }
    public class ReservationModel
    {
        public string Msisdn { get; set; }
        public string FlightId { get; set; }
        public string ReturnFlightId { get; set; }
        public string AirLine { get; set; }
        public string ReturnAirline { get; set; }
    }

    public class FlightReserveRequestModel
    {
        public string FromSector { get; set; }
        public string ToSector { get; set; }
        public string ClassType { get; set; }
        public string AirlineCode { get; set; }
        public string AgencyCommission { get; set; }
        public string ChildCommission { get; set; }
        public string Currency { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string FlightType { get; set; }
    }

    public class FlightReserveData
    {
        public string FlightId { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string FlightClassCode { get; set; }
        public string Airline { get; set; }
        public string AgencyCommission { get; set; }
        public string ChildCommission { get; set; }
        public string Currency { get; set; }
        public string Adult { get; set; }
        public string Child { get; set; }
        public string FlightType { get; set; }
        public string MerchantCode { get; set; }
        public string FlightDate { get; set; }
        public decimal GrandTotal { get; set; }

        public string FlightNo { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string AircraftType { get; set; }
        public int Infant { get; set; }
        public decimal AdultFare { get; set; }
        public decimal ChildFare { get; set; }
        public decimal InfantFare { get; set; }
        public string ResFare { get; set; }
        public decimal FuelSurcharge { get; set; }
        public decimal Tax { get; set; }
        public string Refundable { get; set; }
        public string FreeBaggage { get; set; }
        public decimal Cashback { get; set; }
        public string AirlineLogo { get; set; }
    }

    public class IssueTicketModel : ProductInsertLogRequest
    {
        public string FlightId { get; set; }
        public string ReturnFlightId { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonMobileNo { get; set; }
        public decimal InboundGrandTotal { get; set; }
        public decimal OutboundGrandTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string InboundDate { get; set; }
        public string OutboundDate { get; set; }
        public string ReserveFlightRequest { get; set; }
        public string OutboundMerchantCode { get; set; }
        public string InboundMerchantCode { get; set; }
        public List<PassangerModel> PassangerList { get; set; }

        //for ReserveFlightRequest
        public FlightReserveData FlightData { get; set; }
        public FlightReserveData ReturnFlightData { get; set; }

    }

    public class ReserveFlightRequest
    {
        //public string OutboundCommission { get; set; }
       // public string InboundCommission { get; set; }
        public string InboundData { get; set; }
        public string OutboundData { get; set; }
    }

    public class PassangerModel
    {
        public string PaxType { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string PaxRemarks { get; set; }
    }
    public class TicketSearchModel
    {
        public string Msisdn { get; set; }
        public string User { get; set; }
        public string TransactionId { get; set; }
    }

    public class AirTicketCommissionModel
    {
        public string ToSector { get; set; }
        public string FromSector { get; set; }
        public string ClassType { get; set; }
        public string Airlines { get; set; }
        public string AgencyCommission { get; set; }
        public string ChildCommission { get; set; }
        public string Currency { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string FlightType { get; set; }
        public string SourceMsisdn { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class TransactionResultResponse
    {
        public string TransactionId { get; set; }
        public int StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public string ResponseDescription { get; set; }
        public string Charge { get; set; }

        public string Commission { get; set; }

        public string ReferenceId { get; set; }
        public int ResponseCode { get; set; }
    }
    public class AirlineIssueResponse : TransactionResultResponse
    {
        public string TicketUrl { get; set; }
    }
    public class JsonResponse : BaseResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FlightIssueId { get; set; }
       
    }
    public class BaseResponse
    {
        public string TransactionId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class  AirlinesSettlementDetails
    {
        public string FastConnectTxnId { get; set; }
        public string BankTxnId { get; set; }
        public string CreatedDate { get; set; }
        public string AirlinesCode { get; set; }
        public string FromSector { get; set; }
        public string ToSector { get; set; }
        public string TicketClass { get; set; }
        public string Pnr { get; set; }
        public string TicketNumber { get; set; }
        public string Amount { get; set; }
        public string AirlinesCommission { get; set; }
    }

    public class AirlinesSettlementRequest
    {
        public string OrganisationCode { get; set; }
        public List<AirlinesSettlementDetails> RequestDetails { get; set; }
    }
}
