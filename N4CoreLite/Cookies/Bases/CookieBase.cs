#nullable disable

using Microsoft.AspNetCore.Http;

namespace N4CoreLite.Cookies.Bases
{
    public abstract class CookieBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected CookieBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual void Set(string key, string value, CookieOptions cookieOptions)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
        }

        public virtual void Set(string key, string value, int hours)
        {
            var cookieOptions = new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddHours(hours)),
                HttpOnly = true
            };
            Set(key, value, cookieOptions);
        }

        public virtual string Get(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public virtual void Remove(string key)
        {
            var cookieOptions = new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(-1)),
                HttpOnly = true
            };
            Set(key, string.Empty, cookieOptions);
        }
    }
}
