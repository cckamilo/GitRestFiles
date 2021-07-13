using System;
using System.Collections.Generic;

namespace FilesApi.Models.Entities
{
    public class Products
    {
        public string id { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string urlMain { get; set; }
        public List<string> filesName { get; set; }
        public List<string> files { get; set; }
        public string date { get; set; }
    }
}
