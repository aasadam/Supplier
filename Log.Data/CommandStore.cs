using Domain.Core.Persistence;
using System;

namespace Log.Data
{
    public class EventStore : IEventStore
    {
        public void Save()
        {
            //TODO: Save CommandHistory
            throw new NotImplementedException();
        }
    }
}
