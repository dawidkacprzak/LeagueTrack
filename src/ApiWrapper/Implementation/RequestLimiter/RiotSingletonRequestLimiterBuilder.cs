/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Abstract.RequestLimiter;

namespace ApiWrapper.Implementation.RequestLimiter
{
    /// <summary>
    /// RiotSingletonRequestLimiterBuilder builder - this limiter is singleton so just GetRequestLimiter method invoke is enough.
    /// </summary>
    public class RiotSingletonRequestLimiterBuilder : IRequestLimiterBuilder
    {
        
        /// <summary>
        /// Do nothing. Builder pattern is here just for future extension opportunity.
        /// This limiter is singleton so building is senseless. Just invoke GetRequestLimiter method.
        /// </summary>
        public void BuildRequestLimiter()
        {
        }

        /// <summary>
        /// Gets the RiotSingletonRequestLimiter singleton instance
        /// </summary>
        /// <returns>RiotSingletonRequestLimiter instance</returns>
        public IRequestLimiter GetRequestLimiter()
        {
            return RiotSingletonRequestLimiter.Instance;
        }
    }
}