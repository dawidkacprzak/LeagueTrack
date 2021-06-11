using ApiWrapper.Abstract.RequestLimiter;

namespace ApiWrapper.Implementation.RequestLimiter
{
    public class RequestLimiterDirector
    {
        public IRequestLimiterBuilder RequestLimiterBuilder;

        public void Construct()
        {
            RequestLimiterBuilder.BuildRequestLimiter();
        }


    }
}