using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.Models.Models
{
    public class Product
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string filePath { get; set; }
        public List<string> files { get; set; }
        public string date { get; set; }
        public int price { get; set; }

    }
}
