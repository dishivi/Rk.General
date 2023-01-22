using General.Models.Http;

namespace Core.ServeHttp.Interface
{
    public interface IDefaultHttpService
    {
        Task<HttpResponseMessage> GetAsync(string clientName, BaseHttpRequest request);
    }
}
