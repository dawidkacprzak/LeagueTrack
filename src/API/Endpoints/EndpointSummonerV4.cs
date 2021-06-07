using API.Abstract;
using API.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public class EndpointSummonerV4 : IEndpoint
    {
        public string GetEndpointPath()
        {
            return "/lol/summoner/v4/";
        }

        public RiotRequest ByName(string summonerName)
        {
            return new RiotRequest();
        }
        
    }
}
