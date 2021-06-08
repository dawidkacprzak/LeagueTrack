using API.Abstract;
using API.Implementation;
using API.Enum;

namespace API.Endpoints
{
    public class EndpointSummonerV4 : IEndpoint
    {
        private readonly RequestDirector requestDirector = new RequestDirector();
        private readonly string apiKey;
        private readonly int oneSecRequestRate;
        private readonly int twoMinRequestRate;
        private readonly  ELocation location;

        public EndpointSummonerV4(string apiKey, int oneSecRequestRate, int twoMinRequestRate, ELocation location)
        {
            this.apiKey = apiKey;
            this.oneSecRequestRate = oneSecRequestRate;
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
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneSecRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/by-name/{summonerName}");
            return requestDirector.builder.GetRequest();
        }

        /// <summary>
        /// Create request for fetching summoner info by account id
        /// </summary>
        /// <param name="encryptedAccountId">Encrypted account id</param>
        public IRequest ByAccount(string encryptedAccountId)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneSecRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/by-account/{encryptedAccountId}");
            return requestDirector.builder.GetRequest();
        }
        /// <summary>
        /// Create request for fetching summoner info by account id
        /// </summary>
        /// <param name="encryptedPuuid">Encrypted account id</param>
        public IRequest ByPuuid(string encryptedPuuid)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneSecRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/by-puuid/{encryptedPuuid}");
            return requestDirector.builder.GetRequest();
        }
        /// <summary>
        /// Create request for fetching summoner info by account id
        /// </summary>
        /// <param name="encryptedSummonerId">Encrypted account id</param>
        public IRequest BySummoner(string encryptedSummonerId)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, oneSecRequestRate, twoMinRequestRate, location);
            requestDirector.Construct($"{GetEndpointPath()}/summoners/{encryptedSummonerId}");
            return requestDirector.builder.GetRequest();
        }

    }
}
