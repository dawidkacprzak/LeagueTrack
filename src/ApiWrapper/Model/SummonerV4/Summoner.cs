using System;

namespace ApiWrapper.Model.SummonerV4
{
    /// <summary>
    /// Response object from https://developer.riotgames.com/apis#summoner-v4/ methods
    /// </summary>
    public class Summoner
    {
        /// <summary>
        /// Summoner id - encrypted
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Account id - encrypted
        /// </summary>
        public string accountId { get; set; }

        /// <summary>
        /// Puuid id - encrypted
        /// </summary>
        public string puuid { get; set; }

        /// <summary>
        /// Summoner nickname
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// In game profile icon id
        /// </summary>
        public int profileIconId { get; set; }

        /// <summary>
        /// Last revision date
        /// </summary>
        public long revisionDate { get; set; }

        /// <summary>
        /// Current summoner level
        /// </summary>
        public int summonerLevel { get; set; }


        /// <summary>
        /// Check equality based on class fields
        /// </summary>
        /// <param name="other">Object to compare</param>
        /// <returns></returns>
        public bool Equals(Summoner other)
        {
            return id == other.id && accountId == other.accountId && puuid == other.puuid && name == other.name && profileIconId == other.profileIconId && revisionDate == other.revisionDate && summonerLevel == other.summonerLevel;
        }

        /// <summary>
        /// Generates hashcode based on class fields
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(id, accountId, puuid, name, profileIconId, revisionDate, summonerLevel);
        }
    }
}