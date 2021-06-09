using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;
using ApiWrapper.Abstract.Response;
using ApiWrapper.Implementation.Response;

namespace ApiWrapper.Implementation.RequestSender
{
    /// <summary>
    /// Concrete request sender implementation for request builder
    /// </summary>
    public class RiotRequestSender : IRequestSender
    {
        /// <summary>
        /// Riot GET method implementation
        /// </summary>
        /// <param name="request">IRequest implementation</param>
        /// <returns>IResponse implementation</returns>
        public async Task<IResponse> GetAsync(IRequest request)
        {
            Exception exception = null;
            HttpResponseMessage response = null;

            using HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(request.GetHttpAddress()),
                Timeout = TimeSpan.FromMinutes(5)
            };
                
            PrepareRequestWithHeaders(client,request.GetHeaderParams());
            
            try
            {
                 response = await client.GetAsync(request.GetHttpAddress()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            
            return new RiotResponse(response, exception);
        }

        private void PrepareRequestWithHeaders(HttpClient client, Dictionary<string,string> headerParameters)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            foreach (KeyValuePair<string, string> header in headerParameters)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
    }
}

