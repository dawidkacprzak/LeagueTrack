using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Abstract
{
    public interface IRequestBuilder
    {
        void BuildRequest();
        void AddQueryParam(string key, string value);
        void AddHeaderParam(string key, string value);
        IRequest GetRequest();
    }
}
