using API.Abstract;
using API.Endpoints;
using System;

namespace API
{
    public sealed class Api
    {
        private static Api _api;
        private string apiKey;
        private int oneMinRateLimit;
        private int twoMinRateLimit;
        public IEndpoint SummonerV4;

        public static Api GetInstance(string apiKey, int oneMinRateLimit, int twoMinRateLimit)
        {
            if(_api == null)
            {
                _api = new Api(apiKey, oneMinRateLimit,twoMinRateLimit);
            }
            return _api;
        }

        private Api(string apiKey, int oneMinRateLimit, int twoMinRateLimit)
        {
            this.apiKey = apiKey;
            this.oneMinRateLimit = oneMinRateLimit;
            this.twoMinRateLimit = twoMinRateLimit;
            SummonerV4 = new EndpointSummonerV4();
        }
    }
}
