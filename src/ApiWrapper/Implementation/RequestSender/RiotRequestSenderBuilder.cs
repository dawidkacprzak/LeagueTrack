/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;
using ApiWrapper.Implementation.Request;
using ApiWrapper.Implementation.RequestLimiter;

namespace ApiWrapper.Implementation.RequestSender
{
    /// <summary>
    /// Concrete riot request builder implementation
    /// </summary>
    public class RiotRequestSenderBuilder : IRequestSenderBuilder
    {
        private RiotRequestSender sender;

        /// <summary>
        /// Builds new Riot request sender
        /// </summary>
        public void BuildRequestSender()
        {
            sender = new RiotRequestSender(RiotSingletonRequestLimiter.Instance);
        }

        /// <summary>
        /// Returns built request sender created by BuildRequestSender method
        /// </summary>
        public IRequestSender GetRequestSender()
        {
            return sender;
        }
    }
}