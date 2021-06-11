﻿/*
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
        TR = 10
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
        SummonerV4BySummoner = 4
    }
}