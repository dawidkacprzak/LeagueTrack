using System;
using ApiWrapper.Abstract.RequestLimiter;

namespace ApiWrapper.Implementation.RequestLimiter
{
    /// <summary>
    /// RiotSingletonRequestLimiter - created to 'limit' maximum request output to Riot API.
    /// </summary>
    public sealed class RiotSingletonRequestLimiter : IRequestLimiter
    {
        private RiotSingletonRequestLimiter()
        {
        }

        private static readonly Lazy<RiotSingletonRequestLimiter> LazyRiotLimiterInstance =
            new Lazy<RiotSingletonRequestLimiter>(() => new RiotSingletonRequestLimiter());

        /// <summary>
        /// Gets the RiotSingletonRequestLimiter singleton instance
        /// </summary>
        public static RiotSingletonRequestLimiter Instance => LazyRiotLimiterInstance.Value;

        public void Limit(string requestIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}