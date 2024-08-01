using Microsoft.AspNetCore.Http;
using N4CoreLite.Cookies.Bases;

namespace N4CoreLite.Cookies
{
    public class Cookie : CookieBase
    {
        public Cookie(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
