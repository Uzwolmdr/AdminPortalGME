using Repository.Models;

namespace SampleApiService.Services
{
    public interface IProfileService
    {
        Task<GenericResponse> GetLoginDetailsAsync(string email, string userCode, string password);
        Task<GenericResponse> ChangePassword(string email, string userCode, string oldPassword, string newPassword);
        Task<string?> GetEmailByUserCodeAsync(string userCode);
    }
}
