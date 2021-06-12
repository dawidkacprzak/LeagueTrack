/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestLimiter;
using ApiWrapper.Abstract.RequestSender;
using ApiWrapper.Abstract.Response;
using ApiWrapper.Enum;
using ApiWrapper.Implementation.Request;
using ApiWrapper.Implementation.Response;

namespace ApiWrapper.Implementation.RequestSender
{
    /// <summary>
    /// Concrete request sender implementation for request builder
    /// </summary>
    public class RiotRequestSender : IRequestSender
    {
        private IRequestLimiter<ERiotRequest> limiter;
        private int retryCount;

        /// <summary>
        /// RiotRequestSender contructor
        /// </summary>
        /// <param name="limiter">IRequestLimiter implementation</param>
        /// <param name="retryCount">Amount of unsuccessful request retry</param>
        public RiotRequestSender(IRequestLimiter<ERiotRequest> limiter, int retryCount)
        {
            this.limiter = limiter;
            this.retryCount = retryCount;
        }

        /// <summary>
        /// Riot GET method implementation
        /// </summary>
        /// <param name="request">IRequest implementation</param>
        /// <returns>IResponse implementation</returns>
        [ExcludeFromCodeCoverage]
        public async Task<IResponse> GetAsync(IRequest request)
        {
            Exception exception = null;
            HttpResponseMessage response = null;

            using HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(request.GetHttpAddress()),
                Timeout = TimeSpan.FromMinutes(5)
            };

            PrepareRequestWithHeaders(client, request.GetHeaderParams());
   
            try
            {
                if (request.GetType() == typeof(RiotRequest))
                {
                    int currentRetryCount = 0;
                    do
                    {
                        await limiter.Limit(((RiotRequest) request).RiotRequestType);
                        response = await client.GetAsync(request.GetHttpAddress()).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            break;
                        }

                        if (response.Headers.Contains("Retry-After"))
                        {
                            List<string> retryAfterValues = response.Headers.GetValues("Retry-After").ToList();
                            if (retryAfterValues.Any())
                            {
                                await Task.Delay(int.Parse(retryAfterValues.First()) * 1000).ConfigureAwait(false);
                            }
                        }
                        currentRetryCount++;
                    } while (currentRetryCount <= this.retryCount);
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return new RiotResponse(response, exception);
        }

        
        /// <summary>
        /// Retrurns current maximum retry count of unsuccessful requests
        /// </summary>
        /// <returns></returns>
        public int GetMaxRetryCount()
        {
            return retryCount;
        }

        private void PrepareRequestWithHeaders(HttpClient client, Dictionary<string, string> headerParameters)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            foreach (KeyValuePair<string, string> header in headerParameters)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
    }
}