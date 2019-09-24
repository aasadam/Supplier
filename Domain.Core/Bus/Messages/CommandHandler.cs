using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core.Bus.Messages.Events;
using Domain.Core.Bus.Messages.Events.Handlers;
using Domain.Core.Persistence;
using MediatR;

namespace Domain.Core.Bus.Messages
{
    public class CommandHandler<TEntity> where TEntity : class
    {
        protected readonly IMediatorHandler _bus;
        protected readonly IRepository<TEntity> _repository;
        protected readonly DomainErrorHandler _notifications;

        public CommandHandler(IMediatorHandler bus, IRepository<TEntity> repository, INotificationHandler<DomainErrorNotification> notifications)
        {
            _bus = bus;
            _repository = repository;
            _notifications = (DomainErrorHandler)notifications;
        }

        public virtual bool Commit()
        {
            try
            {
                if (_notifications.HasNotifications()) return false;
                if (_repository.SaveChanges()) return true;
            }
            catch (Exception ex)
            {
                _repository.Rollback();
                _bus.RaiseEvent(new DomainErrorNotification("Commit", "We had a problem during saving your data.", ex.ToString()));
                return false;
            }

            _bus.RaiseEvent(new DomainErrorNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
