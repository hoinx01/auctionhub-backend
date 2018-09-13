using AuctionHub.Data.Mongo.Dao.Interfaces;
using AuctionHub.Data.Mongo.Dto;
using MongoDB.Driver;
using SrvCornet.Dal.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHub.Data.Mongo.Dao
{
    public class MdAccountDao : BaseMongoDao<MdAccount>, IMdAccountDao
    {
        public MdAccountDao(IMongoDbFactory databaseFactory, string collectionName) : base(databaseFactory, "Accounts")
        {
            Console.WriteLine("haha");

        }
        public async Task<List<MdAccount>> Filter(AccountFilter filterModel)
        {
            var filterBuilder = Builders<MdAccount>.Filter;

            var criterias = new List<FilterDefinition<MdAccount>>();
            if (string.IsNullOrWhiteSpace(filterModel.Email))
                criterias.Add(Builders<MdAccount>.Filter.Eq("Email", filterModel.Email));
            if (string.IsNullOrWhiteSpace(filterModel.PhoneNumber))
                criterias.Add(Builders<MdAccount>.Filter.Eq("PhoneNumber", filterModel.PhoneNumber));
            if (string.IsNullOrWhiteSpace(filterModel.UserName))
                criterias.Add(Builders<MdAccount>.Filter.Eq("UserName", filterModel.UserName));

            var filter = filterBuilder.And(criterias);

            var filterResult = await (await Collection.FindAsync<MdAccount>(filter)).ToListAsync();
            return filterResult;
        }

        public async Task<List<MdAccount>> GetForLogin(string loginName)
        {
            var filterBuilder = Builders<MdAccount>.Filter;

            var orCriterias = new List<FilterDefinition<MdAccount>>();
            orCriterias.Add(Builders<MdAccount>.Filter.Eq("Email", loginName));
            orCriterias.Add(Builders<MdAccount>.Filter.Eq("PhoneNumber", loginName));
            orCriterias.Add(Builders<MdAccount>.Filter.Eq("UserName", loginName));


            var filter = filterBuilder.Or(orCriterias);
            var filterResult = await (await Collection.FindAsync<MdAccount>(filter)).ToListAsync();
            return filterResult;
        }
    }
}
