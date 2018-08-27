using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SrvCornet.Dal.Mongo
{
    public interface IMongoDao<T> where T : BaseMongoDto
    {
        Task<T> GetByIdAsync(ObjectId id);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T dto);
        Task AddAsync(IEnumerable<T> listDto);
        Task<T> UpdateAsync(T dto);
        Task<T> DeleteAsync(T dto);
    }
}
