using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Insurance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Insurance
{
    public class InsuranceRepo : IInsuranceRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<InsuranceRepo>();
        SwiftDao dao;
        ImePayConnection _imeDao;
        public InsuranceRepo()
        {
            dao = new SwiftDao();
            _imeDao = new ImePayConnection();
        }

        public DbLogResponse InsertIMELifeRenewalPayment(IMELifeRenewalPaymentDetail request)
        {
            DbLogResponse response = new DbLogResponse
            {
                ResponseCode =101,
                ResponseDescription = "Internal Error",
                ReferenceId = ""
            };
            try
            {
                var sql = "EXEC SW_PROC_IMELIFE_RENEWAL_PAYMENT";
                sql += " @Flag=" + _imeDao.FilterString("insertRecord");
                sql += ",@PolicyNumber=" + _imeDao.FilterString(request.PolicyNo);
                sql += ",@CustomerName=" + _imeDao.FilterString(request.CustomerName);
                sql += ",@PremiumAmount=" + _imeDao.FilterString(request.PremiumAmount);
                sql += ",@FineAmount=" + _imeDao.FilterString(request.FineAmount);
                sql += ",@RebateAmount=" + _imeDao.FilterString(request.RebateAmount);
                sql += ",@PayAmount=" + _imeDao.FilterString(request.Amount);
                sql += ",@PlanName=" + _imeDao.FilterString(request.PlanName);
                sql += ",@Term=" + _imeDao.FilterString(request.Term);
                sql += ",@MaturityDate=" + _imeDao.FilterString(request.MaturityDate);
                sql += ",@Token=" + _imeDao.FilterString(request.Token);
                sql += ",@UniqueIDGuid=" + _imeDao.FilterString(request.UniqueIdGuid);
                sql += ",@RequestJson=" + _imeDao.FilterString(request.Json);
                sql += ",@CreatedBy=" + _imeDao.FilterString(request.Msisdn);


                Log.Information("InsertIMELifeRenewalPaymentDbRequest SQL:{0}", sql);
                var dr = _imeDao.ExecuteDataRow(sql);
                if (dr != null)
                {
                    response.ResponseCode = Convert.ToInt32(dr["Code"]);
                    response.ResponseDescription = Convert.ToString(dr["Msg"]);
                    response.ReferenceId = Convert.ToString(dr["Id"]);
                }
                return response;

            }
            catch (Exception ex)
            {
                Log.Error("Exception occur while InsertIMELifeRenewalPaymentDbRequest data with error :{0}", ex.Message.ToString());
            }
            return response;
        }

    }
}
