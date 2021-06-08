using System;
using System.Net.Http;
using ApiWrapper.Abstract.Response;

namespace ApiWrapper.Implementation.Response
{
    /// <summary>
    /// Class for wrapping riot response object
    /// </summary>
    public class RiotResponse : IResponse
    {
        private bool success;
        private Exception thrownException;
        private string responseContent;

        /// <summary>
        /// Creates new Riot response object based on HttpResponseMessage
        /// </summary>
        /// <param name="message">Response from httpclient</param>
        /// <param name="ex">Exception from response sender</param>
        public RiotResponse(HttpResponseMessage message, Exception ex = null)
        {
            success = message.IsSuccessStatusCode;
            responseContent = message.Content.ReadAsStringAsync().Result;
            thrownException = ex;
        }
        
        /// <summary>
        /// Information request finished with success status code
        /// </summary>
        /// <returns></returns>
        public bool Success()
        {
            return success;
        }

        /// <summary>
        /// Retrieves exception if any thrown
        /// </summary>
        /// <returns></returns>
        public Exception GetThrownException()
        {
            return thrownException;
        }

        /// <summary>
        /// Retrieves string response content
        /// </summary>
        /// <returns></returns>
        public string GetResponseContent()
        {
            return responseContent;
        }
    }
}