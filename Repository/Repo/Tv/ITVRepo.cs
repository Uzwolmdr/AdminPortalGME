using Repository.Entities;
using Repository.Entities.Tv;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Tv
{
   public interface ITvRepo
    {
       CoreResponse InsertSimTVRequest(SimTVPaymentRequest request);
        DbLogResponse InsertMeroTvData(MeroTvPaymentRequestModel model);
        DbLogResponse InsertWebServerData(WebServerPaymentRequestModel model);
        DbLogResponse InsertParbhuTvData(ParbhuTvPaymentRequestModel model);
        DbLogResponse InsertNetTvData(NetTvPaymentModel request);
    }
}
