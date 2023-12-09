namespace DayZ_CommandAPI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class Api_Key
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "api_key";
        private readonly string _apiKeyValue;

        public Api_Key(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKeyValue = configuration.GetValue<string>("ApiKey");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/getdata") || context.Request.Path.StartsWithSegments("/api/discordgetdata"))
            {
                await _next(context);
                return;
            }

            // API key check for all other endpoints
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            if (!_apiKeyValue.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}
