using System;
using FilesApi.DataAccess.Entities.MongoDb;

namespace FilesApi.DataAccess.Interfaces.MongoDb
{
    public interface IRepositoryUsers : IMongoDbRepository<Users>
    {


    }
}
