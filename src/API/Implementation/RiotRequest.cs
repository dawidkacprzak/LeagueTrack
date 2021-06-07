using API.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Implementation
{
    public class RiotRequest : ARequestBase
    {
        public override string HttpAddress
        {
            get => $"https://{location.ToTextName()}.api.riotgames.com{this.MethodPath}";
            protected set => throw new Exception("Cannot override RiotRequest address. It is building automatically.");
        }

        private ELocation location;


        public void SetLocation(ELocation location)
        {
            this.location = location;
        }
    }
}