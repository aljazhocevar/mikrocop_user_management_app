using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UserRepositoryService.Data;
using Microsoft.EntityFrameworkCore;

namespace UserRepositoryService.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiKeyMiddleware(RequestDelegate next) { _next = next; }
        public async Task InvokeAsync(HttpContext context, AppDbContext db)
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "API Key missing" });
                return;
            }

            var key = extractedApiKey.ToString();
            var client = await db.Clients.FirstOrDefaultAsync(c => c.ApiKey == key);
            if (client == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid API Key" });
                return;
            }

            context.Items["ClientName"] = client.Name;
            context.Items["ClientId"] = client.Id.ToString();

            await _next(context);
        }
    }
}
