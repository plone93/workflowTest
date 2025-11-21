using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;
using Microsoft.Extensions.Configuration;

namespace razorWebApplication1.Pages
{
    public class DeleteModel(IConfiguration configuration) : PageModel
    {
        private readonly string _connectionString = configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("OracleConnection ÇÃê⁄ë±ï∂éöóÒÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        [BindProperty]
        public required Product Product { get; set; }

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
                Product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };
            }
        }

        // POST: Delete
        public IActionResult OnPost(int id)
        {
            using var conn = new OracleConnection(_connectionString);
            conn.Open();

            using var cmd = new OracleCommand("DELETE FROM PRODUCT WHERE ID = :id", conn);
            cmd.Parameters.Add("id", id);

            cmd.ExecuteNonQuery();

            return RedirectToPage("Index");
        }
    }
}
