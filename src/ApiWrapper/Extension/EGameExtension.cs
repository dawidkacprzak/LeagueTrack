using ApiWrapper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.Extension
{
    /// <summary>
    /// Extension class for EGame enum
    /// </summary>
    public static class EGameExtension
    {
        /// <summary>
        /// Name parser for AccountV1 endpoint
        /// </summary>
        /// <param name="game">Game type</param>
        /// <returns>Translated name</returns>
        public static string ToApiName(this EGame game)
        {
            if(game == EGame.Valorant)
            {
                return "val";
            }
            if(game == EGame.LeagueOfRuneterra)
            {
                return "lor";
            }
            throw new NotImplementedException("Game is not supported to translated name");
        }
    }
}
