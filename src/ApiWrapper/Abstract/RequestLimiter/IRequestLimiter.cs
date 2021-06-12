/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System;
using System.Threading.Tasks;
using ApiWrapper.Abstract.Response;

namespace ApiWrapper.Abstract.RequestLimiter
{
    /// <summary>
    /// RequestLimiter interface - instances are created to limit output to any APIs
    /// </summary>
    public interface IRequestLimiter<T>
    {
        /// <summary>
        /// Should check request should be delayed - if yes then implement some delay
        /// </summary>
        /// <param name="requestIdentifier">Request identifier - can be useful to distinguish request types</param>
        public Task Limit(T requestIdentifier);
    }
}