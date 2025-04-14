namespace ProductManagmentAPI.Models;

public class ProductGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentGroupId { get; set; }
    public ProductGroup? ParentGroup { get; set; }

    public List<ProductGroup> ChildGroups { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}
