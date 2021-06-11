namespace ApiWrapper.Abstract.RequestLimiter
{
    public interface IRequestLimiterBuilder
    {
        public void BuildRequestLimiter();

        public IRequestLimiter GetRequestLimiter();
    }
}