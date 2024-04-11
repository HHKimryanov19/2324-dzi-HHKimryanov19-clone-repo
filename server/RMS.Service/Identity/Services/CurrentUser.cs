using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RMS.Shared.Contracts;

namespace RMS.Service.Identity.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private Guid? userId;

        public CurrentUser(IHttpContextAccessor httpContextAccessor = default!)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id
        {
            get
            {
                if (!this.userId.HasValue)
                {
                    var rawUserId = this.httpContextAccessor?
                        .HttpContext?
                        .User?
                        .Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                        .Value;

                    if (!string.IsNullOrWhiteSpace(rawUserId) && Guid.TryParse(rawUserId, out var parseUserId))
                    {
                        this.userId = parseUserId;
                    }
                }

                return this.userId;
            }
        }
    }
}