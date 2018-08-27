using AuctionHub.Data.Mssql.Dto;
using SrvCornet.Dal.Mssql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHub.Data.Mssql.Dao.Interfaces
{
    public interface IAccountDao : IMssqlDao<Account>
    {
        Task<List<Account>> Filter(AccountFilter filterModel);
        Task<List<Account>> GetForLogin(string loginName);
    }
    
}
