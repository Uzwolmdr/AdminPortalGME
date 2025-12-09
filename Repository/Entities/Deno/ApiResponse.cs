using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Deno
{
    public class ApiResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public object ResponseData { get; set; }
    }
   
}
