/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;

namespace Test.Integration
{
    public static class IntegrationConfiguration
    {
        public static readonly string API_KEY = Environment.GetEnvironmentVariable("KEY");
    }
}