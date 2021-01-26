using FilesApi.DataAccess.Entities.MongoDb;
using FilesApi.DataAccess.Interfaces.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using FilesApi.DataAccess.Data.Configuration;

namespace FilesApi.DataAccess.Implementaion.MongoDb
{
    public class UserDb : IUserRepository
    {
        private readonly IMongoCollection<Users> _usersCollection;

        public UserDb(IStoreDataBaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.connectionString);
            var database = mdbClient.GetDatabase(settings.dataBaseName);
            _usersCollection = database.GetCollection<Users>("Users");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteIdAsync(string id)
        {
            try
            {
                var result = await _usersCollection.DeleteOneAsync(item => item.id == id);
                if (result.DeletedCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Users>> GetAsync()
        {
            try
            {
                var result = await _usersCollection.Find<Users>(item => item.id != null).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> GetIdAsync(string id)
        {
            try
            {
                var result = await _usersCollection.Find<Users>(item => item.id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Users> InsertAsync(Users user)
        {
            try
            {
                await _usersCollection.InsertOneAsync(user);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(string id, Users user)
        {
            try
            {
                var update = Builders<Users>.Update.Set(a => a.userName, user.userName)
                  .Set(a => a.password, user.password)
                  .Set(a => a.role, user.role);

                var result = await _usersCollection.UpdateOneAsync(item => item.id == id, update);
                return result.IsModifiedCountAvailable;
            }
            catch (Exception)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
