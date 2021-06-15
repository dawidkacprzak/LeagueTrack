/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiWrapper;
using ApiWrapper.Abstract.Response;
using ApiWrapper.Enum;
using ApiWrapper.Facade;
using ApiWrapper.Implementation.Request;
using ApiWrapper.Implementation.RequestSender;
using ApiWrapper.Implementation.Response;
using ApiWrapper.Model.AccountV1;
using ApiWrapper.Model.SummonerV4;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Test.Integration.ApiWrapper
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public class EndpointMethods
    {
        private Api instance;
        private RequestSenderDirector requestSenderDirector;
        private RiotRequestSender requestSender;

        [SetUp]
        public void Setup()
        {
            instance = new Api(IntegrationConfiguration.API_KEY, 20, 100, ELocation.EUNE);
            requestSenderDirector = new RequestSenderDirector
            {
                builder = new RiotRequestSenderBuilder(3)
            };
            requestSenderDirector.Construct();

            requestSender = (RiotRequestSender) requestSenderDirector.builder.GetRequestSender();
        }

        [TearDown]
        public void Teardown()
        {
            Thread.Sleep(2000); //Integration tests are trying to request riot api.
            //Just wait some time to do not get TooManyRequests status code
        }

        [Test]
        public void Check_Get_By_Name_Method_Returns_Success_Status_Code()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
            Assert.IsTrue(response.IsSuccess());
        }

        [Test]
        public void Check_Get_By_Name_Method_Do_Not_Throw_Exception_For_All_Servers()
        {
            ELocation[] enums = Enum.GetValues<ELocation>().Skip(1).ToArray(); //Skip no info element
            foreach (var loc in enums)
            {
                Api localApi = GetServerApiInstance(loc);
                RiotRequest request = (RiotRequest) localApi.SummonerV4.ByName("TestName123");
                Assert.DoesNotThrow(() =>
                {
                    RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
                });
            }
        }


        [Test]
        public async Task Check_Get_By_Name_Method_Serializable_Model()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());
            Assert.IsNotNull(summoner.id);
            Assert.IsNotNull(summoner.name);
            Assert.IsNotNull(summoner.puuid);
            Assert.IsNotNull(summoner.accountId);
            Assert.NotZero(summoner.revisionDate);
            Assert.NotZero(summoner.summonerLevel);
            Assert.NotZero(summoner.profileIconId);
        }

        [Test]
        public async Task Check_SummonerV4_Methods_Integration()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());
            RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
            RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
            RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);

            RiotResponse responseByAccount = (RiotResponse) requestSender.GetAsync(requestByAccount).Result;
            RiotResponse responseByPuuid = (RiotResponse) requestSender.GetAsync(requestByPuuid).Result;
            RiotResponse responseBySummoner = (RiotResponse) requestSender.GetAsync(requestBySummoner).Result;

            Summoner summonerByAccount =
                JsonConvert.DeserializeObject<Summoner>(await responseByAccount.GetResponseContentAsync());
            Summoner summonerByPuuid =
                JsonConvert.DeserializeObject<Summoner>(await responseByPuuid.GetResponseContentAsync());
            Summoner summonerBySummoner =
                JsonConvert.DeserializeObject<Summoner>(await responseBySummoner.GetResponseContentAsync());

            Assert.IsTrue(summoner.Equals(summonerByAccount) && summoner.Equals(summonerByPuuid) &&
                          summoner.Equals(summonerBySummoner));
        }

        [Test]
        public void Check_SummonerV4_Does_Not_Throw_Exception_When_Fail_From_Bad_Parameter_Async()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("NICKNAMETHATDOESNTEXISTSGW23f2g");
                RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
                Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());
                RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
                RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
                RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);

                RiotResponse responseByAccount = (RiotResponse) requestSender.GetAsync(requestByAccount).Result;
                RiotResponse responseByPuuid = (RiotResponse) requestSender.GetAsync(requestByPuuid).Result;
                RiotResponse responseBySummoner = (RiotResponse) requestSender.GetAsync(requestBySummoner).Result;
                Assert.IsNull(responseByAccount.GetThrownException());
                Assert.IsNull(responseByPuuid.GetThrownException());
                Assert.IsNull(responseBySummoner.GetThrownException());
                Assert.IsNull(response.GetThrownException());
            });
        }

        [Test]
        public void Check_SummonerV4_Does_Not_Throw_Exception_When_Fail_From_Bad_Parameter_Async_With_Retry_Count()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                RequestSenderDirector localRequestSenderDirector = new RequestSenderDirector
                {
                    builder = new RiotRequestSenderBuilder(10)
                };
                RiotRequestSender localRequestSender =
                    (RiotRequestSender) localRequestSenderDirector.builder.GetRequestSender();

                RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("NICKNAMETHATDOESNTEXISTSGW23f2g");
                RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
                Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());
                RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
                RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
                RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);

                RiotResponse responseByAccount = (RiotResponse) requestSender.GetAsync(requestByAccount).Result;
                RiotResponse responseByPuuid = (RiotResponse) requestSender.GetAsync(requestByPuuid).Result;
                RiotResponse responseBySummoner = (RiotResponse) requestSender.GetAsync(requestBySummoner).Result;
                Assert.IsNull(responseByAccount.GetThrownException());
                Assert.IsNull(responseByPuuid.GetThrownException());
                Assert.IsNull(responseBySummoner.GetThrownException());
                Assert.IsNull(response.GetThrownException());
            });
        }

        [Test]
        public void
            Check_SummonerV4_Does_Not_Throw_Exception_When_Fail_From_Bad_Parameter_Async_With_Retry_Count_Facade()
        {
            RiotFacade facade = new RiotFacade();
            facade.SetLocation(ELocation.EUNE);


            RiotResponse response = facade
                .GetAsync(facade.ApiInstance.SummonerV4.ByName("NICKNAMETHATDOESNTEXISTSGW23f2g")).Result;
            RiotResponse responseByAccount = facade
                .GetAsync(facade.ApiInstance.SummonerV4.ByAccount("NICKNAMETHATDOESNTEXISTSGW23f2g")).Result;
            RiotResponse responseByPuuid = facade
                .GetAsync(facade.ApiInstance.SummonerV4.ByPuuid("NICKNAMETHATDOESNTEXISTSGW23f2g")).Result;
            RiotResponse responseBySummoner = facade
                .GetAsync(facade.ApiInstance.SummonerV4.BySummoner("NICKNAMETHATDOESNTEXISTSGW23f2g")).Result;

            Assert.IsNull(responseByAccount.GetThrownException());
            Assert.IsNull(responseByPuuid.GetThrownException());
            Assert.IsNull(responseBySummoner.GetThrownException());
            Assert.IsNull(response.GetThrownException());
        }

        [Test]
        public void Check_SummonerV4_Does_Not_Throw_Exception_When_Fail_From_Bad_Parameter()
        {
            Assert.DoesNotThrow(() =>
            {
                RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("NICKNAMETHATDOESNTEXISTSGW23f2g");
                RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
                Summoner summoner = JsonConvert.DeserializeObject<Summoner>(response.GetResponseContent());
                RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
                RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
                RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);

                RiotResponse responseByAccount = (RiotResponse) requestSender.GetAsync(requestByAccount).Result;
                RiotResponse responseByPuuid = (RiotResponse) requestSender.GetAsync(requestByPuuid).Result;
                RiotResponse responseBySummoner = (RiotResponse) requestSender.GetAsync(requestBySummoner).Result;
                Assert.IsNull(responseByAccount.GetThrownException());
                Assert.IsNull(responseByPuuid.GetThrownException());
                Assert.IsNull(responseBySummoner.GetThrownException());
                Assert.IsNull(response.GetThrownException());
            });
        }

        [Test]
        public void Check_SummonerV4_Does_Not_Throw_Exception_When_Request_Error()
        {
            Assert.DoesNotThrow(() =>
            {
                RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
                request.SetMethodPath("VWG2g"); //Force to invalid host 
                RiotResponse response = (RiotResponse) requestSender.GetAsync(request).Result;
            });
        }

        [Test]
        public async Task Check_SummonerV4_Does_Not_Throw_Facade()
        {
            RiotFacade facade = new RiotFacade(ELocation.EUNE);
            RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));

            Assert.IsNull(response.GetThrownException());
            Assert.IsNull(response.GetMessage());
        }

        [Test]
        public async Task Check_AccountV1_By_Puuid()
        {
            RiotFacade facade = new RiotFacade(ELocation.EUNE);
            RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));
            
            if(!response.IsSuccess())
                Assert.Fail("Request failed: " + response.GetMessage());
            
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());

            RiotResponse accountV1Response =
                await facade.GetAsync(facade.ApiInstance.AccountV1.AccountsByPuuid(summoner.puuid));

            Assert.IsTrue(accountV1Response.IsSuccess());
        }

        [Test]
        public async Task Check_AccountV1_By_Riot_id()
        {
            RiotFacade facade = new RiotFacade(ELocation.EUNE);
            RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));
            
            if(!response.IsSuccess())
                Assert.Fail("Request failed: " + response.GetMessage());
            
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());

            RiotResponse accountV1Response =
                await facade.GetAsync(facade.ApiInstance.AccountV1.AccountsByPuuid(summoner.puuid));

            Account account = JsonConvert.DeserializeObject<Account>(await accountV1Response.GetResponseContentAsync());
            
            RiotResponse accountV1ResponseRiotId =
                await facade.GetAsync(facade.ApiInstance.AccountV1.AccountsByRiotId(account.puuid,account.gameName)); //Cannot validate it. I do not have any correct parameters. TODO
            Assert.IsTrue(accountV1Response.IsSuccess());
            Assert.IsNotEmpty(account.tagLine);
        }
        
        [Test]
        public async Task Check_AccountV1_Active_Shards_By_Game_ByPuuid()
        {
            RiotFacade facade = new RiotFacade(ELocation.EUNE);
            RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));
            
            if(!response.IsSuccess())
                Assert.Fail("Request failed: " + response.GetMessage());
            
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());

            RiotResponse accountV1Response =
                await facade.GetAsync(facade.ApiInstance.AccountV1.AccountsByPuuid(summoner.puuid));

            Account account = JsonConvert.DeserializeObject<Account>(await accountV1Response.GetResponseContentAsync());
            
            RiotResponse accountV1ResponseRiotId =
                await facade.GetAsync(facade.ApiInstance.AccountV1.ActiveShardsByGameByPuuid(EGame.Valorant,account.puuid)); //Cannot validate it. I do not have any correct parameters. TODO
            Assert.IsTrue(accountV1Response.IsSuccess());
            Assert.IsNotEmpty(account.tagLine);
        }
        
        [Test]
        public async Task Check_AccountV1_Active_Shards_By_Game_ByPuuid_To_ShardAccount_Serializable()
        {
            RiotFacade facade = new RiotFacade(ELocation.EUNE);
            RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));
            
            if(!response.IsSuccess())
                Assert.Fail("Request failed: " + response.GetMessage());
            
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(await response.GetResponseContentAsync());

            RiotResponse accountV1Response =
                await facade.GetAsync(facade.ApiInstance.AccountV1.AccountsByPuuid(summoner.puuid));

            Account account = JsonConvert.DeserializeObject<Account>(await accountV1Response.GetResponseContentAsync());
            
            RiotResponse accountV1ResponseActiveShardsByGameByPuuid =
                await facade.GetAsync(facade.ApiInstance.AccountV1.ActiveShardsByGameByPuuid(EGame.Valorant,account.puuid)); //Cannot validate it. I do not have any correct parameters. TODO
            ShardAccount sa =
                JsonConvert.DeserializeObject<ShardAccount>(await accountV1ResponseActiveShardsByGameByPuuid
                    .GetResponseContentAsync());
            Assert.IsNotNull(sa);
            Assert.IsNotNull(sa.game);
            Assert.IsNotNull(sa.puuid);
            Assert.IsNotNull(sa.activeShard);
            Assert.AreEqual(sa.puuid, account.puuid);
            Assert.IsTrue(accountV1Response.IsSuccess());
            Assert.IsNotEmpty(account.tagLine);
        }
        
        private Api GetServerApiInstance(ELocation location)
        {
            return new Api(IntegrationConfiguration.API_KEY, 20, 100, location);
        }
    }
}