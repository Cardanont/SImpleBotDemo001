using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo1.Middleware
{
    public class SimpleMiddleware1 : IMiddleware
    {
        public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
        {
            await context.SendActivityAsync($"[SimpleMiddleware1] (context)/OnTurn/Before");
        }
    }
}
