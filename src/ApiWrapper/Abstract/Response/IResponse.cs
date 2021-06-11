/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;
using System.Threading.Tasks;

namespace ApiWrapper.Abstract.Response
{
    /// <summary>
    /// Response interface for api calls
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Information - did api returned successful status code?
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess();

        /// <summary>
        /// Returns exception from request sender if any occurred
        /// </summary>
        /// <returns></returns>
        public Exception GetThrownException();

        /// <summary>
        /// Returns response message as string
        /// </summary>
        /// <returns></returns>
        public Task<string> GetResponseContentAsync();

        /// <summary>
        /// Returns response message as string
        /// </summary>
        /// <returns></returns>
        public string GetResponseContent();

        /// <summary>
        /// Returns message if occurred
        /// </summary>
        /// <returns></returns>
        public string GetMessage();
    }
}