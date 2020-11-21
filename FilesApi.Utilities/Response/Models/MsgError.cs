using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.Utilities.Response.Models
{
    public class MsgError
    {
        public Error error { get; set; }

        public class Error
        {
            public Status status = new Status();
        }

        public class Status
        {
            public int statusCode { get; set; }
            public string severity { get; set; }
            public string statusDesc { get; set; }
            public AdditionalStatus additionalStatus { get; set; }

        }

        public class AdditionalStatus
        {
            public int serverStatusCode { get; set; }
            public string category { get; set; }
            public string statusDesc { get; set; }
        }

    }
}
