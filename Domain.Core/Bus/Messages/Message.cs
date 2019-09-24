using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core.Models;
using MediatR;

namespace Domain.Core.Bus
{
    public class Message : IRequest<bool>
    {   
        protected Message()
        {
            Type = GetType().Name;
            Id = Guid.NewGuid();
            Date = DateTimeOffset.Now;
        }

        public Guid Id { get; protected set; }
        public string Type { get; protected set; }
        public DateTimeOffset Date { get; protected set; }
    }
}
