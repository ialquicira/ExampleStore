using ExampleStore.Models;
using ExampleStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleStore.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _customerService;
        public ClientController(IClientService customerService)
        {
            _customerService = customerService;
        }
        // GET: api/<ClientController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Client>> GetAll()
        {
            return await Task.Run(() => _customerService.GetAllClients());
        }

        // GET api/<ClientController>/5
        [HttpGet]
        [Route("GetById")]
        public Client Get(int id)
        {
            return _customerService.GetClientById(id);
        }

        [HttpGet]
        [Route("GetFilter")]
        public Client Get(string filter)
        {
            return _customerService.GetClientByFilter(filter);
        }

        // POST api/<ClientController>
        [HttpPost]
        [Route("Create")]
        public void Post([FromBody] Client model)
        {
            _customerService.CreateClient(model);
        }

        [HttpPut]
        [Route("Update")]
        public void Put([FromBody] Client model)
        {
            _customerService.UpdateClient(model);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            _customerService.DeleteClient(id);
        }
    }
}
