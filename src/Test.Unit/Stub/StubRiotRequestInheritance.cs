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