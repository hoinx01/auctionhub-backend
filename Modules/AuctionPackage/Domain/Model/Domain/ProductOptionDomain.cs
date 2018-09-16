using Modules.Product.Domain.Model.Domain.ConstAndEnums;
using Modules.Product.Domain.Model.Dto;
using Modules.Product.Domain.SeedWork;
using SrvCornet.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class ProductOptionDomain : Subdomain<ProductDomain>
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public ProductOptionDomain(ProductDomain root, ProductOptionDto dto)
        {
            Root = root;
            Id = dto.Id;
            Name = dto.Name;
            Status = dto.Status ?? ProductOptionStatus.ACTIVE;
        }
        public void Update(ProductOptionDto dto)
        {
            SetName(dto.Name);
            SetStatus(dto.Status);
        }
        private void SetName(string name)
        {
            if (StringUtils.Equals(Name, name))
                return;
            Name = name;
            Modify();
        }
        private void SetStatus(string status)
        {
            if (StringUtils.Equals(status, Status))
                return;
            Status = status;
            Modify();
        }
    }
}
