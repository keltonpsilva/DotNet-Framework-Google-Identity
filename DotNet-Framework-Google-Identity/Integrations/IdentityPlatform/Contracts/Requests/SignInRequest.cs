namespace DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Requests
{
    public class SignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool ReturnSecureToken { get; } = true;
    }
}