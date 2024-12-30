using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using My_site.DAL;
using My_site.Services.Services;
using System.Security.Claims;

namespace My_site.Domain.Attributes
{
    public class LogUserActionAttribute : ActionFilterAttribute
    {
        private readonly IUserActionLogger _logger;

        public LogUserActionAttribute(IUserActionLogger logger)
        {
            _logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var username = context.HttpContext.Items["UserName"]?.ToString();
            if (!username.IsNullOrEmpty())
            {
                var action = context.ActionDescriptor.RouteValues["action"];
                var controller = context.ActionDescriptor.RouteValues["controller"];
                var method = context.HttpContext.Request.Method;

                await _logger.LogAsync(username, action, controller, method);
            }

            await next();
        }
    }
}
