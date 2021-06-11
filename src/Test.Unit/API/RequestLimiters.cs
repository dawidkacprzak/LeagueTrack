using System;
using System.Threading.Tasks;
using ApiWrapper.Enum;
using ApiWrapper.Implementation.RequestLimiter;
using NUnit.Framework;

namespace Test.Unit.API
{
    public class RequestLimiters
    {
        [SetUp]
        public void Setup()
        {
            RiotSingletonRequestLimiter.Instance.AbsoluteOneSecRateLimit = 20;
            RiotSingletonRequestLimiter.Instance.AbsoluteTwoMinRateLimit = 200;
        }

        [Test]
        public void RiotSingletonRequestLimiter_Throw_When_Configuration_Not_Applied()
        {
            RiotSingletonRequestLimiter.Instance.AbsoluteOneSecRateLimit = 0;
            RiotSingletonRequestLimiter.Instance.AbsoluteTwoMinRateLimit = 0;

            Assert.ThrowsAsync<Exception>(() => RiotSingletonRequestLimiter.Instance.Limit(ERiotRequest.NoInfo));
        }

        [Test]
        public void RiotSingletonRequestLimiter_CheckAbsoluteRateLimit_Gets_OneSecond_If_OneSecLimit_Exceed()
        {
            RiotSingletonRequestLimiter.Instance.AbsoluteOneSecRateLimit = 3;
            RiotSingletonRequestLimiter.Instance.AbsoluteTwoMinRateLimit = 50;

            DateTime now = DateTime.Now;
            
            for (int i = 0; i < 10; i++)
            {
                RiotSingletonRequestLimiter.Instance.AbsoluteRequests.Add(
                    new RiotSingletonRequestLimiter.AbsoluteRequest(now));
            }
            
            int delay = RiotSingletonRequestLimiter.Instance.CheckAbsoluteRateLimit(now);

            Assert.AreEqual(delay, 1000);
        }
        
        [Test]
        public void RiotSingletonRequestLimiter_CheckAbsoluteRateLimit_Gets_Delay_If_2MinLimit_Exceed()
        {
            RiotSingletonRequestLimiter.Instance.AbsoluteOneSecRateLimit = 9999;
            RiotSingletonRequestLimiter.Instance.AbsoluteTwoMinRateLimit = 5;

            DateTime now = DateTime.Now;
            
            for (int i = 0; i < 10; i++)
            {
                RiotSingletonRequestLimiter.Instance.AbsoluteRequests.Add(
                    new RiotSingletonRequestLimiter.AbsoluteRequest(now));
            }
            
            int delay = RiotSingletonRequestLimiter.Instance.CheckAbsoluteRateLimit(now);

            Assert.NotZero(delay);
        }
    }
}