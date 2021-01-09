using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.Utilities.Response
{
    public class SftpResponse
    {
        public bool result { get; set; }
        public string urlMain { get; set; }
        public List<string> files { get; set; }

    }
}
