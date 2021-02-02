

using FilesApi.DataAccess.MongoDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.MongoDb.Base
{
    public interface IMongoDbRepository<TEntity> where TEntity :  EntityBase
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(string id);
        IList<TEntity>
        SearchForAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
    }
}
