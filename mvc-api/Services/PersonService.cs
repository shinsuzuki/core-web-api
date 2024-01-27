using Microsoft.AspNetCore.Http;
using mvc_api.Config;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace mvc_api.Services
{
    public class PersonService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMyConfig _myConfig;

        public PersonService(IHttpContextAccessor httpContextAccessor, IMyConfig myConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _myConfig = myConfig;

        }

        public string GetLoginName()
        {
            // todo 設定値の取得テスト
            var loglevel = _myConfig.GetConfigurationRoot()["Logging:LogLevel:Default"];

            return _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "none";
        }
    }
}
