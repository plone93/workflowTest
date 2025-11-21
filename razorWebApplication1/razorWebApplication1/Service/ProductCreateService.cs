using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;

namespace razorWebApplication1.Service
{
    public class ProductCreateService
    {
        //private readonly string _connectionString;

        //// Create
        //public void AddProduct(Product product)
        //{
        //    using (var conn = new OracleConnection(_connectionString))
        //    {
        //        conn.Open();
        //        using (var cmd = new OracleCommand("INSERT INTO PRODUCT (ID, NAME, PRICE) VALUES (:id, :name, :price)", conn))
        //        {
        //            cmd.Parameters.Add("id", product.Id);
        //            cmd.Parameters.Add("name", product.Name);
        //            cmd.Parameters.Add("price", product.Price);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
