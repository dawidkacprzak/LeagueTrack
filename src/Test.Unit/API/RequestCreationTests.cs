/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using ApiWrapper;
using ApiWrapper.Enum;
using ApiWrapper.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ApiWrapper.Abstract;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;
using ApiWrapper.Facade;
using ApiWrapper.Implementation.Request;
using ApiWrapper.Implementation.RequestSender;
using Test.Unit.Stub;

namespace Test.Unit
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public class RequestCreationTests
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
            Assert.AreEqual(ELocation.NoInfo.ToTextName(), "");
            Assert.AreEqual(ELocation.EUNE.ToTextName(), "eun1");
            Assert.AreEqual(ELocation.JP.ToTextName(), "jp1");
            Assert.AreEqual(ELocation.OC.ToTextName(), "oc1");
            Assert.AreEqual(ELocation.KR.ToTextName(), "kr");
            Assert.AreEqual(ELocation.LA1.ToTextName(), "la1");
            Assert.AreEqual(ELocation.LA2.ToTextName(), "la2");
            Assert.AreEqual(ELocation.NA.ToTextName(), "na1");
            Assert.AreEqual(ELocation.RU.ToTextName(), "ru");
            Assert.AreEqual(ELocation.TR.ToTextName(), "tr1");
            Assert.AreEqual(ELocation.EUW.ToTextName(), "euw1");
            Assert.Throws<NotImplementedException>(() => ((ELocation) (-3)).ToTextName());
            int eLocationCount = Enum.GetNames(typeof(ELocation)).Length;
            Assert.AreEqual(eLocationCount, 11); //Just to don't forget about insert and CHECK next locations. 
        }

        [Test]
        public void API_EEndpoint_HttpAddress_Correct()
        {
            ARequestBase aRequestBase = new StubARequestBase();
            aRequestBase.SetHttpAddress("http://google.pl");
            Assert.AreEqual(aRequestBase.GetHttpAddress(), "http://google.pl");
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
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Rekurencja");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void API_Endpoint_SummonerV4_ByAccount_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByAccount("encryptedAccountId");
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-account/encryptedAccountId");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void API_Endpoint_SummonerV4_ByPuuid_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByPuuid("encryptedPUUID");
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/encryptedPUUID");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void API_Endpoint_SummonerV4_BySummoner_CorrectHttpPath()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.BySummoner("encryptedSummonerId");
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/encryptedSummonerId");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void API_Endpoint_Query_Parameter_Single_Built_Properly()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.BySummoner("encryptedSummonerId");
            request.SetQueryParams(new Dictionary<string, string>() {{"Key", "Value"}});
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/encryptedSummonerId?Key=Value");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void API_Endpoint_Query_Parameter_Multiple_Built_Properly()
        {
            Api instance = new Api(TestKey, limit1Min, limit2Min, ELocation.EUNE);
            RiotRequest request = (RiotRequest) instance.SummonerV4.BySummoner("encryptedSummonerId");
            request.SetQueryParams(new Dictionary<string, string>() {{"Key", "Value"}, {"Key2", "Value2"}});
            Assert.AreEqual(request.GetHttpAddress(),
                "https://eun1.api.riotgames.com/lol/summoner/v4/summoners/encryptedSummonerId?Key=Value&Key2=Value2");
            Assert.IsNotNull(request.GetHttpAddress());
        }

        [Test]
        public void Riot_Face_Location_Change_Works()
        {
            RiotFacade facade = new RiotFacade();
            ELocation currentLocation = facade.GetLocation();
            ELocation deepLocation = facade.ApiInstance.CurrentLocation;

            facade.SetLocation(ELocation.LA1);

            ELocation changedCurrentLocation = facade.GetLocation();
            ELocation changedDeepLocation = facade.ApiInstance.CurrentLocation;

            Assert.IsTrue(changedDeepLocation == changedCurrentLocation && currentLocation == deepLocation &&
                          changedDeepLocation != currentLocation);
        }
    }
}