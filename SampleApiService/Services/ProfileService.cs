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
                //Retrieves successful login detail response if the credentials are correct
                var result = await _profileRepository.GetLoginDetailsAsync(email, userCode, password);

                //In case of failure login, the result would be null
                if (result == null)
                {
                    return new GenericResponse
                    {
                        ResponseCode = "101",
                        ResponseDescription = "Invalid usercode and password"
                    };
                }
                //Checking for successful user status
                //102 is sent for inactive accounts
                else if (result.Status != "1")
                {
                    return new GenericResponse
                    {
                        ResponseCode = "102",
                        ResponseDescription = "Account is not in active state"
                    };
                }
                //checking whether the user has exceeded the login fail attempt count
                // 103 response code is sent
                else if (result.LoginFailAttempt > 5)
                {
                    return new GenericResponse
                    {
                        ResponseCode = "103",
                        ResponseDescription = "Login Failed Attempt has exceeded"
                    };
                }
                //Successful login 100 response code is sent
                return new GenericResponse
                {
                    ResponseCode = "100",
                    ResponseDescription = "Successful login attempt"
                };
            }
            catch (Exception)
            {   //In case of exceptions 105 response code is sent
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
                //Checking whether the user entered correct old password first
                //If the old password is incorrect, the change password will not proceed further
                var isOldPasswordCorrect = await _profileRepository.CheckOldPassword(oldPassword, email, userCode);
                if (isOldPasswordCorrect != null && isOldPasswordCorrect.ResponseCode == "100")
                {
                    //if the old password is correct, the process continues with updating the new password for the email and usercode of the customer
                    var changePasswordResponseCount = await _profileRepository.UpdatePasswordAsync(email, userCode, newPassword);
                    if (changePasswordResponseCount == "100")
                    {

                        // 100 response code suggests password change has been successful
                        return new GenericResponse
                        {
                            ResponseCode = "100",
                            ResponseDescription = "Password Change Successful"
                        };
                    }
                    // 101 response code suggests password change has failed
                    return new GenericResponse
                    {
                        ResponseCode = "101",
                        ResponseDescription = "Change Password failed"
                    };
                }
                // 102 response code suggests old password was incorrect
                return new GenericResponse
                {
                    ResponseCode = "102",
                    ResponseDescription = "Old Password doesn''t match"
                };
            }
            catch (Exception)
            {
                // 103 response code suggests technical exception occured
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
