/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
namespace ApiWrapper.Abstract.RequestSender
{
    /// <summary>
    /// Request sender interface - base for concrete request sender builder pattern
    /// </summary>
    public interface IRequestSenderBuilder
    {
        /// <summary>
        /// Builds request object
        /// </summary>
        public void BuildRequestSender();
        
        /// <summary>
        /// Returns request sender built object - object is build only when BuildRequestSender method invoked
        /// </summary>
        /// <returns></returns>
        public IRequestSender GetRequestSender();
    }
}