using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class ClassicTechResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string result { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string username { get; set; }
        public string package { get; set; }
        public List<Packagedetail> packagedetail { get; set; }
        public Userinfo userinfo { get; set; }
        public string token { get; set; }
    }
    public class Userinfo
    {
        public string name { get; set; }
        public string address { get; set; }
    }
    public class Packagedetail
    {
        public string duration { get; set; }
        public float amount { get; set; }
    }

    public class ClassicTechPaymentRequest:ProductInsertLogRequest
    {
        public string Json { get; set; }
        public string Month { get; set; }
        public string Package { get; set; }
        public string Token { get; set; }
    }
}
