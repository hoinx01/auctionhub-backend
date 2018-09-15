using Modules.Product.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Dto
{
    public class VariantDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public List<VariantOptionDto> OptionValues { get; set; }
        public List<ImageDto> Images { get; set; }
        public string Status { get; set; }

    }
}
