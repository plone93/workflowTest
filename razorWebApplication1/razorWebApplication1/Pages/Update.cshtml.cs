using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;
using razorWebApplication1.Service;
using razorWebApplication1.Utils;
using System.Configuration;
using System.Xml.Linq;

namespace razorWebApplication1.Pages
{
    public class UpdateModel(IConfiguration configuration) : PageModel
    {
        private readonly string _connectionString = configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("OracleConnection ÇÃê⁄ë±ï∂éöóÒÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        [BindProperty]
        public required Product product { get; set; }

        // GET: /Delete?id=1
        public void OnGet(int id)
        {
            using var conn = new OracleConnection(_connectionString);
            conn.Open();

            using var cmd = new OracleCommand("SELECT ID, NAME, PRICE FROM PRODUCT WHERE ID = :id", conn);
            cmd.Parameters.Add("id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };
            }
        }

        // POST: Update
        public IActionResult OnPost(Product product, string action)
        {
            if (action == "back")
            {
                return RedirectToPage("/Select");
            }
            else if (action == "save")
            {
                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("UPDATE PRODUCT SET NAME = :name, PRICE = :price WHERE ID = :id", conn);
                cmd.Parameters.Add("name", product.Name);
                cmd.Parameters.Add("price", product.Price);
                cmd.Parameters.Add("id", product.Id);

                cmd.ExecuteNonQuery();

            }
            else if (action == "delete")
            {
                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("DELETE FROM PRODUCT WHERE ID = :id", conn);
                cmd.Parameters.Add("id", product.Id);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("Select");
        }
    }
}
