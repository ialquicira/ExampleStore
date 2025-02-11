using ExampleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStore.Services
{
    public interface IStoreService
    {
        void CreateStore(Store domain);
        void DeleteStore(int StoreId);
        public List<Store> GetAllStores();
        public Store GetStoreById(int StoreId);
        public Store GetStoreByFilter(string filter);
        public void UpdateStore(Store domain);
    }
}
