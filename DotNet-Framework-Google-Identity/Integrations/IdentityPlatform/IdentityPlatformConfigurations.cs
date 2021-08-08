using System.Configuration;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Interfaces;

namespace DotNet_Framework_Google_Identity.Integrations.IdentityPlatform
{
    public class IdentityPlatformConfigurations : IIdentityPlatformConfigurations
    {
        public string BaseUrl => ConfigurationManager.AppSettings["IdentityPlatform:BaseUrl"];
        public string APIKey => ConfigurationManager.AppSettings["IdentityPlatform:APIKey"];
    }
}