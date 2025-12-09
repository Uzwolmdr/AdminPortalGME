using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DapperRepo
{
    public interface IProfileRepository
    {
        Task<LoginResponse> GetLoginDetailsAsync(string email, string userCode, string password);
        Task<GenericResponse> CheckOldPassword(string oldPassword, string email, string userCode);
        Task<string> UpdatePasswordAsync(string email, string userCode, string password);
        Task<string?> GetEmailByUserCodeAsync(string userCode);
    }
}
