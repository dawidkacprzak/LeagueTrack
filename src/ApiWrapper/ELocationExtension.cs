using System;
using ApiWrapper.Enum;

namespace ApiWrapper
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
        /// <param name="location">Elocation enum</param>
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
                default: throw new NotImplementedException();
            }
        }
    }
}