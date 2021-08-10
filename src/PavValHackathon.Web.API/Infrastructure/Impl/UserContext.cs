using System;
using Microsoft.AspNetCore.Http;

namespace PavValHackathon.Web.API.Infrastructure.Impl
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public int UserId
        {
            get
            {
                //TODO: get userId from the OAuth2 Bearer token
                if (_contextAccessor.HttpContext is null ||
                    !_contextAccessor.HttpContext.Request.Headers.TryGetValue("tmp-user-sub", out var userIdStr) ||
                    !int.TryParse(userIdStr, out var userId))
                {
                    //TODO: throw a forbidden exception
                    throw new Exception();
                }
                
                return userId;
            }
        }
    }
}