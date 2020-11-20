using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.Utilities.Response
{
    public class SftpResponse
    {
        public string path { get; set; }
        public bool result { get; set; }
        public string fileName { get; set; }
        public string message { get; set; }

    }
}
