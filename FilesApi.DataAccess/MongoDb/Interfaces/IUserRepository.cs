using System;
using FilesApi.DataAccess.MongoDb.Base;
using FilesApi.DataAccess.MongoDb.Entities;

namespace FilesApi.DataAccess.MongoDb.Interfaces
{
    public interface IUserRepository : IMongoDbRepository<Users>
    {


    }
}
