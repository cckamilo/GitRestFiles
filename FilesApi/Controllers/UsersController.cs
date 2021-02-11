using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/users")]
    [ApiController]
    [Authorize(Roles ="admin")]
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, Users user)
        {
            try
            {

                if (user != null)
                {
                    user.id = id;
                    var response = await iUserBll.Update(user);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await iUserBll.GetById(id);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await iUserBll.DeleteById(id);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }






    }
}
