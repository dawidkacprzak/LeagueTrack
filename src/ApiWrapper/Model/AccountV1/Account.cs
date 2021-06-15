namespace ApiWrapper.Model.AccountV1
{
    /// <summary>
    /// Account model for AccountV1 endpoint
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account puuid
        /// </summary>
        public string puuid { get; set; }

        /// <summary>
        /// Account gameName
        /// </summary>
        public string gameName { get; set; }

        /// <summary>
        /// Account tagLine
        /// </summary>
        public string tagLine { get; set; }
    }
}