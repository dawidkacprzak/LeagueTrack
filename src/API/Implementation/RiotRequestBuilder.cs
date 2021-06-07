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
        private RiotRequest request = new RiotRequest();
        ELocation location;
        private string riotApiKey;
        private string methodPath;
        Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        Dictionary<string, string> headerParameters = new Dictionary<string, string>();

        public RiotRequestBuilder(string apiKey, int oneMinRateLimit, int twoMinRateLimit, ELocation location)
        {
            this.riotApiKey = apiKey;
            this.location = location;
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

            request.SetHeaderParams(this.headerParameters);
            request.SetQueryParams(this.queryParameters);
            request.SetLocation(this.location);
            request.SetMethodPath(this.methodPath);
        }

        public IRequest GetRequest()
        {
            return request;
        }

        public void SetMethodPath(string path)
        {
            this.methodPath = path;
        }
    }
}
