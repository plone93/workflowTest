using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Data;
using razorWebApplication1.Models;
using razorWebApplication1.Service;
using razorWebApplication1.Services;
using System.Configuration;

using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using razorWebApplication1.Utils;


namespace razorWebApplication1.Pages
{
    public class SelectModel(IConfiguration configuration) : PageModel
    {

        private readonly string _connectionString = configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("OracleConnection の接続文字列が設定されていません。");

        public List<Product> productList { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int totalCount { get; set; }
        public int totalPages => (int)Math.Ceiling((double)totalCount / pageSize);

        public void OnGet(int page)
        {
            if (page > 1)
            {
                pageNumber = page;
            }
            int offset = (pageNumber - 1) * pageSize;
            using var conn = new OracleConnection(_connectionString);
            conn.Open();
            using var cmd1 = new OracleCommand("SELECT COUNT(*) FROM PRODUCT", conn);
            {
                totalCount = Convert.ToInt32(cmd1.ExecuteScalar());
            }



            using var cmd2 = new OracleCommand("SELECT ID, NAME, PRICE FROM PRODUCT ORDER BY ID OFFSET :offset ROWS FETCH NEXT :limit ROWS ONLY", conn);
            cmd2.Parameters.Add(new OracleParameter("offset", offset));
            cmd2.Parameters.Add(new OracleParameter("limit", pageSize));
            using var reader = cmd2.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };
                productList.Add(product);
            }

        }

        // POST: Update
        public IActionResult OnPost(string keyword, string action)
        {
            if (action == "search")
            {
                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("SELECT ID, NAME, PRICE FROM PRODUCT WHERE NAME LIKE '%' || :keyword || '%'", conn);
                cmd.Parameters.Add("keyword", keyword);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };
                    productList.Add(product);
                }
            }

            return Page();
        }

        // サービスクラスを利用するパターン
        //private readonly ProductReadService _service;

        //public List<Product> productList { get; set; }

        //public SelectModel(ProductReadService service)
        //{
        //    _service = service;
        //}

        //public void OnGet()
        //{
        //    productList = _service.GetProducts();
        //}

        // DbContextを直接利用するパターン
        //private readonly AppDbContext _context;

        //public SelectModel(AppDbContext context)
        //{
        //    _context = context;
        //}

        //public　 IList<Product> productList { get; set; } = new List<Product>();

        //public async Task OnGetAsync()
        //{
        //    //productList = await _context.Products.ToListAsync();
        //    productList = await _context.productList
        //        .FromSqlRaw("SELECT ID, NAME, PRICE FROM PRODUCT")
        //        .ToListAsync();

        //}



    }
}
