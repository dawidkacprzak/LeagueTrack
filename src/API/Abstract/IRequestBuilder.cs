namespace API.Abstract
{
    public interface IRequestBuilder
    {
        void BuildRequest();
        void AddQueryParam(string key, string value);
        void AddHeaderParam(string key, string value);
        void SetMethodPath(string path);
        IRequest GetRequest();
    }
}
