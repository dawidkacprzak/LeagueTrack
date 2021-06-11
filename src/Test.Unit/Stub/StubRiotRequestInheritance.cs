/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Implementation.Request;

namespace Test.Unit.Stub
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public sealed class StubRiotRequestInheritance : RiotRequest
    {
        public StubRiotRequestInheritance()
        {
            httpAddress = "throws exception";
        }
    }
}