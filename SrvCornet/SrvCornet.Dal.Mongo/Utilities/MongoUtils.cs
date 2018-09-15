using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Dal.Mongo.Utilities
{
    public class MongoUtils
    {
        public static string GenerateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

    }
}
