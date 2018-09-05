using Modules.Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class VariantDomain : Subdomain
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public List<VariantOptionDomain> OptionValues { get; set; }
        public string Status { get; set; }
    }
}
