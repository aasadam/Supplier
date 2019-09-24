using Domain.Core.Bus;
using Domain.Core.Bus.Messages.Events;
using Domain.Core.Bus.Messages.Events.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupplierReg.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierReg.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected readonly DomainErrorHandler _notifications;
        private readonly IMediatorHandler _bus;

        public CustomControllerBase(INotificationHandler<DomainErrorNotification> notifications,
                                    IMediatorHandler bus)
        {
            _notifications = (DomainErrorHandler)notifications;
            _bus = bus;
        }

        protected IEnumerable<ApiErrorDTO> GetComandErrors()
        {
            foreach (var error in _notifications.GetNotifications())
            {
                yield return new ApiErrorDTO
                {
                    Key = error.Key,
                    Detail = error.Details,
                    Message = error.Value
                };
            }
        }

        protected async Task<bool> SendCommand(Command command)
        {
            return await _bus.SendCommand(command);
        } 
    }
}
