using System.Collections.Generic;

namespace API.Abstract
{
    public interface IRequest
    {
        public Dictionary<string, string> GetHeaderParams();
        public Dictionary<string, string> GetQueryParams();
        public void SetHeaderParams(Dictionary<string, string> parameters);
        public void SetQueryParams(Dictionary<string, string> parameters);
        public void SetMethodPath(string methodPath);
    }
}
