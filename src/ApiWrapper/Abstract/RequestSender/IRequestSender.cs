using System.Net.Http;
using System.Threading.Tasks;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.Response;

namespace ApiWrapper.Abstract.RequestSender
{
    /// <summary>
    /// Request sender interface - designed for sending http requests
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        /// HTTP GET implementation
        /// </summary>
        /// <param name="request">IRequest interface implementation</param>
        /// <returns>IResponse object implementation</returns>
        public Task<IResponse> GetAsync(IRequest request);

    }
}