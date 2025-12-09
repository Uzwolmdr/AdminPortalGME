using Repository.Entities;
using Repository.Entities.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Ticketing
{
    public interface ITicketingRepo
    {
        DbLogResponse InsertManakamanaData(CableCarPaymentRequestModel request);
        DbLogResponse InsertChandragiriData(CableCarPaymentRequestModel request);
        AirlineConfig GetSectorAndAirlines();
        string SaveFlightSearchResponse(string searchParam, string flightData);
        string GetParentAirlinesCode(string code);
        JsonResponse InsertAirlinesTicketingData(IssueTicketModel issueTicketModel);
        object GetInvoiceDetails(string tranId, string flag);
        BaseApiResponse GetMyCableCarTicketByTranId(MyTicketRequest request);
        BaseApiResponse GetMyCableCarTickets(MyTicketRequest request);
        DbLogResponse AirlinesSettlementDetails(AirlinesSettlementRequest request);
    }
}
