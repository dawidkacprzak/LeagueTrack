/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;
using System.Net.Http;
using System.Threading.Tasks;
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
        private Task<string> responseContent;
        private string message;

        /// <summary>
        /// Creates new Riot response object based on HttpResponseMessage
        /// </summary>
        /// <param name="message">Response from httpclient</param>
        /// <param name="ex">Exception from response sender</param>
        public RiotResponse(HttpResponseMessage message, Exception ex = null)
        {
            if (message != null)
            {
                success = message.IsSuccessStatusCode;
                responseContent = message.Content.ReadAsStringAsync();
            }
            else
            {
                success = false;
            }

            thrownException = ex;
            if (ex != null) this.message = ex.Message;
        }
        
        /// <summary>
        /// Information request finished with success status code
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
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
        public async Task<string> GetResponseContentAsync()
        {
            return await responseContent.ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves string response content
        /// </summary>
        /// <returns></returns>
        public string GetResponseContent()
        {
            return responseContent.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retrieves exception message if occured
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return message;
        }
    }
}