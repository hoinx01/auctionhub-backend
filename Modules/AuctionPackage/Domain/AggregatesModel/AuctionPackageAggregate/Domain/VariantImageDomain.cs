using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.AuctionPackageAggregate.Domain
{
    public class VariantImageDomain : ValueObject
    {
        public string Source { get; set; }
        public string CdnPath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
