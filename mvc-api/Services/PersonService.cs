using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace mvc_api.Services
{
    public class PersonService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public PersonService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLoginName()
        {
            return _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "none";
        }
    }
}
