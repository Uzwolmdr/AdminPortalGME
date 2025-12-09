namespace SampleApiService
{
    public class Request
    {
    }

    public class Response
    {
        public int code { get; set; }
        public string msg { get; set; }
    }

    public class VersionResponse
    {
        public int code { get; set; }
        public string version { get; set; }
    }

}
