namespace API.Abstract
{
    /// <summary>
    /// Interface for new endpoints
    /// </summary>
    public interface IEndpoint
    {
         /// <summary>
         /// Get URI endpoint
         /// </summary>
         /// <returns>String value of endpoint path</returns>
         string GetEndpointPath();
    }
}
