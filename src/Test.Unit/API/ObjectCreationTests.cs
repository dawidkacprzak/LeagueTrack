using API;
using API.Enum;
using API.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using API.Abstract;
using Test.Unit.Stub;

namespace Test.Unit
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public class ObjectCreationTests
    {
        private const string TestKey = "ABCKEY";
        private const int limit1Min = 5;
        private const int limit2Min = 20;

        [Test]
        public void Check_Api_GetInstance_Do_Not_Throw()
        {
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
        }

        [Test]
        public void Check_Api_GetInstance_Multiple_Times_Do_Not_Throw()
        {
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
            Assert.DoesNotThrow(() => new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE));
        }

        [Test]
        public void Riot_Request_Builder_Header_And_Query_Params_Are_Added()
        {
            RequestDirector director = new RequestDirector();
            director.builder = new RiotRequestBuilder(TestKey, ELocation.EUNE);
            Dictionary<string, string> queryParams = new Dictionary<string, string> {{"A", "B"}};
            Dictionary<string, string> headerParams = new Dictionary<string, string> {{"A1", "B1"}};

            director.Construct("/", queryParams, headerParams);

            RiotRequest r = (RiotRequest) director.builder.GetRequest();
            Assert.True(r.GetHeaderParams().Count ==
                        headerParams.Count + 1); //+1 - Riot Request always add Riot Api Key header
            Assert.True(r.GetQueryParams().Count == queryParams.Count);

            Assert.True(r.GetHeaderParams().Except(headerParams).Count() == 1);
            Assert.True(!r.GetQueryParams().Except(queryParams).Any());
        }

        [Test]
        public void API_Endpoints_Correct_Path()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            Assert.AreEqual(instance.SummonerV4.GetEndpointPath(), "/lol/summoner/v4");
        }

        [Test]
        public void ELocation_To_Name_Correct()
        {
            Assert.AreEqual(ELocation.EUNE.ToTextName(), "eun1");
            Assert.AreEqual(ELocation.NoInfo.ToTextName(), "");
            Assert.Throws<NotImplementedException>(() => ((ELocation) (-3)).ToTextName());
            int eLocationCount = Enum.GetNames(typeof(ELocation)).Length;
            Assert.AreEqual(eLocationCount, 2); //Just to don't forget about insert and CHECK next locations. 
        }

        [Test]
        public void API_EEndpoint_HttpAddress_Correct()
        {
            ARequestBase aRequestBase = new StubARequestBase();
            aRequestBase.SetHttpAddress("http://google.pl");
            Assert.AreEqual(aRequestBase.HttpAddress, "http://google.pl");
        }

        [Test]
        public void API_Riot_HttpAddress_Set_Throws()
        {
            StubRiotRequestInheritance request = null;
            Assert.Throws<Exception>(() => request = new StubRiotRequestInheritance());
            Assert.IsNull(request);
        }

        [Test]
        public void API_Endpoints_Contains_Key()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest requestByName = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount("Rekurencja");
            RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid("Rekurencja");
            RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner("Rekurencja");


            Assert.IsTrue(requestByName.GetHeaderParams().ContainsKey("X-Riot-Token"));
            Assert.IsTrue(requestByAccount.GetHeaderParams().ContainsKey("X-Riot-Token"));
            Assert.IsTrue(requestByPuuid.GetHeaderParams().ContainsKey("X-Riot-Token"));
            Assert.IsTrue(requestBySummoner.GetHeaderParams().ContainsKey("X-Riot-Token"));
        }

        [Test]
        public void API_Endpoint_SummonerV4_ByName_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            Assert.AreEqual(request.HttpAddress,
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Rekurencja");
            Assert.IsNotNull(request.HttpAddress);
        }

        [Test]
        public void API_Endpoint_SummonerV4_ByAccount_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByAccount("encryptedAccountId");
            Assert.AreEqual(request.HttpAddress,
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-account/encryptedAccountId");
            Assert.IsNotNull(request.HttpAddress);
        }

        [Test]
        public void API_Endpoint_SummonerV4_ByPuuid_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByPuuid("encryptedPUUID");
            Assert.AreEqual(request.HttpAddress,
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/encryptedPUUID");
            Assert.IsNotNull(request.HttpAddress);
        }

        [Test]
        public void API_Endpoint_SummonerV4_BySummoner_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.BySummoner("encryptedSummonerId");
            Assert.AreEqual(request.HttpAddress,
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/encryptedSummonerId");
            Assert.IsNotNull(request.HttpAddress);
        }
    }
}