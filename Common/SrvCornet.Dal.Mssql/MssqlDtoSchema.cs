using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Dal.Mssql
{
    public class MssqlDtoSchema
    {
        public static Dictionary<Type, MssqlDtoSchema> SCHEMA_COLLECTION = new Dictionary<Type, MssqlDtoSchema>();
        public static MssqlDtoSchema GetSchema(Type type)
        {
            if (!SCHEMA_COLLECTION.ContainsKey(type))
            {
                SCHEMA_COLLECTION.Add(type, MssqlDtoSchemaUtils.GetDtoSchema(type));
            }
            return SCHEMA_COLLECTION[type];
        }
        public string TableName { get; set; }
        public Dictionary<string, Type> PropertyTypeMap { get; set; }
        public List<string> Keys { get; set; }
        public bool AllowInsertKey { get; set; }
    }
}
