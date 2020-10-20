using demo1.Bots;
using demo1.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
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
            // Create the stroage we'll be using for user and Conversation state.
            // (Memory is great for testing purposes - examples of implementing storage with
            var storage = new MemoryStorage();

            // Create the USer state passing in the sotrage layer.
            var userState = new UserState(storage);
            services.AddSingleton(userState);



            // Create the Conversation state passing in the storage layer
            var conversationState = new ConversationState(storage);

            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            services.AddSingleton(configuration);
            services.AddSingleton(conversationState);

            

            

            // Add your SimpleBot to your app
            services.AddBot<SimpleBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

                //options.Middleware.Add(new SimpleMiddleware1());
                //options.Middleware.Add(new SimpleMiddleware2());
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
