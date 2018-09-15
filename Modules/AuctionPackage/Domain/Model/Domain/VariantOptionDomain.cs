using Domain.Model.Dto;
using SrvCornet.Utils;
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
        public void Update(VariantOptionDto dto)
        {

        }
        public void SetValue(string value)
        {
            if (StringUtils.Equals(value, Value))
                return;
            Value = value;
            

        }
    }
}
