using System;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Requests;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Contracts.Responses;
using DotNet_Framework_Google_Identity.Integrations.IdentityPlatform.Interfaces;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace DotNet_Framework_Google_Identity.Integrations.IdentityPlatform
{
    public class IdentityPlatformClient : IIdentityPlatformClient
    {
        private readonly IRestClient _restClient;
        private readonly IIdentityPlatformConfigurations _config;

        public IdentityPlatformClient(
            IRestClient restClient,
            IIdentityPlatformConfigurations config)
        {
            _restClient = restClient;
            _config = config;
            _restClient.BaseUrl = new Uri($"{_config.BaseUrl}");
            _restClient.UseNewtonsoftJson();
        }

        public GoogleAuthResponse SignUpWithEmailAndPassword(SignUpRequest signUpRequest)
        {
            var request = new RestRequest(Method.POST) {
                Resource = $"/v1/accounts:signUp",
                RequestFormat = DataFormat.Json,
            };

            request.AddParameter("key", _config.APIKey, ParameterType.QueryString);
            request.AddJsonBody(signUpRequest);

            return ExecuteRequest<GoogleAuthResponse>(_config.BaseUrl, request);

        }

        public GoogleAuthResponse SignInWithEmailAndPassword(SignInRequest signInRequest)
        {
            var request = new RestRequest(Method.POST) {
                Resource = $"/v1/accounts:signInWithPassword",
                RequestFormat = DataFormat.Json,
            };

            request.AddParameter("key", _config.APIKey, ParameterType.QueryString);
            request.AddJsonBody(signInRequest);

            return ExecuteRequest<GoogleAuthResponse>(_config.BaseUrl, request);
        }

        private T ExecuteRequest<T>(string baseUrl, IRestRequest request)
        {
            _restClient.BaseUrl = new Uri(baseUrl);

            IRestResponse<T> response = _restClient.Execute<T>(request);

            return HandlerResponse<T>(response);
        }

        private T HandlerResponse<T>(IRestResponse<T> response)
        {
            var baseErrorMessage = $"Failed execute {response.Request.Method} to '{_restClient?.BaseUrl?.OriginalString}{response.Request.Resource}'";

            if (response.ErrorException != null || !string.IsNullOrEmpty(response.ErrorMessage))
                throw new ApplicationException($"{baseErrorMessage}: {response.ErrorMessage}", response.ErrorException);

            if (!response.IsSuccessful)
                throw new ApplicationException($"{baseErrorMessage}: {response.Content}");

            if (response.Data == null)
                throw new ApplicationException(baseErrorMessage);

            return response.Data;
        }
    }
}