using ExampleStore.Models;
using ExampleStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleStore.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _customerService;
        public ProductController(IProductService customerService)
        {
            _customerService = customerService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAll")]
        public IList<Product> GetAll()
        {
            return _customerService.GetAllProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet]
        [Route("GetById")]
        public Product Get(int id)
        {
            return _customerService.GetProductById(id);
        }

        [HttpGet]
        [Route("GetByFilter")]
        public Product Get(string filter)
        {
            return _customerService.GetProductByFilter(filter);
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("Create")]
        public void Post([FromBody] Product model)
        {
            _customerService.CreateProduct(model);
        }
        [HttpPut]
        [Route("Update")]
        public void Put([FromBody] Product model)
        {
            _customerService.UpdateProduct(model);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            _customerService.DeleteProduct(id);
        }
    }
}
