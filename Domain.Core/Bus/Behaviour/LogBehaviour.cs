using Domain.Core.Bus.Messages.Events;
using Domain.Core.Identity;
using Domain.Core.Persistence;
using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Bus.Behaviour
{
    public class LogBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command
    {
        private readonly IMediatorHandler _bus;
        private readonly IEventStore _eventStore;
        private readonly ILoggedUser _user;
        private readonly Stopwatch _sw;

        public LogBehaviour(IMediatorHandler bus, IEventStore eventStore)
        {
            _bus = bus;
            _eventStore = eventStore;
            //TODO
            _user = null;
            _sw = new Stopwatch();            
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _sw.Start();


            try
            {
                TResponse response = await next();
                //TODO
                //_eventStore.Save();
                return response;
            }catch(Exception ex)
            {
                await _bus.RaiseEvent(new DomainErrorNotification("unknownError", ex.Message, ex.ToString()));
                //TODO
                //_eventStore.Save();
                return default(TResponse);
            }
        }
    }
}
