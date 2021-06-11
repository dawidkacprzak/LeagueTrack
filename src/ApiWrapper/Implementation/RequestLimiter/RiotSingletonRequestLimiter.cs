/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWrapper.Abstract.RequestLimiter;
using ApiWrapper.Enum;

namespace ApiWrapper.Implementation.RequestLimiter
{
    /// <summary>
    /// RiotSingletonRequestLimiter - created to 'limit' maximum request output to Riot API.
    /// </summary>
    public sealed class RiotSingletonRequestLimiter : IRequestLimiter<ERiotRequest>
    {
        /// <summary>
        /// List of last requests
        /// </summary>
        public ConcurrentBag<AbsoluteRequest> AbsoluteRequests = new ConcurrentBag<AbsoluteRequest>();

        /// <summary>
        /// Configuration of maximum requests per sec
        /// </summary>
        public int AbsoluteOneSecRateLimit = 0;

        /// <summary>
        /// Configuration of maximum requests per two minutes
        /// </summary>
        public int AbsoluteTwoMinRateLimit = 0;

        /// <summary>
        /// 2 states of limit
        /// 1. Absolute rate limit check - https://developer.riotgames.com/app/XXX/info
        /// 2. Method header limit check - X-Method-Rate-Limit + Retry-After
        /// </summary>
        private RiotSingletonRequestLimiter()
        {
        }

        private static readonly Lazy<RiotSingletonRequestLimiter> LazyRiotLimiterInstance =
            new Lazy<RiotSingletonRequestLimiter>(() => new RiotSingletonRequestLimiter());

        /// <summary>
        /// Gets the RiotSingletonRequestLimiter singleton instance
        /// </summary>
        public static RiotSingletonRequestLimiter Instance => LazyRiotLimiterInstance.Value;

        /// <summary>
        /// Limits requests - force to wait X seconds based on remaining rate limit.
        /// </summary>
        /// <param name="requestIdentifier"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Limit(ERiotRequest requestIdentifier)
        {
            System.Diagnostics.Debug.WriteLine(AbsoluteRequests.Count + " - absolute requests");
            if (AbsoluteOneSecRateLimit == 0 || AbsoluteTwoMinRateLimit == 0)
                throw new Exception("Cannot limit request if configuration is not specified");

            await Task.Delay(CheckAbsoluteRateLimit(DateTime.Now)).ConfigureAwait(false);
            AbsoluteRequests = new ConcurrentBag<AbsoluteRequest>(AbsoluteRequests
                .Where(x => x.RequestDate >= DateTime.Now.Subtract(new TimeSpan(0, 3, 0))))
            {
                new AbsoluteRequest(DateTime.Now)
            };
        }


        /// <summary>
        /// Check absolute limit based on last requests
        /// </summary>
        /// <param name="checkDate">Relative date to check</param>
        /// <returns>Milliseconds to delay</returns>
        public int CheckAbsoluteRateLimit(DateTime checkDate)
        {
            IEnumerable<AbsoluteRequest> currentSecRequests =
                AbsoluteRequests.Where(x =>
                    x.RequestDate.Hour == checkDate.Hour &&
                    x.RequestDate.Minute == checkDate.Minute &&
                    x.RequestDate.Second == checkDate.Second
                ).AsEnumerable();
            if (currentSecRequests.Count() >= AbsoluteOneSecRateLimit)
            {
                return 1000; //If in this second rate limit exceed - just wait 1 second more to reset limit
            }
            else
            {
                DateTime twoMinDateCheck = checkDate.Subtract(new TimeSpan(0, 2, 0));
                ConcurrentBag<AbsoluteRequest> current2MinRequests = new ConcurrentBag<AbsoluteRequest>(AbsoluteRequests
                    .Where(x => x.RequestDate >= twoMinDateCheck
                    ).AsEnumerable());
                

                if (current2MinRequests.Count() > AbsoluteTwoMinRateLimit)
                {
                    AbsoluteRequest sortedMin = current2MinRequests.OrderByDescending(x => x.RequestDate).Last();
                    var delay = (checkDate - sortedMin.RequestDate).TotalMilliseconds;
                    System.Diagnostics.Debug.WriteLine($"Wait {2 * 60 * 1000 - Convert.ToInt32(delay)}ms");

                    return 2 * 60 * 1000 - Convert.ToInt32(delay);
                }

                return 0;
            }
        }

        /// <summary>
        /// Simple DateTime wrapper
        /// </summary>
        public struct AbsoluteRequest
        {
            /// <summary>
            /// DateTime store
            /// </summary>
            public DateTime RequestDate;

            /// <summary>
            /// DateTime wrapper constructor
            /// </summary>
            /// <param name="requestDate">DateTime to store</param>
            public AbsoluteRequest(DateTime requestDate)
            {
                RequestDate = requestDate;
            }
        }
    }
}