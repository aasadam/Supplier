using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Bus.Messages.Events;
using Domain.Core.Bus.Messages.Events.Handlers;
using Domain.Core.Models;
using MediatR;

namespace Domain.Core.Bus.Behaviour
{
    //TODO Translate
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command
    {
        private readonly IMediatorHandler _bus;

        public ValidationBehavior(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default(TResponse);
            }

            return await next();
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.GetErrors())
            {
                _bus.RaiseEvent(new DomainErrorNotification($"Validation error on {error.PropertyName}", error.Message));
            }           
        }
    }
}
