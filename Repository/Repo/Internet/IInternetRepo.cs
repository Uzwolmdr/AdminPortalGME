using Repository.Entities;
using Repository.Entities.Internet;
using Repository.Entities.Tv;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Internet
{
    public interface IInternetRepo
    {
        DbLogResponse InsertClassicTechPaymentImePayDb(ClassicTechPaymentRequest request);
        DbLogResponse InsertSubisuPaymentImePayDb(SubisuPaymentRequest request);
        DbLogResponse InsertVianetPaymentRequest(VianetPaymentRequest request);
        DbLogResponse InsertParbhuNetData(ParbhuNetPaymentRequestModel model);
        DbLogResponse InsertWebserverInternetData(WebServerPaymentRequestModel request);
        DbLogResponse InsertPalsNetRequestData(PalsNetPaymentRequestModel request);
        DbLogResponse InsertTechMindsData(TechMindsPaymentRequestModel request);
        DbLogResponse InsertLoopInternetData(LoopInternetPaymentRequestModel request);
        DbLogResponse InsertFirstLinkData(FirstLinkPaymentRequestModel request);
        DbLogResponse InsertArrowNetData(ArrowNetPaymentModel request);
        DbLogResponse InsertDishHomeFiberNetRequestData(DishHomeFiberNetPaymentModel request);
        DbLogResponse InsertReliantInternetRequestData(ReliantInternetPaymentModel request);
        DbLogResponse InsertSubisuPaymentImePayDbV2(SubisuPaymentRequestV2 request);
    }
}
