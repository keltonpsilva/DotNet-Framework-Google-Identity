using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Interfaces;
using Ninject.Modules;
using RestSharp;

namespace DotNet_Framework_Google_Identity.Infrastructure
{
    public class DependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IRestClient>().To<RestClient>();

            Bind<IIdentityPlatformClient>().To<IdentityPlatformClient>();
            Bind<IIdentityPlatformConfigurations>().To<IdentityPlatformConfigurations>();
        }
    }
}