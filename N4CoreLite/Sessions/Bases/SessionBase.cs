#nullable disable

using Microsoft.AspNetCore.Http;
using N4CoreLite.Extensions;

namespace N4CoreLite.Sessions.Bases
{
    public abstract class SessionBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected SessionBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual void Remove(string sessionKey)
        {
            _httpContextAccessor.HttpContext.Session.Remove(sessionKey);
        }

        public virtual T Get<T>(string sessionKey) where T : class
        {
            return _httpContextAccessor.HttpContext.Session.Get<T>(sessionKey);
        }

        public virtual void Set<T>(T sessionObject, string sessionKey) where T : class
        {
            _httpContextAccessor.HttpContext.Session.Set(sessionKey, sessionObject);
        }
    }
}
