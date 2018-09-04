using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.SeedWork
{
    public abstract class AggregateRoot : Entity
    {
        public List<IntegratedEvent> Events { get; set; }
        public void AddIntegratedEvent(string name, string target, string data)
        {
            Events = Events ?? new List<IntegratedEvent>();
            var integratedEvent = new IntegratedEvent()
            {
                Name = name,
                Target = target,
                Data = data
            };
            Events.Add(integratedEvent);
        }
    }
}
