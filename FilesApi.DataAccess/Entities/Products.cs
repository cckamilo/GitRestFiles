using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.DataAccess.Entities
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Name { get; set; }
        public string filePath { get; set; }
        public string date { get; set; }
        public int price { get; set; }

    }
}
