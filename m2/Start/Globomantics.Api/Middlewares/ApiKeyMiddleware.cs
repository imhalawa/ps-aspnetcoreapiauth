
namespace Globomantics.Api.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;
        private const string _ApiKeyName = "XApiKey";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            var allowAccessToSwagger = context.Request.Path.StartsWithSegments("/swagger");
            var apiKeyPresentInHeader = context.Request.Headers.TryGetValue(_ApiKeyName, out var consumerKey);
            var apiKey = config[_ApiKeyName];

            if ((apiKeyPresentInHeader && apiKey == consumerKey) || allowAccessToSwagger)
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid Api key");
        }
    }

    public static class ApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKey(this IApplicationBuilder self) => self.UseMiddleware<ApiKeyMiddleware>();
    }
}
