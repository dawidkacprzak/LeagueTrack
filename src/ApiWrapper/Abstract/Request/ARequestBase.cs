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
        public virtual string HttpAddress { get; protected set; }

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
            this.HttpAddress = httpAddress;
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
    }
}