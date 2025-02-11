using ExampleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStore.Services
{
    public interface IProductService
    {
        void CreateProduct(Product domain);
        void DeleteProduct(int ProductId);
        public List<Product> GetAllProducts();
        public Product GetProductById(int ProductId);
        public Product GetProductByFilter(string filter);
        public void UpdateProduct(Product domain);
    }
}
