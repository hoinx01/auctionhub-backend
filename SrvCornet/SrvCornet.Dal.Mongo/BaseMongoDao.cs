using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace SrvCornet.Dal.Mongo
{
    public abstract class BaseMongoDao<T> : IMongoDao<T> where T : BaseMongoDto
    {
        protected IMongoDatabase Database;
        protected IMongoCollection<T> Collection { get; }
        public BaseMongoDao(IMongoDbFactory databaseFactory, string collectionName)
        {
            Database = databaseFactory.GetMongoDatabase<T>();
            Collection = Database.GetCollection<T>(collectionName);
        }
        public async Task<T> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var filterResult = await (await Collection.FindAsync(filter)).ToListAsync();
            return filterResult.FirstOrDefault();
        }
        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var filterResult = await (await Collection.FindAsync(filter)).ToListAsync();
            return filterResult.FirstOrDefault();
        }
        public async Task AddAsync(T dto)
        {
            await Collection.InsertOneAsync(dto);
        }

        public async Task AddAsync(IEnumerable<T> listDto)
        {
            await Collection.InsertManyAsync(listDto);
        }
        public async Task<T> UpdateAsync(T dto)
        {
            var filter = Builders<T>.Filter.Eq("_id", dto.Id);
            return await Collection.FindOneAndReplaceAsync(filter, dto);
        }
        public async Task<T> DeleteAsync(T dto)
        {
            var filter = Builders<T>.Filter.Eq("_id", dto.Id);
            return await Collection.FindOneAndDeleteAsync(filter);
        }
    }
}
