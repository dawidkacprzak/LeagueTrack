using API.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Implementation
{
    public class RiotRequestBuilder : IRequestBuilder
    {
        private IRequest request = new RiotRequest();
        private string riotApiKey;
        private int oneMinRateLimit;
        private int twoMinRateLimit;
        Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        Dictionary<string, string> headerParameters = new Dictionary<string, string>();

        public RiotRequestBuilder(string apiKey, int oneMinRateLimit, int twoMinRateLimit)
        {
            this.riotApiKey = apiKey;
            this.oneMinRateLimit = oneMinRateLimit;
            this.twoMinRateLimit = twoMinRateLimit;
        }

        public void AddHeaderParam(string key, string value)
        {
            headerParameters.Add(key, value);
        }

        public void AddQueryParam(string key, string value)
        {
            queryParameters.Add(key, value);
        }

        public void BuildRequest()
        {
            Dictionary<string, string> headerParametersWithKey = headerParameters;
            headerParametersWithKey.Add("X-Riot-Token", riotApiKey);

            request.SetHeaderParams(headerParameters);
            request.SetQueryParams(queryParameters);
        }

        public IRequest GetRequest()
        {
            return request;
        }
    }
}
