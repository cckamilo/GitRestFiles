using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilesApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthenticationManager authenticationManager;

        public LoginController(IAuthenticationManager _authenticationManager)
        {
            this.authenticationManager = _authenticationManager;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Post([FromBody] Users user)
        {

            var token =  authenticationManager.Authenticate(user.userName, user.password);

            if (token == null)
                return Unauthorized();
            return Ok(token);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
