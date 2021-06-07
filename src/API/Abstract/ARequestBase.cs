using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Abstract
{
    public abstract class ARequestBase : IRequest
    {
        protected Dictionary<string, string> QueryParameters;
        protected Dictionary<string, string> HeaderParameters;
        public virtual string HttpAddress { get; protected set; }
        public virtual string MethodPath { get; protected set; }
        public ARequestBase()
        {
            QueryParameters = new Dictionary<string, string>();
            HeaderParameters = new Dictionary<string, string>();
        }

        public virtual Dictionary<string, string> GetHeaderParams()
        {
            return HeaderParameters;
        }

        public virtual Dictionary<string, string> GetQueryParams()
        {
            return QueryParameters;
        }

        public virtual void SetHeaderParams(Dictionary<string, string> parameters)
        {
            this.HeaderParameters = parameters;
        }

        public virtual void SetHttpAddress(string httpAddress)
        {
            this.HttpAddress = httpAddress;
        }

        public virtual void SetQueryParams(Dictionary<string, string> parameters)
        {
            this.QueryParameters = parameters;
        }

        public virtual void SetMethodPath(string methodPath)
        {
            this.MethodPath = methodPath;
        }
    }
}
