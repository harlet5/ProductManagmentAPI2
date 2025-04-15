using System.Text.Json.Serialization;

namespace ProductManagmentAPI.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ProductGroupId { get; set; }
    public ProductGroup ProductGroup { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public float Price { get; set; }
    public float PriceWithVAT { get; set; }
    public float VAT { get; set; }

    public List<ProductStore> ProductStores { get; set; } = [];
}
