using System;
using System.Linq;
using System.Security.Claims;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers
{
    public static class AuthenticateHelper
    {
        public static int AuthenticateUserId()
        {
            var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var id = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.PrimarySid);
            return Convert.ToInt32(id?.Value);
        }
    }
}