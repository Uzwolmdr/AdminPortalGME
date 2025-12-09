using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class LoginResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Status { get; set; }
        public int LoginFailAttempt { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
