using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SrvCornet.Core;
using SrvCornet.Dal.Mongo;

namespace AuctionHub.Data.Mongo
{
    public class MongoDbFactory : IMongoDbFactory
    {
        public MongoDbFactory()
        {
        }
        public IMongoDatabase GetMongoDatabase<T>()
        {
            IMongoDatabase database = null;
            var mongoDbLocation = AppSettings.Get<MongoDbLocation>("Databases:Mongo:Main");
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoDbLocation.ConnectionString));
                if (mongoDbLocation.Ssl)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                database = mongoClient.GetDatabase(mongoDbLocation.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access MongoDb server.", ex);
            }
            return database;
        }
    }
}
