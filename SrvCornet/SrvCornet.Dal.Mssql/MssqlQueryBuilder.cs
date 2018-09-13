using System;
using System.Collections.Generic;
using System.Linq;

namespace SrvCornet.Dal.Mssql
{
    public class MssqlQueryBuilder
    {
        public static string BuildSelectStatement<T>(object condition) where T : BaseMssqlDto
        {
            var type = typeof(T);
            var schema = MssqlDtoSchema.GetSchema(type);

            var conditionType = condition.GetType();
            var query = "select * from {0} {1}";

            string whereClause = BuildWhereClauseFromCondition(condition);
            query = string.Format(query, schema.TableName, whereClause);
            return query;
        }
        
        private static string BuildWhereClauseFromCondition(object condition)
        {
            var conditionType = condition.GetType();
            var properties = conditionType.GetProperties();
            if (properties.Length == 0)
                return "";
            string whereClause = "where {0}";

            List<string> criterias = new List<string>();
            foreach (var property in properties)
            {
                string criteria = "{0} = @{1}";
                criteria = string.Format(criteria, property.Name, property.Name);
                criterias.Add(criteria);
            }
            whereClause = string.Format(whereClause, string.Join(" and ", criterias));
            return whereClause;
        }

        public static string BuildInsertStatement<T>() where T : BaseMssqlDto
        {
            var entityType = typeof(T);
            var entitySchema = MssqlDtoSchema.GetSchema(entityType);

            string statement = "Insert into {0}({1}) Values ({2})";

            List<string> insertableColumns = GetInsertableColumns<T>();
            if (insertableColumns.Count == 0)
                throw new Exception(string.Format("type {0} has no insertable column", entityType.Name));
            string columnsClause = string.Join(",", insertableColumns);
            string valuesClause = string.Join(",", insertableColumns.Select(s => "@" + s));

            statement = string.Format(statement, entitySchema.TableName, columnsClause, valuesClause);
            return statement;
        }
        private static List<string> GetInsertableColumns<T>() where T : BaseMssqlDto
        {
            var entityType = typeof(T);
            var entitySchema = MssqlDtoSchema.GetSchema(entityType);
            var propertieNames = entitySchema.PropertyTypeMap.Keys;

            var result = new List<string>(propertieNames);
            if (!entitySchema.AllowInsertKey)
            {
                foreach (string key in entitySchema.Keys)
                    result.Remove(key);
            }
            return result;
        }

        public static string BuildSingleUpdateStatement<T>() where T : BaseMssqlDto
        {
            var entityType = typeof(T);
            var entitySchema = MssqlDtoSchema.GetSchema(entityType);

            var query = "Update {0} {1} {2}";
            var setClause = BuildSetClauseForSingleUpdate<T>();
            var whereClause = BuildWhereClauseForSingleUpdate<T>();
            query = string.Format(query, entitySchema.TableName, setClause, whereClause);
            return query;
        }
        private static string BuildWhereClauseForSingleUpdate<T>() where T : BaseMssqlDto
        {
            var entityType = typeof(T);
            var keyProperties = MssqlDtoSchemaUtils.GetKeyProperties(entityType);

            if (keyProperties.Count == 0)
                throw new Exception("entity must have least one property as KeyAttribute");

            List<string> conditionClauses = new List<string>();
            foreach (var keyProperty in keyProperties)
            {
                string conditionClause = "{0} = @{1}";
                conditionClause = string.Format(conditionClause, keyProperty.Name, keyProperty.Name);
                conditionClauses.Add(conditionClause);
            }
            if (conditionClauses.Count > 0)
                return string.Format("where {0}", string.Join(" and ", conditionClauses));
            return "";
        }
        private static string BuildSetClauseForSingleUpdate<T>() where T : BaseMssqlDto
        {
            var entityType = typeof(T);
            var schema = MssqlDtoSchema.GetSchema(entityType);

            List<string> setColumnClauses = new List<string>();
            foreach (var propertyName in schema.PropertyTypeMap.Keys)
            {
                if (schema.Keys.Contains(propertyName))
                    continue;
                string setColumnClause = "{0} = @{1}";
                setColumnClause = string.Format(setColumnClause, propertyName, propertyName);
                setColumnClauses.Add(setColumnClause);
            }
            if (setColumnClauses.Count == 0)
                throw new Exception("Cannot update the entity only contain KeyAttribute or has no property");
            string setClause = string.Format("set {0}", string.Join(", ", setColumnClauses));
            return setClause;
        }

        public static string BuildSingleDeleteStatement<T>() where T : BaseMssqlDto
        {
            string statement = "delete from {0} {1}";
            var entityType = typeof(T);

            var schema = MssqlDtoSchema.GetSchema(entityType);

            string tableName = schema.TableName;
            string whereStatement = BuildWhereClauseForSingleUpdate<T>();

            statement = string.Format(statement, tableName, whereStatement);

            return statement;
        }

        public static string BuildSinglePutStatement<T>() where T : BaseMssqlDto
        {
            string statement =
                "if exists({0}) " +
                " begin " +
                "   {1} " +
                " end " +
                " else " +
                " begin " +
                " {2} " +
                " end ";

            var entityType = typeof(T);
            var schema = MssqlDtoSchema.GetSchema(entityType);

            string checkExistStatement = "select * from {0} {1}";
            var whereStatement = BuildWhereClauseForSingleUpdate<T>();
            checkExistStatement = string.Format(checkExistStatement, schema.TableName, whereStatement);

            string updateStatement = BuildSingleUpdateStatement<T>();
            string insertStatement = BuildInsertStatement<T>();

            statement = string.Format(statement, checkExistStatement, updateStatement, insertStatement);
            return statement;
        }
    }
}
