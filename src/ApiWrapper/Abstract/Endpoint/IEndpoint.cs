/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
namespace ApiWrapper.Abstract.Endpoint
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
