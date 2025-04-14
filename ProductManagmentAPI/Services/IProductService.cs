using ProductManagmentAPI.Contracts;
using ProductManagmentAPI.Models;

namespace ProductManagmentAPI.Services;

public interface IProductService
{
    Task<int> CreateProduct(Product product);
    Task<GetProductResponse?> GetProductById(int id);
    Task<List<GetProductResponse>> GetAllProducts();
    Task<List<ProductGroup>> GetAllProductGroups();

}
