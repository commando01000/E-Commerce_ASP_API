﻿using Microsoft.AspNetCore.Mvc;
using Store.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<ProductDetailsDto>> Get()
        {
            return await this._productService.GetAllProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ProductDetailsDto> Get(int id)
        {
            return await this._productService.GetProductById(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}