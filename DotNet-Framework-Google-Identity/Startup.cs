using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DotNet_Framework_Google_Identity;
using DotNet_Framework_Google_Identity.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace DotNet_Framework_Google_Identity
{
    public class Startup
    {
        private JwtSecurityTokenHandler TokenHandler
        {
            get {

                JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
                jwtTokenHandler.InboundClaimTypeMap.Clear();
                jwtTokenHandler.InboundClaimTypeMap.Add("sub", ClaimTypes.Name);

                return jwtTokenHandler;
            }
        }

        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ConfigureAuthentication(app);
        }

        private void ConfigureAuthentication(IAppBuilder app)
        {
            var validAudience = ConfigurationManager.AppSettings["IdentityPlatform:ProjectName"];
            var validIssuer = $"https://securetoken.google.com/{validAudience}";

            var keyResolver = new OpenIdConnectSigningKeyResolver(validIssuer);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions {
                    TokenHandler = TokenHandler,
                    AuthenticationType = OAuthDefaults.AuthenticationType,
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters() {
                        ValidAudience = validAudience,
                        ValidateIssuer = true,
                        ValidIssuer = validIssuer,
                        IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) => keyResolver.GetSigningKey(identifier),
                        SaveSigninToken = true,
                    }
                });
        }
    }
}