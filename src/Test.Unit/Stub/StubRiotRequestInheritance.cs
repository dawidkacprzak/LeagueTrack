using API.Implementation;

namespace Test.Unit.Stub
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public sealed class StubRiotRequestInheritance : RiotRequest
    {
        public StubRiotRequestInheritance()
        {
            HttpAddress = "throws exception";
        }
    }
}