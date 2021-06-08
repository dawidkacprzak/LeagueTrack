using System.Collections.Generic;

namespace ApiWrapper.Abstract.Request
{
    
    /// <summary>
    /// Direction for request builder pattern
    /// </summary>
    public class RequestDirector
    {
        /// <summary>
        /// Builder used for director
        /// </summary>
        public IRequestBuilder builder;

        /// <summary>
        /// Builds new request
        /// </summary>
        /// <param name="methodPath">Method uri math - concatenated at the end of base path</param>
        /// <param name="queryParams">Query parameters for request</param>
        /// <param name="headerParams">Header parameters for request</param>
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
