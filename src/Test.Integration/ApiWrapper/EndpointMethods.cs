using System.Collections.Generic;
using ApiWrapper;
using ApiWrapper.Abstract.Response;
using ApiWrapper.Enum;
using ApiWrapper.Implementation.Request;
using ApiWrapper.Implementation.RequestSender;
using ApiWrapper.Implementation.Response;
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
                builder = new RiotRequestSenderBuilder()
            };
            requestSenderDirector.Construct();

            requestSender = (RiotRequestSender)requestSenderDirector.builder.GetRequestSender();
        }

        [Test]
        public void Check_Get_By_Name_Method_Returns_Success_Status_Code()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse)requestSender.GetAsync(request).Result;
            Assert.IsTrue(response.IsSuccess());
        }
        
        [Test]
        public void Check_Get_By_Name_Method_Serializable_Model()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse)requestSender.GetAsync(request).Result;
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(response.GetResponseContent());
            Assert.IsNotNull(summoner.id);
            Assert.IsNotNull(summoner.name);
            Assert.IsNotNull(summoner.puuid);
            Assert.IsNotNull(summoner.accountId);
            Assert.NotZero(summoner.revisionDate);
            Assert.NotZero(summoner.summonerLevel);
            Assert.NotZero(summoner.profileIconId);
        }

        [Test]
        public void Check_SummonerV4_Methods_Integration()
        {
            RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("Rekurencja");
            RiotResponse response = (RiotResponse)requestSender.GetAsync(request).Result;
            Summoner summoner = JsonConvert.DeserializeObject<Summoner>(response.GetResponseContent());
            RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
            RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
            RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);
            
            RiotResponse responseByAccount = (RiotResponse)requestSender.GetAsync(requestByAccount).Result;
            RiotResponse responseByPuuid = (RiotResponse)requestSender.GetAsync(requestByPuuid).Result;
            RiotResponse responseBySummoner = (RiotResponse)requestSender.GetAsync(requestBySummoner).Result;
            
            Summoner summonerByAccount = JsonConvert.DeserializeObject<Summoner>(responseByAccount.GetResponseContent());
            Summoner summonerByPuuid = JsonConvert.DeserializeObject<Summoner>(responseByPuuid.GetResponseContent());
            Summoner summonerBySummoner = JsonConvert.DeserializeObject<Summoner>(responseBySummoner.GetResponseContent());

            Assert.IsTrue(summoner.Equals(summonerByAccount) && summoner.Equals(summonerByPuuid) && summoner.Equals(summonerBySummoner));
        }
        
        [Test]
        public void Check_SummonerV4_Does_Not_Throw_Exception_When_Fail_From_Bad_Parameter()
        {
            Assert.DoesNotThrow(() =>
            {
                RiotRequest request = (RiotRequest) instance.SummonerV4.ByName("NICKNAMETHATDOESNTEXISTSGW23f2g");
                RiotResponse response = (RiotResponse)requestSender.GetAsync(request).Result;
                Summoner summoner = JsonConvert.DeserializeObject<Summoner>(response.GetResponseContent());
                RiotRequest requestByAccount = (RiotRequest) instance.SummonerV4.ByAccount(summoner.accountId);
                RiotRequest requestByPuuid = (RiotRequest) instance.SummonerV4.ByPuuid(summoner.puuid);
                RiotRequest requestBySummoner = (RiotRequest) instance.SummonerV4.BySummoner(summoner.id);
            
                RiotResponse responseByAccount = (RiotResponse)requestSender.GetAsync(requestByAccount).Result;
                RiotResponse responseByPuuid = (RiotResponse)requestSender.GetAsync(requestByPuuid).Result;
                RiotResponse responseBySummoner = (RiotResponse)requestSender.GetAsync(requestBySummoner).Result;
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
                RiotResponse response = (RiotResponse)requestSender.GetAsync(request).Result;
                Assert.IsNotNull(response.GetThrownException());
                Assert.IsNotNull(response.GetMessage());
            });
        }
    }
}