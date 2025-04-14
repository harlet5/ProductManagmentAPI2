using Microsoft.EntityFrameworkCore;
using ProductManagmentAPI.Contracts;
using ProductManagmentAPI.Data;
using ProductManagmentAPI.Models;

namespace ProductManagmentAPI.Services;

public class ProductService(ApiDB dbContext) : IProductService
{
    private readonly ApiDB _dbContext = dbContext;

    public async Task<int> CreateProduct(Product product)
    {
        try
        {
            await _dbContext.Products.AddAsync(product);
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception here if needed
            return 0; 
        }
    }

    public async Task<GetProductResponse?> GetProductById(int id)
    {
        return await _dbContext.Products
            .Where(p => p.Id == id)
            .Select(p => new GetProductResponse(
                p.Name,
                p.ProductGroup.Name,
                p.DateAdded,
                p.Price,
                p.PriceWithVAT,
                p.VAT))
            .FirstOrDefaultAsync();
    }

    public async Task<List<GetProductResponse>> GetAllProducts()
    {
        return await _dbContext.Products
            .Select(p => new GetProductResponse(
                p.Name,
                p.ProductGroup.Name,
                p.DateAdded,
                p.Price,
                p.PriceWithVAT,
                p.VAT))
            .ToListAsync();
    }

    public async Task<List<ProductGroup>> GetAllProductGroups()
    {
        return await _dbContext.ProductGroups.ToListAsync();
    }
}
