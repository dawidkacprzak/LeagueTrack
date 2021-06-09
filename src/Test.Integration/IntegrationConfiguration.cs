using System;
namespace Test.Integration
{
    public static class IntegrationConfiguration
    {
        public const string API_KEY = Environment.GetEnvironmentVariable("KEY");
    }
}
