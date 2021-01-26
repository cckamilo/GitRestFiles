using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.Entities.MongoDb;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserBll iUserBll;
        private ServiceResponse response;

        public UsersController(IUserBll _iUserBll, ServiceResponse _response)
        {
            this.iUserBll = _iUserBll;
            this.response = _response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            response = await iUserBll.Get();
            return Ok(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(Users user)
        {
            try
            {
                if (user != null)
                {
                    response = await iUserBll.Insert(user);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }







    }
}
