using API.Abstract;
using System.Collections.Generic;

namespace API
{
    public class RequestDirector
    {
        public IRequestBuilder builder;

        public void Construct(string methodPath, Dictionary<string, string> queryParams = null, Dictionary<string, string> headerParams = null)
        {
            if (queryParams != null)
                foreach (var qp in queryParams)
                    builder.AddQueryParam(qp.Key, qp.Value);

            if (headerParams != null)
                foreach (var hp in headerParams)
                    builder.AddHeaderParam(hp.Key, hp.Value);

            builder.SetMethodPath(methodPath);

            builder.BuildRequest();
        }


    }
}
