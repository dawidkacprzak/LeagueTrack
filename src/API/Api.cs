using API.Endpoints;
using API.Enum;


namespace API
{
    public sealed class Api
    {
        private static Api _api;
        public EndpointSummonerV4 SummonerV4;

        public static Api GetInstance(string apiKey, int oneSecRateLimit, int twoMinRateLimit, ELocation location)
        {
            if(_api == null)
            {
                _api = new Api(apiKey, oneSecRateLimit,twoMinRateLimit, location);
            }
            return _api;
        }

        private Api(string apiKey, int oneSecRateLimit, int twoMinRateLimit,ELocation location)
        {
            SummonerV4 = new EndpointSummonerV4(apiKey,oneSecRateLimit,twoMinRateLimit,location);
        }
    }
}
