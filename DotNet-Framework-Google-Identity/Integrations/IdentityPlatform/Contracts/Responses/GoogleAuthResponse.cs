using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Responses
{
    public class GoogleAuthResponse
    {
        [JsonProperty(PropertyName = "localId")]
        public string LocalId { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [DeserializeAs(Name = "idToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}