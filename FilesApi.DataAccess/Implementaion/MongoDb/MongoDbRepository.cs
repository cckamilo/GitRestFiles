using FilesApi.DataAccess.Data.Configuration;
using FilesApi.DataAccess.Interfaces.MongoDb;
using FilesApi.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Implementaion.MongoDb
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IMongoCollection<TEntity> _usersCollection;

        public MongoDbRepository(IStoreDataBaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.connectionString);
            var database = mdbClient.GetDatabase(settings.dataBaseName);
            _usersCollection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _usersCollection.InsertOneAsync(entity);
            return entity;

        }

        public IList<TEntity> SearchForAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
