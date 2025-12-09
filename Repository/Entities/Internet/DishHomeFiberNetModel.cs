using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Internet
{
    public class DishHomeFiberNetPaymentModel : ProductInsertLogRequest
    {
        public string PackagePlan { get; set; }
        public string Json { get; set; }

    }

    public class DishHomeFiberNetAccountResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public DishHomeFiberNetData ResponseData { get; set; }
    }
    public class DishHomeFiberNetData
    {
        public string Cid { get; set; }
        public string Name { get; set; }
        public List<DishHomeFiberInternet> Internet { get; set; }
        public string Amt { get; set; }
        public string Request { get; set; }
    }
    public class DishHomeFiberInternet
    {
        public string User { get; set; }
        public string Plan { get; set; }
        public string Price { get; set; }
    }
}
