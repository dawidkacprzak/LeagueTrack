/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

namespace ApiWrapper.Enum
{
    /// <summary>
    /// Server location list
    /// </summary>
    public enum ELocation
    {
        /// <summary>
        /// No information for corresponding location
        /// </summary>
        NoInfo = 0,

        /// <summary>
        /// Europe nordic east
        /// </summary>
        EUNE = 1,

        /// <summary>
        /// Europe west
        /// </summary>
        EUW = 2,

        /// <summary>
        /// Japan
        /// </summary>
        JP = 3,

        /// <summary>
        /// Korea
        /// </summary>
        KR = 4,

        /// <summary>
        /// Latin America North
        /// </summary>
        LA1 = 5,

        /// <summary>
        /// Latin America South
        /// </summary>
        LA2 = 6,

        /// <summary>
        /// North America
        /// </summary>
        NA = 7,

        /// <summary>
        /// Oceania
        /// </summary>
        OC = 8,

        /// <summary>
        /// Russia
        /// </summary>
        RU = 9,

        /// <summary>
        /// Turkey
        /// </summary>
        TR = 10,

        /// <summary>
        /// America - only for AccountV1 endpoint
        /// </summary>
        AMERICAS = 11,

        /// <summary>
        /// Asia - only for AccountV1 endpoint
        /// </summary>
        ASIA = 12,

        /// <summary>
        /// Europe - only for AccountV1 endpoint
        /// </summary>
        EUROPE = 13
    }

    /// <summary>
    /// Listing of available riot request types
    /// </summary>
    public enum ERiotRequest
    {
        /// <summary>
        /// No information
        /// </summary>
        NoInfo = 0,

        /// <summary>
        /// Endpoint SummonerV4 - ByAccount method
        /// </summary>
        SummonerV4ByAccount = 1,

        /// <summary>
        /// Endpoint SummonerV4 - ByName method
        /// </summary>
        SummonerV4ByName = 2,

        /// <summary>
        /// Endpoint SummonerV4 - ByPuuid method
        /// </summary>
        SummonerV4ByPuuid = 3,

        /// <summary>
        /// Endpoint SummonerV4 - BySummoner method
        /// </summary>
        SummonerV4BySummoner = 4,

        /// <summary>
        /// Endpoint AccountV1 - Accounts by puuid method
        /// </summary>
        AccountV1AccountsByPuuid = 5,

        /// <summary>
        /// Endpoint AccountV1 - Accounts by riot id method
        /// </summary>
        AccountV1AccountsByRiotId = 6,

        /// <summary>
        /// Endpoint AccountV1 - Active shards by game method
        /// </summary>
        AccountV1ActiveShardsByGame = 7
    }
    /// <summary>
    /// Game enum for AccountV1, ActiveShardsByGameByPuuid method
    /// </summary>
    public enum EGame
    {
        /// <summary>
        /// No infomation
        /// </summary>
        NoInfo = 0,
        /// <summary>
        /// Valorant game
        /// </summary>
        Valorant = 1,
        /// <summary>
        /// League of runeterra game
        /// </summary>
        LeagueOfRuneterra = 2
    }
}