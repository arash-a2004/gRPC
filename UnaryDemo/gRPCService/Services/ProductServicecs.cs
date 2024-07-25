using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Data.SqlClient;
using static gRPCService.Product;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gRPCService.Services
{
    public class ProductServicecs : Product.ProductBase
    {
        private readonly IMapper _mapper;    
        private readonly ILogger<ProductModel> _logger;
        public string _connectionString { get; set; }
        public ProductServicecs(ILogger<ProductModel> logger,IMapper mapper)
        {
            _mapper = mapper;
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

        public override async Task<productList> getproducts(Empty request, ServerCallContext context)
        {
            _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ProductTest; Integrated Security = True";
            List<models.ProductModels.ProductModel> products = new();
            string query = "SELECT * from [Product];";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new gRPCService.models.ProductModels.ProductModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            productName = reader["productName"].ToString(),
                            productCode = reader["productCode"].ToString(),
                            Price = reader["price"].ToString()
                        });
                    }
                }
                con.Close();
            }

            List<models.ProductModels.Dto.ProductModelDto> productModelDtos = new();
            foreach(models.ProductModels.ProductModel items in products)
            {
                productModelDtos.Add(_mapper.Map<models.ProductModels.Dto.ProductModelDto>(items));
            }

            var result = new productList();
            foreach(models.ProductModels.Dto.ProductModelDto item in productModelDtos)
            {
                var product = new ProductModel()
                {
                    Price = item.Price,
                    ProductCode = item.productCode,
                    ProductName = item.productName
                };
                result.List.Add(product);
            }

            return result;
        }
    }
}
