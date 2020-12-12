using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Services;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilesApi.Controllers
{

    [Produces("application/json")]
    [Route("api/v1/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FilesSftp filesSftp;
        private ServiceResponse response;

        public FilesController(FilesSftp _filesSftp, ServiceResponse _response)
        {
            filesSftp = _filesSftp;
            response = _response;
        }

        // GET: api/<FilesController>                                                                  l
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FilesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FilesController>
        [HttpPost("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            response = await filesSftp.UploadFiles(file);      
            return Ok(response);
        }

        // GET api/<FilesController>/5
        [HttpGet("file")]
        public async Task<IActionResult> GetFiles()
        {
            response = await filesSftp.ListFiles("");
            return Ok(response);
        }

        // PUT api/<FilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
