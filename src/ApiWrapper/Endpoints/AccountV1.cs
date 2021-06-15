/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Abstract.Endpoint;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Enum;
using ApiWrapper.Extension;
using ApiWrapper.Implementation.Request;

namespace ApiWrapper.Endpoints
{
    /// <summary>
    /// https://developer.riotgames.com/apis#account-v1 implementation
    /// </summary>
    public class AccountV1 : IEndpoint
    {
        private readonly RequestDirector requestDirector = new RequestDirector();
        private readonly string apiKey;
        private readonly ELocation location;
        
        
        /// <summary>
        /// Constructs new endpoint object
        /// </summary>
        /// <param name="apiKey">Prod/Dev riot api key</param>
        /// <param name="location">Location</param>
        public AccountV1(string apiKey, ELocation location)
        {
            this.apiKey = apiKey;
            this.location = location.MapToAccountV1Location();
        }
        /// <summary>
        /// Endpoint path, part after base uri
        /// </summary>
        /// <returns></returns>
        public string GetEndpointPath()
        {
            return "/riot/account/v1";
        }
        
        /// <summary>
        /// Create request for account info by puuid
        /// </summary>
        /// <param name="puuid">Account puuid</param>
        public IRequest AccountsByPuuid(string puuid)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, location, ERiotRequest.SummonerV4ByName);
            requestDirector.Construct($"{GetEndpointPath()}/accounts/by-puuid/{puuid}");
            return requestDirector.builder.GetRequest();
        }
        
        /// <summary>
        /// Create request for account info by riot id
        /// </summary>
        /// <param name="gameName">Account puuid</param>
        /// <param name="riotId">Account puuid</param>

        public IRequest AccountsByRiotId(string gameName,string riotId)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, location, ERiotRequest.SummonerV4ByName);
            requestDirector.Construct($"{GetEndpointPath()}/accounts/by-riot-id/{gameName}/{riotId}");
            return requestDirector.builder.GetRequest();
        }
        
        /// <summary>
        /// Create request for shard getting
        /// </summary>
        /// <param name="game">Account game</param>
        /// <param name="puuid">Account puuid</param>

        public IRequest ActiveShardsByGameByPuuid(EGame game,string puuid)
        {
            requestDirector.builder = new RiotRequestBuilder(apiKey, location, ERiotRequest.SummonerV4ByName);
            requestDirector.Construct($"{GetEndpointPath()}/active-shards/by-game/{game.ToApiName()}/by-puuid/{puuid}");
            return requestDirector.builder.GetRequest();
        }
    }
}