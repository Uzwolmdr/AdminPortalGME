namespace SampleApiService.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }
    }

    public class EmailRequest
    {
        public string UserCode { get; set; }
    }
}
