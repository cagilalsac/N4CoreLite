using Microsoft.AspNetCore.Http;
using N4CoreLite.Sessions.Bases;

namespace N4CoreLite.Sessions
{
    public class Session : SessionBase
    {
        public Session(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
