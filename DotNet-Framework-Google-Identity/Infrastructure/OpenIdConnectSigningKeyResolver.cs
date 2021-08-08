using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace DotNet_Framework_Google_Identity.Infrastructure
{
    public class OpenIdConnectSigningKeyResolver
    {
        private readonly OpenIdConnectConfiguration _openIdConfig;

        public OpenIdConnectSigningKeyResolver(string authority)
        {
            var cm = new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{authority.TrimEnd('/')}/.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever());
            _openIdConfig = AsyncHelper.RunSync(async () => await cm.GetConfigurationAsync());
        }

        public SecurityKey[] GetSigningKey(string kid)
        {
            // Find the security token which matches the identifier
            return new[] {_openIdConfig.JsonWebKeySet.GetSigningKeys().FirstOrDefault(t => t.KeyId == kid)};
        }
    }

    internal static class AsyncHelper
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static void RunSync(Func<Task> func)
        {
            TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }
    }
}