using FilesApi.DataAccess.MongoDb.Base;
using FilesApi.DataAccess.MongoDb.Configuration;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.DataAccess.MongoDb.Interfaces;
using System;
namespace FilesApi.DataAccess.MongoDb.Repository
{
    public class UserRepository: MongoDbRepository<Users>, IUserRepository
    {
        public UserRepository(IStoreDataBaseSettings settings)  : base(settings)
        {
        }
    }
}
