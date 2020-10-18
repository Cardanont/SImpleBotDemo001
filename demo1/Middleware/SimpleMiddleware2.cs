﻿using demo1.Bots;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace demo1.Middleware
{
    public class SimpleMiddleware2 : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default)
        {
            await context.SendActivityAsync($"[SimpleMiddleware2] {context.Activity.Type}/OnTurn/Before");

            if(context.Activity.Type == ActivityTypes.Message && context.Activity.Text == "secret password")
            {
                // Calling next is totally optional. if the middleware does not call next then the
                // next middleware in the pipeline will not be called, AND the vot will not receive the message.
                //
                // in this instance, we are only handling the message to downstream bots if the user says "secret password"
                await next();
            }

            await context.SendActivityAsync($"[SimpleMiddleware2] {context.Activity.Type}/OnTurnAsync/After");
        }
    }
}
