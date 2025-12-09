using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Water
{
    public interface IWaterRepo
    {
        List<WaterCounterList> GetCommunityWaterCounter();
        CoreResponse InsertDiyaloKhanepaniRequest(CommunityWaterPaymentDbRequestLog req);
        CoreResponse InsertHeartsunKhanepaniRequest(CommunityWaterPaymentDbRequestLog request);
        CoreResponse InsertHetaudaKhanepaniRequest(CommunityWaterPaymentDbRequestLog request);
        CoreResponse InsertH2OKhanepaniRequest(CommunityWaterPaymentDbRequestLog request);
        CoreResponse InsertNepalKhanePaniRequest(CommunityWaterPaymentDbRequestLog logRequest);
    }
}
