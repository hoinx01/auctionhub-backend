using AuctionHub.Data.Mssql.Dao.Interfaces;
using AuctionHub.Data.Mssql.Dto;
using SrvCornet.Dal.Mssql;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AuctionHub.Data.Mssql.Dao
{
    public class AccountDao : BaseMssqlDao<Account>, IAccountDao
    {
        public AccountDao(IDbFactory factory) : base(factory)
        {

        }
        public async Task<List<Account>> Filter(AccountFilter filterModel)
        {
            var accounts = await QuerySPAsync<Account>("Accounts_Filter", filterModel);
            return accounts.ToList();
        }
        public async Task<List<Account>> GetForLogin(string loginName)
        {
            var accounts = await QuerySPAsync<Account>("Accounts_GetForLogin", new { LoginName = loginName });
            return accounts.ToList();
        }
    }
}
