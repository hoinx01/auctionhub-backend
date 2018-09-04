using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.AuctionPackageAggregate.Domain
{
    public class ProductDomain : Subdomain
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
    }
}
