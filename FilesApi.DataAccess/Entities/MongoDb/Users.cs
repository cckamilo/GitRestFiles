using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FilesApi.DataAccess.Entities.MongoDb
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string role { get; set; }

    }
}
