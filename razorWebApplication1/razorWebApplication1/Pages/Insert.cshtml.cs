using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorWebApplication1.Models;

namespace razorWebApplication1.Pages
{
    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //};


    public class InsertModel : PageModel
    {
        public required List<Product> Products { get; set; }
        public int ProductCount { get; set; }

        private List<Product> GetSampleProductsFromDatabase()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "notebook", Price = 1200000 },
                new Product { Id = 2, Name = "mouse", Price = 35000 },
                new Product { Id = 3, Name = "keyboard", Price = 80000 }
            };
        }

        public void OnGet()
        {
            Products = GetSampleProductsFromDatabase();
            ProductCount = Products.Count;
        }
    }
}
