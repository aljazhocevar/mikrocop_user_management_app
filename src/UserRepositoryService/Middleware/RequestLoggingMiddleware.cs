using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;
using System.IO;
using System.Text;
using System;

namespace UserRepositoryService.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next) { _next = next; }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var clientName = context.Items.ContainsKey("ClientName") ? context.Items["ClientName"].ToString() : "-";
            var hostName = Environment.MachineName;
            var method = request.Method + " " + request.Path;

            request.EnableBuffering();
            string body = "";
            if (request.ContentLength > 0 && request.Body.CanSeek)
            {
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    body = await reader.ReadToEndAsync();
                    request.Body.Position = 0;
                }
            }

            Log.Information("[Info] {Time} {ClientIP} {ClientName} {Host} {ApiMethod} Params: {Params}", DateTime.UtcNow, ip, clientName, hostName, method, body);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Error] {Time} {ClientIP} {ClientName} {Host} {ApiMethod} Params: {Params} Message: {Message}", DateTime.UtcNow, ip, clientName, hostName, method, body, ex.Message);
                throw;
            }
        }
    }
}
