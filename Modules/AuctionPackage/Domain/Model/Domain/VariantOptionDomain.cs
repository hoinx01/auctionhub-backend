using Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class VariantOptionDomain
    {
        public string OptionName { get; set; }
        public string Value { get; set; }

        public VariantOptionDomain(VariantOptionDto dto)
        {
            OptionName = dto.OptionName;
            Value = dto.Value;
        }
    }
}
