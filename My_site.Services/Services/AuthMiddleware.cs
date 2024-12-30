using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using My_site.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.Services.Services
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuthMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["token"];

            if (token != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var accountService = scope.ServiceProvider.GetRequiredService<IJwtProvider>();
                    var principal = accountService.ValidateToken(token);

                    if (principal != null)
                    {
                        var role = principal.FindFirst("Role")?.Value;
                        context.Items["IsAuthenticated"] = true;
                        context.Items["UserName"] = principal.FindFirst("Username")?.Value;
                        context.Items["Role"] = int.Parse(role ?? "0");
                    }
                }
            }
            await _next(context);
        }
    }
}
