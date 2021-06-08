using System;

namespace ApiWrapper.Abstract.Response
{
    /// <summary>
    /// Response interface for api calls
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Information - did api returned successful status code?
        /// </summary>
        /// <returns></returns>
        public bool Success();

        /// <summary>
        /// Returns exception from request sender if any occured
        /// </summary>
        /// <returns></returns>
        public Exception GetThrownException();

        /// <summary>
        /// Returns response message as string
        /// </summary>
        /// <returns></returns>
        public string GetResponseContent();
    }
}