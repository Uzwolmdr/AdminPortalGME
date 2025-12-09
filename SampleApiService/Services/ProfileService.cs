using Repository.DapperRepo;
using Repository.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace SampleApiService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly IProfileRepository _profileRepository;

        public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
        }
        public async Task<GenericResponse> GetLoginDetailsAsync(string email, string userCode, string password)
        {
            try
            {
                var result = await _profileRepository.GetLoginDetailsAsync(email, userCode, password);

                if (result == null)
                {
                    return new GenericResponse
                    {
                        ResponseCode = "101",
                        ResponseDescription = "Invalid usercode and password"
                    };
                }

                else if (result.Status != "1")
                {
                    return new GenericResponse
                    {
                        ResponseCode = "102",
                        ResponseDescription = "Account is not in active state"
                    };
                }

                else if (result.LoginFailAttempt > 5)
                {
                    return new GenericResponse
                    {
                        ResponseCode = "103",
                        ResponseDescription = "Login Failed Attempt has exceeded"
                    };
                }

                return new GenericResponse
                {
                    ResponseCode = "100",
                    ResponseDescription = "Successful login attempt"
                };
            }
            catch (Exception)
            {
                return new GenericResponse
                {
                    ResponseCode = "105",
                    ResponseDescription = "Internal Error Occured"
                };
            }
        }

        public async Task<GenericResponse> ChangePassword(string email, string userCode, string oldPassword, string newPassword)
        {
            try
            {
                var isOldPasswordCorrect = await _profileRepository.CheckOldPassword(oldPassword, email, userCode);
                if (isOldPasswordCorrect != null && isOldPasswordCorrect.ResponseCode == "100")
                {
                    //newPassword = Md5SqlCompatible(newPassword);
                    var changePasswordResponseCount = await _profileRepository.UpdatePasswordAsync(email, userCode, newPassword);
                    if (changePasswordResponseCount == "100")
                    {
                        return new GenericResponse
                        {
                            ResponseCode = "100",
                            ResponseDescription = "Password Change Successful"
                        };
                    }
                    return new GenericResponse
                    {
                        ResponseCode = "101",
                        ResponseDescription = "Change Password failed"
                    };
                }
                return new GenericResponse
                {
                    ResponseCode = "102",
                    ResponseDescription = "Old Password doesn''t match"
                };
            }
            catch (Exception)
            {
                return new GenericResponse
                {
                    ResponseCode = "103",
                    ResponseDescription = "Internal Error Occured"
                };
            }
        }

        public Task<string?> GetEmailByUserCodeAsync(string userCode)
        {
            return _profileRepository.GetEmailByUserCodeAsync(userCode);
        }
    }
}
