using API.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class RequestDirector
    {
        public IRequestBuilder builder;

        public void Construct(Dictionary<string,string> queryParams, Dictionary<string,string> headerParams)
        {
            foreach(var qp in queryParams)
                builder.AddQueryParam(qp.Key, qp.Value);

            foreach (var hp in headerParams)
                builder.AddHeaderParam(hp.Key, hp.Value);

            builder.BuildRequest();
        }
    }
}
