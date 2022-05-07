using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Assessment.API.Common
{
    public class CustomConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public CustomConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string RetrieveAppSettingsValue(string name)
        {
            return _configuration[name];
        }
    }
}