using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Repository.Config;
using Microsoft.Extensions.Logging;
using Repository.Connection;
using SWCommon;

namespace Repository.Repo
{
    public class RepoService : IRepoService
    {
        private readonly ILogger<RepoService> _logger;
        SwiftDao dao;
        //private string Passphrase = ConfigurationManager.AppSettings["PassPhrase"];
        public RepoService(ILogger<RepoService> logger)
        {
            dao = new SwiftDao();
            _logger = logger;
        }

        public string SaveMoRequests()
        {
            _logger.LogDebug("Log written in Repository Layer");
            //var sql = string.Format("EXEC SW_PROC_SAVE_MOSMS_LOG @Flag='SaveMo', @MobileNo={0},@Telco={1},@Platform={2},@Message={3},@Receiver={4},@SmscId={5}",
            //    dao.FilterString(mobileNo), dao.FilterString(telco), dao.FilterString(platForm), dao.FilterString(message), dao.FilterString(receiver), dao.FilterString(smscid));
            //Log.Debug(string.Format("Save Mo Requests  Query :{0}", sql));

            //var dbResp = dao.ExecuteDataRow(sql);
            //if (dbResp != null)
            //{

            //    Log.Debug("Insert Success");
            //    return Convert.ToString(dbResp["RowId"]);
            //}
            //else
            //{
            //    Log.Debug("Insert Failed");
            //    return "0";
            //}

            return "0";
        }

        public string UpdateMoRequests()
        {
            _logger.LogDebug("Log written in Repository Layer");
            //var sql = string.Format("EXEC SW_PROC_SAVE_MOSMS_LOG @Flag='UpdateMo', @RowId={0},@ClientResp={1}",
            //    dao.FilterString(rowId), dao.FilterString(clientResp));

            //Log.Information(string.Format("Update Mo Requests  Query :{0}", sql));

            //var dbResp = dao.ExecuteDataRow(sql);
            //if (dbResp != null)
            //{

            //    Log.Debug("Update Success");
            //    return Convert.ToString(dbResp["RowId"]);
            //}
            //else
            //{
            //    Log.Debug("Update Failed");
            //    return "0";
            //}
            return "0";
        }
    }
}