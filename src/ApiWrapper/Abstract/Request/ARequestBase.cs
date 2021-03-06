/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System;
using System.Collections.Generic;

namespace ApiWrapper.Abstract.Request
{
    /// <summary>
    /// Base request class for request wrappers
    /// </summary>
    public abstract class ARequestBase : IRequest
    {
        private Dictionary<string, string> queryParameters;
        private Dictionary<string, string> headerParameters;

        /// <summary>
        /// Final Uri sent to API
        /// </summary>
        protected virtual string httpAddress { get; set; }

        /// <summary>
        /// Method path for api. Useful when we want to build HttpAddress.
        /// </summary>
        public virtual string MethodPath { get; protected set; }

        /// <summary>
        /// Set query and header parameters to empty dictionaries
        /// </summary>
        public ARequestBase()
        {
            queryParameters = new Dictionary<string, string>();
            headerParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Get Header parameters
        /// </summary>
        /// <returns>Dictionary with key value parameters</returns>
        public virtual Dictionary<string, string> GetHeaderParams()
        {
            return headerParameters;
        }

        /// <summary>
        /// Retrieves final request uri
        /// </summary>
        /// <returns></returns>
        public string GetHttpAddress()
        {
            return httpAddress + BuildQueryParameters();
        }

        /// <summary>
        /// Get Query parameters
        /// </summary>
        /// <returns>Dictionary with key value parameters</returns>
        public virtual Dictionary<string, string> GetQueryParams()
        {
            return queryParameters;
        }


        /// <summary>
        /// Set header parameters - overriding whole object
        /// </summary>
        /// <param name="parameters">New header object</param>
        public virtual void SetHeaderParams(Dictionary<string, string> parameters)
        {
            this.headerParameters = parameters;
        }


        /// <summary>
        /// Set Uri for request
        /// </summary>
        /// <param name="httpAddress">New URI</param>
        public virtual void SetHttpAddress(string httpAddress)
        {
            this.httpAddress = httpAddress;
        }

        /// <summary>
        /// Set query parameters - overriding whole object
        /// </summary>
        /// <param name="parameters">New header object</param>
        public virtual void SetQueryParams(Dictionary<string, string> parameters)
        {
            this.queryParameters = parameters;
        }


        /// <summary>
        /// Set method path. Should be concatenated to end of base address 
        /// </summary>
        /// <param name="methodPath"></param>
        public virtual void SetMethodPath(string methodPath)
        {
            this.MethodPath = methodPath;
        }

        private string BuildQueryParameters()
        {
            char separator = '?';
            string finalQueryParams = string.Empty;
            foreach (var param in queryParameters)
            {
                finalQueryParams += $"{separator}{param.Key}={param.Value}";
                separator = '&';
            }

            return finalQueryParams;
        }
    }
}