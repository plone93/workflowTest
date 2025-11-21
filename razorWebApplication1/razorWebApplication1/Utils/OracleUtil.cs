using Oracle.ManagedDataAccess.Client;

namespace razorWebApplication1.Utils
{
    public class OracleUtil
    {
        private readonly string _connectionString;

        // IConfiguration을 통해 appsettings.json에서 읽음
        public OracleUtil(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleConnection");
        }

        public void TestConnection()
        {
            using var conn = new OracleConnection(_connectionString);
            conn.Open();
            Console.WriteLine("Oracle DB 接続成功");
        }
    }
}
