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

        public int UserId =>
            //int.Parse(_contextAccessor.HttpContext.Request.Headers["user-sub"]);
            1;
    }
}