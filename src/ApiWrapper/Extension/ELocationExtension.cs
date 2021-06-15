/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;
using System.Linq;
using ApiWrapper.Enum;

namespace ApiWrapper.Endpoints
{
    /// <summary>
    /// Extenstion class for location enum
    /// </summary>
    public static class ELocationExtension
    {
        /// <summary>
        /// Convert ELocation to string value correct with riot api uri builder
        /// https://---eun1---.api.riotgames.com/lol/....
        /// </summary>
        /// <param name="location">ELocation enum</param>
        /// <returns>String value correct with riot api uri builder</returns>
        /// <exception cref="NotImplementedException">Occurs when there is no implementation for corresponding enum value</exception>
        public static string ToTextName(this ELocation location)
        {
            switch (location)
            {
                case ELocation.NoInfo:
                    return "";
                case ELocation.EUNE:
                    return "eun1";
                case ELocation.EUW:
                    return "euw1";
                case ELocation.JP:
                    return "jp1";
                case ELocation.OC:
                    return "oc1";
                case ELocation.KR:
                    return "kr";
                case ELocation.LA1:
                    return "la1";
                case ELocation.LA2:
                    return "la2";
                case ELocation.NA:
                    return "na1";
                case ELocation.RU:
                    return "ru";
                case ELocation.TR:
                    return "tr1";
                case ELocation.ASIA:
                    return "asia";
                case ELocation.AMERICAS:
                    return "americas";
                case ELocation.EUROPE:
                    return "europe";
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Maps location to corresponding AccountV1 enum
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Mapped location</returns>
        public static ELocation MapToAccountV1Location(this ELocation location)
        {
            ELocation[] europeLocations = {ELocation.TR,ELocation.EUNE,ELocation.EUW, ELocation.EUROPE};
            ELocation[] americaLocations = {ELocation.LA1, ELocation.LA2,ELocation.NA, ELocation.AMERICAS};
            ELocation[] asiaLocations = {ELocation.JP, ELocation.OC, ELocation.KR, ELocation.RU, ELocation.ASIA};

            if (europeLocations.Contains(location))
                return ELocation.EUROPE;
            if (americaLocations.Contains(location))
                return ELocation.AMERICAS;
            if (asiaLocations.Contains(location))
                return ELocation.ASIA;
            throw new NotImplementedException("This location is not supported by MapToAccountV1Location function");
        }
    }
    
}