using Microsoft.Extensions.Logging;
using Repository.Connection;
using Repository.Repo;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Models;

namespace Repository.DapperRepo
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbConnection _db;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(IDbConnection db, ILogger<ProfileRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<LoginResponse> GetLoginDetailsAsync(string email, string userCode, string password)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@UserCode", userCode, DbType.String);
                parameters.Add("@Password", password, DbType.String);
                parameters.Add("@Flag", "LoginDetails", DbType.String);

                // Dapper will automatically open the connection if it's closed
                var result = await _db.QueryFirstOrDefaultAsync<LoginResponse>(
                    "SW_PROC_PROFILE",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling SW_PROC_PROFILE with LoginDetails. Email: {Email}, UserCode: {UserCode}", email, userCode);
                throw;
            }
        }

        public async Task<GenericResponse> CheckOldPassword(string oldPassword, string email, string userCode)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@UserCode", userCode, DbType.String);
                parameters.Add("@Password", oldPassword, DbType.String);
                parameters.Add("@Flag", "CheckOldPassword", DbType.String);

                // Dapper will automatically open the connection if it's closed
                var result = await _db.QueryFirstOrDefaultAsync<GenericResponse>(
                    "SW_PROC_PROFILE",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling SW_PROC_PROFILE with CheckOldPassword. Email: {Email}, UserCode: {UserCode}", email, userCode);
                throw;
            }
        }

        public async Task<string> UpdatePasswordAsync(string email, string userCode, string newPassword)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@UserCode", userCode);
                parameters.Add("@Password", newPassword);
                parameters.Add("@Flag", "UpdatePassword");

                var result = await _db.QueryFirstOrDefaultAsync<string>(
                    "SW_PROC_PROFILE",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;  // "100" or "101"
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating password for email {Email}", email);
                throw;
            }
        }

        public async Task<string?> GetEmailByUserCodeAsync(string userCode)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserCode", userCode);

                var result = await _db.QueryFirstOrDefaultAsync<string>(
                    "SELECT Email FROM dbo.UserProfile WHERE UserCode = @UserCode",
                    parameters,
                    commandType: CommandType.Text
                );

                return result;   // returns Email or null
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching email for UserCode {UserCode}", userCode);
                throw;
            }
        }


    }
}
