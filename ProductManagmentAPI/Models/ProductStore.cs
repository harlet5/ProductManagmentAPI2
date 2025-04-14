namespace ProductManagmentAPI.Models;

public class ProductStore
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
}
