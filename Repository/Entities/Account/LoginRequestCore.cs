using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Account
{
    public class RequestCore
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string DisplayLength { get; set; }
        public string DisplayStart { get; set; }
        public string SortDir { get; set; }
        public string SortCol { get; set; }
        public string Search { get; set; }
        
    }
    public class LoginResponseCore
    {
        public string Username { get; set; }
        public string Organisation { get; set; }
        public string Password { get; set; }  

        public string FullName { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public bool isValid { get; set; }
        public int  UserType { get; set; }

    }

    
    public class ReconcileRequestCore: RequestCore
    {
        public string TransactionId { get; set; }
        public string Flag { get; set; }
        public string Service { get; set; }
        public string Provider { get; set; }
    }
    public class ReconcileResponseCore
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class DashboardRequestCore : RequestCore
    {
        public string Month { get; set; }
    }

    public class ReconcileMultipleRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganisationCode { get; set; }
        public string JsonData { get; set; }
        public string Flag { get; set; }
    }
}
