/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/

using System.Collections.Generic;
using ApiWrapper.Abstract;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Enum;

namespace ApiWrapper.Implementation.Request
{
    /// <summary>
    /// Concrete builder pattern implementation for riot requests
    /// </summary>
    public class RiotRequestBuilder : IRequestBuilder
    {
        private readonly RiotRequest request = new RiotRequest();
        private readonly ELocation location;
        private readonly string riotApiKey;
        private string methodPath;
        private readonly Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        private readonly Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        private readonly ERiotRequest riotRequestType = ERiotRequest.NoInfo;

        /// <summary>
        /// Creates new builder object
        /// </summary>
        /// <param name="apiKey">prod/dev riot api key</param>
        /// <param name="location">Location</param>
        public RiotRequestBuilder(string apiKey, ELocation location)
        {
            this.riotApiKey = apiKey;
            this.location = location;
        }

        /// <summary>
        /// Creates new builder object
        /// </summary>
        /// <param name="apiKey">prod/dev riot api key</param>
        /// <param name="location">Location</param>
        public RiotRequestBuilder(string apiKey, ELocation location, ERiotRequest requestType)
        {
            this.riotApiKey = apiKey;
            this.location = location;
            riotRequestType = requestType;
        }

        /// <summary>
        /// Add new header parameter for all built requests
        /// </summary>
        /// <param name="key">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public void AddHeaderParam(string key, string value)
        {
            headerParameters.Add(key, value);
        }

        /// <summary>
        /// Add new query parameter for all built requests
        /// </summary>
        /// <param name="key">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public void AddQueryParam(string key, string value)
        {
            queryParameters.Add(key, value);
        }

        /// <summary>
        /// Builds new riot request
        /// </summary>
        public void BuildRequest()
        {
            Dictionary<string, string> headerParametersWithKey = headerParameters;
            headerParametersWithKey.Add("X-Riot-Token", riotApiKey);

            request.SetHeaderParams(this.headerParameters);
            request.SetQueryParams(this.queryParameters);
            request.SetLocation(this.location);
            request.SetMethodPath(this.methodPath);
            request.RiotRequestType = riotRequestType;
        }

        /// <summary>
        /// Retrieves new built RiotRequest
        /// </summary>
        /// <returns>New riot request, empty RiotRequest(not null) object if not build with BuildRequest method</returns>
        public IRequest GetRequest()
        {
            return request;
        }

        /// <summary>
        /// Sets the method path. Concatenated at the end of base uri path
        /// </summary>
        /// <param name="path"></param>
        public void SetMethodPath(string path)
        {
            this.methodPath = path;
        }
    }
}