using Modules.Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class ProductDomain : AggregateRoot
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public List<ProductOptionDomain> Options { get; set; }
        public List<VariantDomain> Variants { get; set; }
        public string Status { get; set; }

        public ProductDomain()
        {

        }
        public void AddVariant()
        {

        }
        
    }
}
