using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Globomantics.Api.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =true, Inherited =true)]
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        private const string _ApiKeyHeaderName = "XApiKey";
        private readonly IConfiguration config;

        public ApiKeyAttribute(IConfiguration config)
        {
            this.config = config;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var allowSwaggerForAnonymous = httpContext.Request.Path.StartsWithSegments("/swagger");
            var apiKeyIsPresent = httpContext.Request.Headers.TryGetValue(_ApiKeyHeaderName, out var consumerKey);
            var apiKey = config[_ApiKeyHeaderName];


            if ((apiKeyIsPresent && apiKey == consumerKey) || allowSwaggerForAnonymous){
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
