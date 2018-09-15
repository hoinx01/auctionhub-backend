using SrvCornet.Dal.Mongo.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHub.Common.Utilities
{
    public class IdGenerator
    {
        public static string GenerateId()
        {
            return MongoUtils.GenerateId();
        }
    }
}

