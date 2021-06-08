using API.Abstract;
using System;
using API.Enum;

namespace API.Implementation
{
    /// <summary>
    /// IRequest wrapper. Includes all needed fields for riot API.
    /// </summary>
    public class RiotRequest : ARequestBase
    {
        /// <summary>
        /// Final request uri - exact value is used for requesting riot api
        /// </summary>
        public override string HttpAddress
        {
            get => $"https://{location.ToTextName()}.api.riotgames.com{this.MethodPath}";
            protected set => throw new Exception("Cannot override RiotRequest address. It is building automatically.");
        }

        private ELocation location;
        
        /// <summary>
        /// Set server location. Used for Uri generating
        /// </summary>
        /// <param name="location"></param>
        public void SetLocation(ELocation location)
        {
            this.location = location;
        }
    }
}