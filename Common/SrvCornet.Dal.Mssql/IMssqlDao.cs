using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SrvCornet.Dal.Mssql
{
    public interface IMssqlDao<T> where T : BaseMssqlDto
    {
        Task<IEnumerable<T>> FindAsync(object condition);
        /*
         * Hàm này không trả ra Id sau khi insert, phải sinh trước id;
         * Nếu muốn add các record có Id tự sinh thì tự control
         */
        Task<int> InsertAsync(T entity);
        Task<T> InsertAsync_EF(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);

        Task<IEnumerable<V>> QueryAsync<V>(string query, object param = null);
        Task<IEnumerable<V>> QuerySPAsync<V>(string storeProcedureName, object param = null);
        Task<int> ExecuteAsync(string query, object param = null);
        Task<int> ExecuteSPAsync(string storeProcedureName, object param = null);
        Task<long> GetSequenceValueAsync(string sequenceName);

        IEnumerable<T> Find(object condition);
        /*
         * Hàm này không trả ra Id sau khi insert, phải sinh trước id;
         * Nếu muốn add các record có Id tự sinh thì tự control
         */
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Put(T entity);

        IEnumerable<V> Query<V>(string query, object param = null);
        IEnumerable<V> QuerySP<V>(string storeProcedureName, object param = null);
        int Execute(string query, object param = null);
        int ExecuteSP(string storeProcedureName, object param = null);
        long GetSequenceValue(string sequenceName);
    }
}
