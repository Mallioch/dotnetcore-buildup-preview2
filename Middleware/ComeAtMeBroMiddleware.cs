using System.Threading.Tasks; //For the Task type
using Microsoft.AspNetCore.Http; //For HttpContext and RequestDelegate
using Microsoft.Extensions.Logging;//For ILoggerFactory
using System.IO;//For file reading and path building

namespace BuildUp.Middleware
{
    public class ComeAtMeBroMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ComeAtMeBroMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ComeAtMeBroMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("ComeAtMeBroMiddleware Start");

            if (context.Request.Path.ToString().Contains("ComeAtMeBro"))
            {
                _logger.LogInformation("Ending the request");
                
                var bytes = File.ReadAllBytes(Path.Combine("wwwroot", "come-at-me-bro.jpg"));
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                return;
            }

            await _next.Invoke(context);
            _logger.LogInformation("ComeAtMeBroMiddleware End");
        }
    }
}