/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using ApiWrapper.Endpoints;
using ApiWrapper.Enum;
using ApiWrapper.Implementation.RequestLimiter;


namespace ApiWrapper
{
    /// <summary>
    /// Base class for riot api invoke
    /// </summary>
    public sealed class Api
    {
        /// <summary>
        /// SummonerV4 endpoint implementation - https://developer.riotgames.com/apis#summoner-v4
        /// </summary>
        public readonly EndpointSummonerV4 SummonerV4;

        /// <summary>
        /// https://developer.riotgames.com/apis#account-v1 implementation
        /// </summary>
        public readonly AccountV1 AccountV1;

        /// <summary>
        /// Returns current requests location
        /// </summary>
        public ELocation CurrentLocation { get; private set; }

        /// <summary>
        /// Retrieves new api object
        /// </summary>
        /// <param name="apiKey">Prod/Dev riot api key</param>
        /// <param name="oneSecRateLimit">One second api rate limit</param>
        /// <param name="twoMinRateLimit">Two minute api rate limit</param>
        /// <param name="location">Server location</param>
        /// <returns>New Api object</returns>
        public Api(string apiKey, int oneSecRateLimit, int twoMinRateLimit, ELocation location)
        {
            RiotSingletonRequestLimiter.Instance.AbsoluteOneSecRateLimit = oneSecRateLimit;
            RiotSingletonRequestLimiter.Instance.AbsoluteTwoMinRateLimit = twoMinRateLimit;
            CurrentLocation = location;
            SummonerV4 = new EndpointSummonerV4(apiKey, location);
            AccountV1 = new AccountV1(apiKey, location);
        }
    }
}