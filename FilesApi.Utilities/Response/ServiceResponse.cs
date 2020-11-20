using FilesApi.Utilities.Response.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilesApi.Utilities.Response
{
    public class ServiceResponse
    {
        public MsgError msgError { get; set; }
        public Body body { get; set; }
    }
}
