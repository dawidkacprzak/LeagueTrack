/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
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