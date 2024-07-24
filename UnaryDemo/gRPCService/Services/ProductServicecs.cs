using Grpc.Core;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gRPCService.Services
{
    public class ProductServicecs : Product.ProductBase
    {
        private readonly ILogger<ProductModel> _logger;
        public string _connectionString;

        public ProductServicecs(ILogger<ProductModel> logger)
        {
            _logger = logger;
        }
        public override async Task<ProductSaveResponse> saveProduct(ProductModel request, ServerCallContext context)
        {
            _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ProductTest; Integrated Security = True";
            ProductSaveResponse result1 = new ProductSaveResponse()
            {
                IsSuccessful = true,
                StatusCode = 200
            };
            int res;
            string query = "INSERT INTO Product (productName, productCode , price) VALUES (@proname,@procode, @prices);";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@proname", request.ProductName);
                cmd.Parameters.AddWithValue("@procode", request.ProductCode);
                cmd.Parameters.AddWithValue("@prices", request.Price);

                Console.WriteLine("connection is established");
                res = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (res != -1)
            {
                result1.IsSuccessful = true;
                result1.StatusCode = 200;
                return result1;
            }
            result1.StatusCode = 500;
            return result1;
        }
    }
}
