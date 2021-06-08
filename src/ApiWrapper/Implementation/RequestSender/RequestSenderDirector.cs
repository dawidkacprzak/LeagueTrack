using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.RequestSender;

namespace ApiWrapper.Implementation.RequestSender
{
    
    /// <summary>
    /// Request sender director builder pattern
    /// </summary>
    public class RequestSenderDirector
    {
        /// <summary>
        /// Concrete builder for director
        /// </summary>
        public IRequestSenderBuilder builder;

        /// <summary>
        /// Constructs request from specified builder
        /// </summary>
        public void Construct()
        {
            builder.BuildRequestSender();
        }
    }
}