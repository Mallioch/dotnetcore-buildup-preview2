using Microsoft.AspNetCore.Builder; //For IApplicationBuilder
using Microsoft.AspNetCore.Http;    //For Response.WriteAsync
using Microsoft.Extensions.DependencyInjection; //For IServiceCollection
using Microsoft.Extensions.Logging; //For ILoggerFactory
using Microsoft.AspNetCore.Hosting;
using BuildUp.Middleware;

namespace BuildUp
{
    public class Startup
    {
        //Setup dependencies for injection here
        public void ConfigureServices(IServiceCollection services)
        {
            //Nothing yet!
        }

        //Configure your middleware pipeline
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ComeAtMeBroMiddleware>();
            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                var logger = loggerFactory.CreateLogger("Pipeline End");
                logger.LogInformation("You have reached the end of the middleware pipeline");
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}