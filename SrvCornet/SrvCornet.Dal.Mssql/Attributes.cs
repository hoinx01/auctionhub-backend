using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Dal.Mssql
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowInsertKeyAttribute : Attribute
    {
        public bool Value { get; set; }
    }
}
