/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;

namespace ApiWrapper.Implementation.RequestSender
{
    
    /// <summary>
    /// Request sender director builder pattern
    /// </summary>
    public class RequestSenderDirector
    {
        /// <summary>
        /// Concrete builder for director
        /// </summary>
        public IRequestSenderBuilder builder;

        /// <summary>
        /// Constructs request from specified builder
        /// </summary>
        public void Construct()
        {
            builder.BuildRequestSender();
        }
    }
}