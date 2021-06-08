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