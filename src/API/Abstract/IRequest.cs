using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Abstract
{
    public interface IRequest
    {
        public void SetHttpAddress(string address);
        public Dictionary<string, string> GetHeaderParams();
        public Dictionary<string, string> GetQueryParams();
        public void SetHeaderParams(Dictionary<string, string> parameters);
        public void SetQueryParams(Dictionary<string, string> parameters);
    }
}
