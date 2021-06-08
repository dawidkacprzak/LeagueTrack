using System;
using API.Enum;


namespace API
{
    public static class ELocationExtension
    {
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
