using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.SeedWork
{
    public abstract class Subdomain : Entity
    {
        protected AggregateRoot Root { get; set; }
    }
}
