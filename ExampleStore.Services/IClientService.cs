using ExampleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStore.Services
{
    public interface IClientService
    {
        void CreateClient(Client domain);
        void DeleteClient(int ClientId);
        public List<Client> GetAllClients();
        public Client GetClientById(int ClientId);
        public Client GetClientByFilter(string filter);
        public void UpdateClient(Client domain);
    }
}
