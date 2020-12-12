using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.DataAccess.Data.Configuration
{
    public class StoreDataBaseSettings : IStoreDataBaseSettings
    {
        public string collectionName { get; set; }
        public string connectionString { get; set; }
        public string dataBaseName { get; set; }                                            

    }

    public interface IStoreDataBaseSettings
    {
        string collectionName { get; set; }
        string connectionString { get; set; }
        string dataBaseName { get; set; }

    }
}
