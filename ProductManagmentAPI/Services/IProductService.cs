using ProductManagmentAPI.Contracts;
using ProductManagmentAPI.Models;

namespace ProductManagmentAPI.Services;

public interface IProductService
{
    Task<Product?> CreateProduct(Product product, List<int>? storeIds);
    Task<GetProductResponse?> GetProductById(int id);
    Task<List<GetProductResponse>> GetAllProducts();
    Task<List<ProductGroup>> GetAllProductGroups();
    Task<int> CreateProductStore(int productId, List<int> storeIds);

}
