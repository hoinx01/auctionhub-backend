using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace SrvCornet.Dal.Mongo
{
    public interface IMongoDbFactory
    {
        IMongoDatabase GetMongoDatabase<T>();
    }
}
