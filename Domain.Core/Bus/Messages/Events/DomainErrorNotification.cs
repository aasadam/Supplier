using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Bus.Messages.Events
{
    public class DomainErrorNotification : Event
    {
        public DomainErrorNotification(string key, string value, string details = null)
        {
            Key = key;
            Value = value;
            Details = details;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public string Details { get; set; }
    }
}
