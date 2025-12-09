using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Ticketing
{
    public class CableCarPaymentRequestModel :ProductInsertLogRequest
    {
        public List<CableCarBookTicket> TicketDetails { get; set; }
        public string CustomerEmail { get; set; }
        public string SearchId { get; set; }

    }


    public  class CableCarTicketPostedToEcom
    {
        public string UserWalletId { get; set; }
        public List<CableCarBookTicket> TicketDetails { get; set; }
        public string TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string SearchId { get; set; }

    }

    public class CableCarBookTicket
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string TripPrice { get; set; }
        public string PassengerCount { get; set; }
    }


    public class EcomsResponse : ChandragiriApiResponse
    {
        public List<TicketTypes> TicketTypes { get; set; }
    }

    public class ChandragiriApiResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class TicketTypes
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string PassengerTypeDesc { get; set; }
        public string Price { get; set; }
    }

    public class TicketsList
    {
        public string Key { get; set; }
        public List<TicketTypes> Values { get; set; }
        //public  IEnumerable<IGrouping<string, TicketTypes>> Tickets { get; set; }
    }
    public class MyTicketRequest
    {
        public  string Msisdn { get; set; }
        public string Flag { get; set; }
        public string TransactionId { get; set; }

    }

    public class CableCarTicket
    {
        public string PurchasedDate { get; set; }
         public string ExpiryDate { get; set; }
        public bool IsExpired { get; set; }
        public string TicketName { get; set; }
        public string TransactionId { get; set; }
        public object  TicketDetails { get; set; }

}
}
