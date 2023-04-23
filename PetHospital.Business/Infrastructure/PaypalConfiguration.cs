using PayPal.Api;

namespace PetHospital.Business.Infrastructure
{
    public static class PaypalConfiguration
    {
        static PaypalConfiguration()
        {

        }

        public static Dictionary<string, string> GetConfig(string mode)
        {
            return new Dictionary<string, string>()
            {
                { "mode", mode }
            };
        }

        private static string GetAccessToken(string clientId, string clientSecret, string mode)
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, new Dictionary<string, string>()
            {
                { "mode", mode }
            }).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetApiContext(string clientId, string clientSecret, string mode)
        {
            APIContext apiContext = new APIContext(GetAccessToken(clientId, clientSecret, mode));
            apiContext.Config = GetConfig(mode);
            return apiContext;
        }
    }
}
