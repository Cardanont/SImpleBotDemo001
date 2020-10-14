using demo1.Bots;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace demo1
{
    public class Startup
    {
        // Inject the IHostEnvironment

        public Startup(IHostEnvironment env)
        {
            ContentRootPath = env.ContentRootPath;
        }

        public string ContentRootPath { get; private set; }

        public IConfiguration Configuration { get; }

        // Track the root path so that it can be used to set up the app config
        public void ConfigureServices(IServiceCollection services)
        {
            //Set up the service config

            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            services.AddSingleton(configuration);

            // Add your SimpleBot to your app
            services.AddBot<SimpleBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            // Tell your application to use Bot Framework
            app.UseBotFramework();
        }
    }
}
