using API;
using API.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unit
{
    [TestFixture]
    public class ObjectCreationTests
    {
        private const string TestKey = "ABCKEY";
        private const int limit1Min = 5;
        private const int limit2Min = 20;

        [Test]
        public void Check_Api_GetInstance_Do_Not_Throw()
        {
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
        }

        [Test]
        public void Check_Api_GetInstance_Multiple_Times_Do_Not_Throw()
        {
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
            Assert.DoesNotThrow(() => Api.GetInstance(TestKey, limit1Min, limit2Min));
        }

        [Test]
        public void Riot_Request_Builder_Header_And_Query_Params_Are_Added()
        {
            RequestDirector director = new RequestDirector();
            director.builder = new RiotRequestBuilder(TestKey, limit1Min, limit2Min);
            Dictionary<string, string> queryParams = new Dictionary<string, string> { { "A", "B" } };
            Dictionary<string, string> headerParams = new Dictionary<string, string> { { "A1", "B1" } };

            director.Construct(queryParams,headerParams);

            RiotRequest r = (RiotRequest)director.builder.GetRequest();
            Assert.True(r.GetHeaderParams().Count == headerParams.Count + 1); //+1 - Riot Request always add Riot Api Key header
            Assert.True(r.GetQueryParams().Count == queryParams.Count);

            Assert.True(r.GetHeaderParams().Except(headerParams).Count() == 1);
            Assert.True(!r.GetQueryParams().Except(queryParams).Any());

        }

        [Test]
        public void API_Endpoints_Correct_Path()
        {
            Api instance = Api.GetInstance(TestKey, limit1Min, limit2Min);
            instance.SummonerV4.GetEndpointPath().Equals("/lol/summoner/v4/");
        }
    }
}
