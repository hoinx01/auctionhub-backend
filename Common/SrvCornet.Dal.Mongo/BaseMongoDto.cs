using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SrvCornet.Dal.Mongo
{
    public class BaseMongoDto
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
