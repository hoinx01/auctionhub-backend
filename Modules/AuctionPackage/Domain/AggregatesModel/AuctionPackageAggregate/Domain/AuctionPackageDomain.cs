using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.AuctionPackageAggregate.Domain
{
    public class AuctionPackageDomain : AggregateRoot
    {
        public int Id { get; set; }
        public string Code { get; set; }
        
        public DateTime PublishedAt { get; set; }
        public long Duration { get; set; }
        public DateTime? ExpectedClosingTime { get; set; }
        public int StatusId { get; set; }
    }
}
