using System.Threading.Tasks;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Abstract.Response;
using ApiWrapper.Enum;
using ApiWrapper.Implementation.RequestSender;
using ApiWrapper.Implementation.Response;

namespace ApiWrapper.Facade
{
    /// <summary>
    /// Simplified method for riot api requests using facade pattern
    /// </summary>
    public class RiotFacade
    {
        /// <summary>
        /// Facade Api instance
        /// </summary>
        public Api ApiInstance { get; private set; }
        private RiotRequestSender sender;
        
        /// <summary>
        /// RiotFacade constructor
        /// </summary>
        public RiotFacade()
        {
            this.ApiInstance = new Api(FacadeGlobalConfig.Api_key,FacadeGlobalConfig.OneSecondLimit,FacadeGlobalConfig.TwoMinutesLimit,FacadeGlobalConfig.Location);
            Build();
        }

        /// <summary>
        /// RiotFacade constructor
        /// </summary>
        public RiotFacade(ELocation location)
        {
            this.ApiInstance = new Api(FacadeGlobalConfig.Api_key,FacadeGlobalConfig.OneSecondLimit,FacadeGlobalConfig.TwoMinutesLimit,location);
            Build();
        }
        
        /// <summary>
        /// Renew api configuration for specified location
        /// </summary>
        /// <param name="location"></param>
        public void SetLocation(ELocation location)
        {
            this.ApiInstance = new Api(FacadeGlobalConfig.Api_key,FacadeGlobalConfig.OneSecondLimit,FacadeGlobalConfig.TwoMinutesLimit,location);
            Build();
        }

        /// <summary>
        /// Returns api current location
        /// </summary>
        /// <returns></returns>
        public ELocation GetLocation()
        {
            return ApiInstance.CurrentLocation;
        }

        private void Build()
        {
            var director = new RequestSenderDirector();
            var builder = new RiotRequestSenderBuilder(FacadeGlobalConfig.MaxRetryCount);
            director.builder = builder;
            director.Construct();
            sender = (RiotRequestSender) builder.GetRequestSender();
        }
        
        /// <summary>
        /// Sends get request for specified request
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <returns></returns>
        public async Task<RiotResponse> GetAsync(IRequest request)
        {
            return (RiotResponse) await sender.GetAsync(request);
        }
    }
}