using API.Endpoints;
using API.Enum;


namespace API
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
        /// Retrieves new api object
        /// </summary>
        /// <param name="apiKey">Prod/Dev riot api key</param>
        /// <param name="oneSecRateLimit">One second api rate limit</param>
        /// <param name="twoMinRateLimit">Two minute api rate limit</param>
        /// <param name="location">Server location</param>
        /// <returns>New Api object</returns>
        public Api(string apiKey, int oneSecRateLimit, int twoMinRateLimit,ELocation location)
        {
            SummonerV4 = new EndpointSummonerV4(apiKey,oneSecRateLimit,twoMinRateLimit,location);
        }
    }
}
