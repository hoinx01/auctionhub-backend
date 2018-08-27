using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHub.Data.Mssql.Dto
{
    public abstract class BaseFilter
    {
    }
    public abstract class BasePagingFilter : BaseFilter
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
