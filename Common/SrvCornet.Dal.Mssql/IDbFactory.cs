using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SrvCornet.Dal.Mssql
{
    public interface IDbFactory
    {
        string GetConnectionString<T>();
        DbContext GetDbContext<T>();
    }
}
