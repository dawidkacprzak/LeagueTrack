/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Implementation.RequestSender;
using NUnit.Framework;

namespace Test.Unit
{
    public class RequestSenderTests
    {
        [Test]
        public void Riot_Request_Sender_Builder_Creation_By_Builder_Correct_Type()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            Assert.IsTrue(director.builder.GetType() == typeof(RiotRequestSenderBuilder));
        }

        [Test]
        public void Riot_Request_Creation_By_Builder_Correct_Type()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            director.Construct();
            Assert.IsTrue(director.builder.GetRequestSender().GetType() == typeof(RiotRequestSender));
        }

        [Test]
        public void Riot_Request_Sender_Creation_By_Builder_Castable()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            director.Construct();
            Assert.DoesNotThrow(() =>
            {
                RiotRequestSender sender = (RiotRequestSender) director.builder.GetRequestSender();
            });
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(656)]
        [TestCase(2)]
        [TestCase(6)]

        public void Riot_Request_Sender_Creation_By_Builder_Max_Retry_Count_Correct(int retryCount)
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder(retryCount);
            director.Construct();
            Assert.AreEqual(director.builder.GetRequestSender().GetMaxRetryCount(),retryCount);
        }
    }
}