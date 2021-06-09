using System;

namespace Test.Integration
{
    public static class IntegrationConfiguration
    {
        public static readonly string API_KEY = Environment.GetEnvironmentVariable("KEY");
    }
}