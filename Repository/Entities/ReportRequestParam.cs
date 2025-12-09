using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public class ReportRequestParam
    {
        public string OrganisationCode { get; set; }
        public string Operator { get; set; }
        public string Branch { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Batch { get; set; }
        public string UserName { get; set; }
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public string DisplayLength { get; set; }
        public string DisplayStart { get; set; }
        public string SortDir { get; set; }
        public string SortCol { get; set; }
        public string Search { get; set; }
        public string Flag { get; set; }


    }
    public class ReportResponseParam
    {
        public string OrganisationCode { get; set; }
        public string Branch { get; set; }
        public string UserName { get; set; }
        public string SmsMessage { get; set; }
        public int SmsCount { get; set; }
        public string ReceiverNo { get; set; }

        public string Operator { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string RequestedDate { get; set; }
        public string SendDate { get; set; }

    }
    public class ReportResponseWrapper
    {
        public List<ReportResponseParam> ReportDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }


    public class DropDownResponse
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }

    public class DropDownResponseWrapper
    {
        public List<DropDownResponse> DropDownDetails { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class DropDownRequest
    {
        public string Flag { get; set; }
        public string OrganisationCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Product { get; set; }
    }
}