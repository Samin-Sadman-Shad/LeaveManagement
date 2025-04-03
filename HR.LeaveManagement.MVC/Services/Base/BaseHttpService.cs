using HR.LeaveManagement.MVC.Contracts;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client { get; set; }
        protected ILocalStorageService _localStorage { get; set; }

        public BaseHttpService(IClient client, ILocalStorageService storage)
        {
            _client = client;
            _localStorage = storage;
        }

        /// <summary>
        /// If the token exists, add it to the Authorization header of the HttpClient
        /// </summary>
        protected void AddBearerToken()
        {
            if (_localStorage.DoesExist("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _localStorage.getStorageValue<string>("token"));
            }
        }

        protected Response<Guid> ConvertApiException<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };
            }
            else
            {
                return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
            }
        }
    }
}
