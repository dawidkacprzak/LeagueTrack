using ApiWrapper.Enum;

namespace ApiWrapper.Facade
{
    /// <summary>
    /// Configuration for riot facade
    /// </summary>
    public static class FacadeGlobalConfig
    {
        public static string Api_key = string.Empty;
        public static int OneSecondLimit = 10;
        public static int TwoMinutesLimit = 100;
        public static ELocation Location = ELocation.EUNE;
        public static int MaxRetryCount = 3;
    }
}