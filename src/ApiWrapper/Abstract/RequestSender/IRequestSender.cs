/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.Response;

namespace ApiWrapper.Abstract.RequestSender
{
    /// <summary>
    /// Request sender interface - designed for sending http requests
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        /// HTTP GET implementation
        /// </summary>
        /// <param name="request">IRequest interface implementation</param>
        /// <returns>IResponse object implementation</returns>
        public Task<IResponse> GetAsync(IRequest request);

    }
}