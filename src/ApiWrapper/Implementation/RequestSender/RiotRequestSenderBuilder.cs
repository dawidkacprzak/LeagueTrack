using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;
using ApiWrapper.Implementation.Request;

namespace ApiWrapper.Implementation.RequestSender
{
    /// <summary>
    /// Concrete riot request builder implementation
    /// </summary>
    public class RiotRequestSenderBuilder : IRequestSenderBuilder
    {
        private readonly RiotRequestSender sender = new RiotRequestSender();

        /// <summary>
        /// Builds new Riot request sender
        /// </summary>
        public void BuildRequestSender()
        {
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