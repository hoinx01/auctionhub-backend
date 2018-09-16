using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.SeedWork
{
    public abstract class Subdomain<T> : Entity where T : AggregateRoot
    {
        protected T Root { get; set; }
    }
}
