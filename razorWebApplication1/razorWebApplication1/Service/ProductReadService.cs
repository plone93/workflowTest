using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;

namespace razorWebApplication1.Services
{
    public class ProductReadService
    {
        //private readonly string _connectionString;

        //public ProductReadService(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("OracleConnection");
        //}

        //public List<Product> GetProducts()
        //{
        //    var products = new List<Product>();

        //    using var conn = new OracleConnection(_connectionString);
        //    conn.Open();

        //    using var cmd = new OracleCommand("SELECT ID, NAME, PRICE FROM PRODUCT", conn);
        //    using var reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        products.Add(new Product
        //        {
        //            Id = reader.GetInt32(0),
        //            Name = reader.GetString(1),
        //            Price = reader.GetDecimal(2)
        //        });
        //    }

        //    return products;
        //}
    }
}
