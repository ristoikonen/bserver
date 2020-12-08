using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using bserver.Data;
using Microsoft.AspNetCore.ResponseCompression;
using System.Net.Http;



namespace bserver
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //services.AddSignalR().AddAzureSignalR();
            //services.AddSignalR();

            //services.AddSignalR().AddAzureSignalR().AddHubOptions<ChatHub>(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //    options.KeepAliveInterval = TimeSpan.FromMinutes(1);
            //});

            services.AddSingleton<WeatherForecastService>();
            //services.AddSingleton<HttpClient>();


            //services.AddResponseCompression(opts =>
            //{
            //    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            //        new[] { "application/octet-stream" });
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseResponseCompression();

            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chathub");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapBlazorHub();
                //endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapFallbackToPage("/_Host");
            });


            //TODO
            //add https://portal.azure.com/#@aihw.gov.au/resource/subscriptions/84a2a0b8-898f-49d1-bc5f-ef38d184eb2b/resourcegroups/Res1/providers/Microsoft.SignalRService/SignalR/SigService/quickstart


            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chat");
            //});
        }
    }
}
