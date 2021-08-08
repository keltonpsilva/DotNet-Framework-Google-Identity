using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Requests;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Responses;

namespace DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Interfaces
{
    public interface IIdentityPlatformClient
    {
        GoogleAuthResponse SignUpWithEmailAndPassword(SignUpRequest signUpRequest);
        GoogleAuthResponse SignInWithEmailAndPassword(SignInRequest signInRequest);
    }
}