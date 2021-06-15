namespace ApiWrapper.Model.AccountV1
{
    /// <summary>
    /// Account model from ActiveShardsByGameByPuuid method
    /// </summary>
    public class ShardAccount
    {
        /// <summary>
        /// account puuid
        /// </summary>
        public string puuid { get; set; }
        /// <summary>
        /// account gmae
        /// </summary>
        public string game { get; set; }
        /// <summary>
        /// account active shard
        /// </summary>
        public string activeShard { get; set; }
    }
}