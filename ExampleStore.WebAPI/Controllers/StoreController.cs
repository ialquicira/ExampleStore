using ExampleStore.Models;
using ExampleStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleStore.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _customerService;
        public StoreController(IStoreService customerService)
        {
            _customerService = customerService;
        }
        // GET: api/<StoreController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Store>> GetAll()
        {
            return await Task.Run(() => _customerService.GetAllStores());
        }

        // GET api/<StoreController>/5
        [HttpGet]
        [Route("GetById")]
        public Store Get(int id)
        {
            return _customerService.GetStoreById(id);
        }

        [HttpGet]
        [Route("GetFilter")]
        public Store Get(string filter)
        {
            return _customerService.GetStoreByFilter(filter);
        }

        // POST api/<StoreController>
        [HttpPost]
        [Route("Create")]
        public void Post([FromBody] Store model)
        {
            _customerService.CreateStore(model);
        }

        [HttpPut]
        [Route("Update")]
        public void Put([FromBody] Store model)
        {
            _customerService.UpdateStore(model);
        }

        // DELETE api/<StoreController>/5
        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            _customerService.DeleteStore(id);
        }
    }
}
