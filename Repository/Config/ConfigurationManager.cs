using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace Repository.Config
{
    public class MyConfigurationManager
    {
        public static void SetConfiguration(IConfiguration configuration)
        {
            AppSettings = configuration;
        }
        public static IConfiguration AppSettings { get; private set; }
    }
}
