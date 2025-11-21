using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Data;
using razorWebApplication1.Models;
using System.Configuration;

namespace razorWebApplication1.Pages
{
    public class CreateModel(IConfiguration configuration) : PageModel
    {
        private readonly string _connectionString = configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("OracleConnection ÇÃê⁄ë±ï∂éöóÒÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public IActionResult OnPost(string Name, decimal Price)
        {
            using var conn = new OracleConnection(_connectionString);
            conn.Open();

            using var cmd = new OracleCommand("INSERT INTO PRODUCT (NAME, PRICE) VALUES (:name, :price)", conn);
            cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = Product.Name ?? string.Empty;
            cmd.Parameters.Add("price", OracleDbType.Decimal).Value = Product.Price;

            cmd.ExecuteNonQuery();

            return RedirectToPage("Select");
        }

            //private readonly AppDbContext _context;


            //public CreateModel(AppDbContext context)
            //{
            //    _context = context;
            //}

            //[BindProperty]
            //public Product Product { get; set; } = new Product();

            //public async Task<IActionResult> OnPostAsync()
            //{
            //    await _context.Database.ExecuteSqlRawAsync(
            //        "INSERT INTO PRODUCT (NAME, PRICE) VALUES ( :p0, :p1)",
            //        Product.Name, Product.Price
            //    );

            //    return RedirectToPage("Index");
            //}
        }
}
