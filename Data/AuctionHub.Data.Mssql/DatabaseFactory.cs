using Microsoft.EntityFrameworkCore;
using SrvCornet.Core;
using SrvCornet.Dal.Mssql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHub.Data.Mssql
{
    public class DatabaseFactory : IDbFactory
    {
        public string GetConnectionString<T>()
        {
            return AppSettings.GetString("Databases:Mssql:Main:ConnectionString");
        }
        public DbContext GetDbContext<T>()
        {
            return new MainDbContext();
        }
    }
}
