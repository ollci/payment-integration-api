using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PaymentIntegration.Helper
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "Authorization";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Headers.TryGetValue(HeaderName, out var extractedToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API token is missing.");
                return;
            }

            var expectedToken = configuration["ApiSecurity:ApiKey"];

            if (string.IsNullOrEmpty(expectedToken) || expectedToken != extractedToken)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized API token.");
                return;
            }

            await _next(context);
        }
    }
}
