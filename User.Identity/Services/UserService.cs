using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using User.Identity.Dtos;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using BuildingBlocks.Resilience.Http;
using zipkin4net.Transport.Http;

namespace User.Identity.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly string _userServiceUrl = "http://localhost:59931";
        private ILogger<UserService> _logger;
        private IHttpClient _identityClient;
        public UserService(/*HttpClient httpClient,*/ ILogger<UserService> logger, IHttpClient identityClient)
        {
             _httpClient = new HttpClient(new TracingHandler("identity_api"));//添加zipkin跟踪
            _logger = logger;
            _identityClient = identityClient;
        }
        public async Task<UserInfo> CheckOrCreate(string phone)
        {
            var form=new Dictionary<string, string> { { "phone",phone} };
            var content = new FormUrlEncodedContent(form);
            var response = await _httpClient.PostAsync(_userServiceUrl + "/api/users/check-or-create", content);
            //var response = await _identityClient.PostAsync(_userServiceUrl + "/api/users/check-or-create", content);
            

            if (response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(result);
                _logger.LogTrace($"Completed CheckorCreate:{userInfo.Id}");
                return userInfo;
            }
            return null;
        }
    }
}
