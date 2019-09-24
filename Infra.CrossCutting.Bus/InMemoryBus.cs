using Domain.Core.Bus;
using Domain.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public async Task<bool> SendCommand<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
