using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class GenericResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public object ResponseData { get; set; }
    }
}
