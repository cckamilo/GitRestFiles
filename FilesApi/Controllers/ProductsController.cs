using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FilesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private ServiceResponse response;
        private readonly IProductsBll iProducts;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_response"></param>
        /// <param name="_products"></param>
        public ProductsController(ServiceResponse _response, IProductsBll _iProducts)
        {
            this.response = _response;
            this.iProducts = _iProducts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("product")]
        public async Task<IActionResult> Get()
        {
            response = await iProducts.GetProducts();
            return Ok(response);
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
                var response = await iProducts.GetById(id);
                return Ok(response);
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Delete: ProductsController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await iProducts.DeleteById(id);
                return Ok(response);
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        // PUT: ProductsController/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, Products products)
        {
            try
            {
                        
                if (id != null)
                {
                    products.id = id;                   
                    var response = await iProducts.Update(id, products);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
              
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        // POST: ProductsController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> files, [FromForm] Products products)
        {
            try
            {
                if (files.Count > 0 || products != null)
                {
                    response = await iProducts.UploadFilesAsync(files, products);
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
