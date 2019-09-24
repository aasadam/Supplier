using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Models;

namespace Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
