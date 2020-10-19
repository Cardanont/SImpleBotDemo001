using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace demo1.Middleware
{
    public class SimpleMiddleware1 : IMiddleware
    {
        //public async Task OnTurn(ITurnContext context, NextDelegate next)
        //{
            
        //}

        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default)
        {
            await context.SendActivityAsync($"[SimpleMiddleware1] {context.Activity.Type}/OnTurnAsync/Before");

            await next(cancellationToken);

            await context.SendActivityAsync($"[SimpleMiddleware1] {context.Activity.Type}/OnTurnAsync/After");
        }
    }
}
