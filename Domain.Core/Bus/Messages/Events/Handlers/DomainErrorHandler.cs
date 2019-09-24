using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Domain.Core.Bus.Messages.Events.Handlers
{
    public class DomainErrorHandler : INotificationHandler<DomainErrorNotification>
    {
        private List<DomainErrorNotification> _errorNotifications;

        public DomainErrorHandler()
        {
            _errorNotifications = new List<DomainErrorNotification>();
        }

        public Task Handle(DomainErrorNotification message, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.CompletedTask;

            _errorNotifications.Add(message);

            return Task.CompletedTask;
        }

        public virtual IEnumerable<DomainErrorNotification> GetNotifications()
        {
            return _errorNotifications;
        }

        public virtual IEnumerable<DomainErrorNotification> GetNotifications(string key)
        {
            return _errorNotifications.Where(x => x.Key == key);
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public virtual bool HasNotifications(string key)
        {
            return GetNotifications(key).Any();
        }

        public virtual List<DomainErrorNotification> GetAndClearNotifications()
        {
            List<DomainErrorNotification> returnList = new List<DomainErrorNotification>(GetNotifications());
            _errorNotifications = new List<DomainErrorNotification>();

            return returnList;
        }

        public virtual List<DomainErrorNotification> GetAndClearNotifications(string key)
        {
            List<DomainErrorNotification> returnList = new List<DomainErrorNotification>(GetNotifications(key));
            _errorNotifications.RemoveAll(x => GetNotifications(key).Contains(x));

            return returnList;
        }

        public void ClearNotifications()
        {
            _errorNotifications = new List<DomainErrorNotification>();
        }

        public void Dispose()
        {
            ClearNotifications();
        }
    }
}
