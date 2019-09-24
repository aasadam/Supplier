using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Domain.Core.Bus
{
    public class Event : Message, INotification
    {
        
    }
}
