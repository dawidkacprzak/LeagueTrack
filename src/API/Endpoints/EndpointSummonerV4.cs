using API.Abstract;
using API.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public class EndpointSummonerV4 : IEndpoint
    {
        private RequestDirector requestDirector = new RequestDirector();
        private string apiKey;
        private int oneMinRequestRate;
        private int twoMinRequestRate;
        private ELocation location;

        public EndpointSummonerV4(string apiKey, int oneMinRequestRate, int twoMinRequestRate, ELocation location)
        {
            this.apiKey = apiKey;
            this.oneMinRequestRate = oneMinRequestRate;
            this.twoMinRequestRate = twoMinRequestRate;
            this.location = location;
        }

        public string GetEndpointPath()
        {
            return "/lol/summoner/v4";
        }

        
        /// <summary>
        /// Create request for fetching summoner info by summoner name
        /// </summary>
        /// <param name="summonerName">Summoner name</param>
        public IRequest ByName(string summonerName)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneMinRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/by-name/{summonerName}");
            return requestDirector.builder.GetRequest();
        }

        /// <summary>
        /// Create request for fetching summoner info by account id
        /// </summary>
        /// <param name="encryptedAccountId">Encrypted account id</param>
        public IRequest ByAccount(string encryptedAccountId)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneMinRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/by-account/{encryptedAccountId}");
            return requestDirector.builder.GetRequest();
        }

    }
}
