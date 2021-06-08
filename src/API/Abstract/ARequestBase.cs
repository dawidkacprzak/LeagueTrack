using System.Collections.Generic;

namespace API.Abstract
{
    public abstract class ARequestBase : IRequest
    {
        private Dictionary<string, string> queryParameters;
        private Dictionary<string, string> headerParameters;
        public virtual string HttpAddress { get; protected set; }
        public virtual string MethodPath { get; protected set; }
        public ARequestBase()
        {
            queryParameters = new Dictionary<string, string>();
            headerParameters = new Dictionary<string, string>();
        }

        public virtual Dictionary<string, string> GetHeaderParams()
        {
            return headerParameters;
        }

        public virtual Dictionary<string, string> GetQueryParams()
        {
            return queryParameters;
        }

        public virtual void SetHeaderParams(Dictionary<string, string> parameters)
        {
            this.headerParameters = parameters;
        }

        public virtual void SetHttpAddress(string httpAddress)
        {
            this.HttpAddress = httpAddress;
        }

        public virtual void SetQueryParams(Dictionary<string, string> parameters)
        {
            this.queryParameters = parameters;
        }

        public virtual void SetMethodPath(string methodPath)
        {
            this.MethodPath = methodPath;
        }
    }
}
