using ApiWrapper.Model.SummonerV4;
using NUnit.Framework;

namespace Test.Unit.API
{
    [TestFixture]
    public class Model
    {
        [Test]
        public void Check_Summoner_HashCode_Correct()
        {
            Summoner s = new Summoner();
            Summoner s1 = new Summoner()
            {
                id = "test"
            };
            
            Assert.AreNotEqual(s.GetHashCode(),s1.GetHashCode());
            Assert.NotZero(s.GetHashCode());
        }
    }
}