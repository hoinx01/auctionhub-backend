using Modules.Product.Domain.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Dto
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public List<ProductOptionDto> Options { get; set; }
        public List<VariantDto> Variants { get; set; }
        public string Status { get; set; }
    }
}
