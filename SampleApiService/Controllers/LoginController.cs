using Elastic.CommonSchema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApiService.Models;
using SampleApiService.Services;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace SampleApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<LoginController> _logger;

        public LoginController( ILogger<LoginController> logger, IProfileService profileService)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpPost]
        [Route("LoginProcess")]
        //This API is used to login from the main home page using Email, usercode and Pasword
        public async Task<IActionResult> LoginProcess([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return Unauthorized("Unauthorized");
            }

            var result = await _profileService.GetLoginDetailsAsync(request.Email, request.UserCode, request.Password);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetVersion")]
        //This API is used to print the dll version on the footer of each login, profile, dashboard and change password page
        //Version is present in the bottom left corner
        public async Task<IActionResult> GetVersion()
        {
            _logger.LogDebug("Sample API EndPoint Called to Get Version of Product.");

            var version = FileVersionInfo
                .GetVersionInfo(Assembly.GetExecutingAssembly().Location)
                .ProductVersion;

            return Ok(version);
        }

        [HttpPost]
        [Route("GetEmail")]
        //This API is used to retrieve the email of the user after successful login and print it in the profile page of the user
        public async Task<IActionResult> GetEmail([FromBody] EmailRequest request)
        {
            _logger.LogDebug("Sample API EndPoint Called to GetEmail.");
            //Retrives email through usercode
            var email = await _profileService.GetEmailByUserCodeAsync(request.UserCode);

            return Ok(email);
        }

        [HttpPost]
        [Route("ChangePassword")]
        //This API is used to change the password of the logged in user using old password from the profile dropdown
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            _logger.LogDebug("ChangePassword request arrived.");
            var result = await _profileService.ChangePassword(request.Email, request.UserCode, request.OldPassword, request.NewPassword);
            return Ok(result);
        }
    }
}
