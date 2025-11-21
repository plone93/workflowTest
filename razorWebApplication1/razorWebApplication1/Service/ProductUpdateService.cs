using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;

namespace razorWebApplication1.Service
{
    public class ProductUpdateService
    {
        //private readonly string _connectionString;

        //// Update
        //public void UpdateProduct(Product product)
        //{
        //    using (var conn = new OracleConnection(_connectionString))
        //    {
        //        conn.Open();
        //        using (var cmd = new OracleCommand("UPDATE PRODUCT SET NAME = :name, PRICE = :price WHERE ID = :id", conn))
        //        {
        //            cmd.Parameters.Add("name", product.Name);
        //            cmd.Parameters.Add("price", product.Price);
        //            cmd.Parameters.Add("id", product.Id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
