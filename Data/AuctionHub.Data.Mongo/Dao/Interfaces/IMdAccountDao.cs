using AuctionHub.Data.Mongo.Dto;
using SrvCornet.Dal.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHub.Data.Mongo.Dao.Interfaces
{
    public interface IMdAccountDao : IMongoDao<MdAccount>
    {
        Task<List<MdAccount>> Filter(AccountFilter filterModel);
        Task<List<MdAccount>> GetForLogin(string loginName);
    }
}
