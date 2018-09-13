using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Dapper;
using System.Linq;
using System.Reflection;
using SrvCornet.Utils;

namespace SrvCornet.Dal.Mssql
{
    public static class MssqlDtoSchemaUtils
    {
        public static MssqlDtoSchema GetDtoSchema(Type type)
        {
            string tableName = GetTableName(type);
            var propertyTypeMap = ObjectUtils.GetPropertyTypeMap(type);
            var keys = GetKeyNames(type);
            bool allowInsertKey = GetAllowInsertKeyValue(type);
            return new MssqlDtoSchema()
            {
                TableName = tableName,
                PropertyTypeMap = propertyTypeMap,
                Keys = keys,
                AllowInsertKey = allowInsertKey
            };
        }
        public static bool GetAllowInsertKeyValue(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(AllowInsertKeyAttribute), false);
            if (attributes != null && attributes.Length >= 1)
                return ((AllowInsertKeyAttribute)attributes.GetValue(0)).Value;
            else
                return false;
        }
        public static string GetTableName(Type type)
        {           
            var tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttributes != null && tableAttributes.Length == 1)
                return ((TableAttribute) tableAttributes.GetValue(0)).Name;

            string tableName = string.Concat(type.Name, "s");
            return tableName;
        }

        public static List<string> GetKeyNames(Type type)
        {
            var keys = new List<string>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var tableAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);
                if (tableAttributes.Count() > 0)
                    keys.Add(property.Name);
            }
            return keys;
        }
        public static List<PropertyInfo> GetKeyProperties(Type type)
        {
            var keys = new List<PropertyInfo>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var tableAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);
                if (tableAttributes.Count() > 0)
                    keys.Add(property);
            }
            return keys;
        }

    }
    
}
