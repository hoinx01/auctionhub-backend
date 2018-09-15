using Modules.Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class ProductOptionDomain : Subdomain
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
