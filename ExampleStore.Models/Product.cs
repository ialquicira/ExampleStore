using System.Diagnostics;

namespace ExampleStore.Models
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool? Stock { get; set; }

    }
}
