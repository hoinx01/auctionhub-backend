using Domain.Model.Dto;
using Modules.Product.Domain.Model.Domain.ConstAndEnums;
using Modules.Product.Domain.Model.Dto;
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
        public List<ImageDomain> Images { get; set; }
        public string Status { get; set; }

        public VariantDomain(VariantDto dto)
        {
            Code = dto.Code;
            Name = dto.Name;
            Description = dto.Description;
            BasePrice = dto.BasePrice;
            Status = VariantStatus.ACTIVE;
        }
        private void SetImages(List<ImageDto> imageDtos)
        {

        }
    }
}
