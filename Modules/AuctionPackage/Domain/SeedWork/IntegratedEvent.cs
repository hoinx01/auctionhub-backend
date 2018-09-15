using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.SeedWork
{
    public class IntegratedEvent
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public string Data { get; set; }

        public IntegratedEvent()
        {

        }
        public IntegratedEvent(string name, string target, string data)
        {
            Name = name;
            Target = target;
            Data = data;
        }
    }
}
