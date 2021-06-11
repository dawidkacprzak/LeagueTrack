namespace ApiWrapper.Abstract.RequestLimiter
{
    /// <summary>
    /// RequestLimiter interface - instances are created to limit output to any APIs
    /// </summary>
    public interface IRequestLimiter
    {
        /// <summary>
        /// Should check request should be delayed - if yes then implement some delay
        /// </summary>
        /// <param name="requestIdentifier">Request identifier - can be useful to distinguish request types</param>
        public void Limit(string requestIdentifier);
    }
}