/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Abstract.RequestLimiter;

namespace ApiWrapper.Implementation.RequestLimiter
{
    public class RequestLimiterDirector
    {
        public IRequestLimiterBuilder RequestLimiterBuilder;

        public void Construct()
        {
            RequestLimiterBuilder.BuildRequestLimiter();
        }


    }
}