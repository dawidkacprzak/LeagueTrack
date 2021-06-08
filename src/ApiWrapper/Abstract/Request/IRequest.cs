using System.Collections.Generic;

namespace ApiWrapper.Abstract.Request
{
    /// <summary>
    /// Base interface for request wrappers
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Retrieves header parameters
        /// </summary>
        /// <returns>Dictionary with header parameters</returns>
        public Dictionary<string, string> GetHeaderParams();

        /// <summary>
        /// Retrieves query parameters
        /// </summary>
        /// <returns>Dictionary with query parameters</returns>
        public Dictionary<string, string> GetQueryParams();

        /// <summary>
        /// Set new object as header parameters
        /// </summary>
        /// <param name="parameters">Object which replace header parameters</param>
        public void SetHeaderParams(Dictionary<string, string> parameters);

        /// <summary>
        /// Set new object as query parameters
        /// </summary>
        /// <param name="parameters">Object which replace query parameters</param>
        public void SetQueryParams(Dictionary<string, string> parameters);

        /// <summary>
        /// Set method path - often concatenated to base uri path
        /// </summary>
        /// <param name="methodPath">String as method path</param>
        public void SetMethodPath(string methodPath);
    }
}