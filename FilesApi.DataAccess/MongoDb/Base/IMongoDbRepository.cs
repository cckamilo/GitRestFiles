using FilesApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Interfaces.MongoDb
{
    public interface IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        IList<TEntity>
        SearchForAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
