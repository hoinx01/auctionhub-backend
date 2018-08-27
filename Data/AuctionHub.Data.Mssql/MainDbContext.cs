using System;
using System.Collections.Generic;
using System.Text;
using AuctionHub.Data.Mssql.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SrvCornet.Core;

namespace AuctionHub.Data.Mssql
{
    public class MainDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.GetString("Databases:Mssql:Main:ConnectionString"));
            }
        }
        public DbSet<Account> Accounts { get; set; }
    }
}
