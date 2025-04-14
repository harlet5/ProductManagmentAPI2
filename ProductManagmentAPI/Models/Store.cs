namespace ProductManagmentAPI.Models;

public class Store
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Product> Products { get; set; } = [];
    public List<ProductStore> ProductStores { get; set; } = [];
}
