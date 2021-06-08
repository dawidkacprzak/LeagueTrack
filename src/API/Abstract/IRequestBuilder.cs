namespace API.Abstract
{
    /// <summary>
    /// Builder pattern - used for request object creation
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Finalizer method. Building new request object
        /// </summary>
        void BuildRequest();

        /// <summary>
        /// Adds new query parameter
        /// </summary>
        /// <param name="key">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        void AddQueryParam(string key, string value);

        /// <summary>
        /// Adds new header parameter
        /// </summary>
        /// <param name="key">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        void AddHeaderParam(string key, string value);

        /// <summary>
        /// Sets the method path - often concatenated to base uri
        /// </summary>
        /// <param name="path"></param>
        void SetMethodPath(string path);

        /// <summary>
        /// Retrieves built request object
        /// </summary>
        /// <returns>IRequest object</returns>
        IRequest GetRequest();
    }
}