namespace ProductManagmentAPI.Models;

public class ProductGroupDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentGroupId { get; set; }
    public List<ProductGroupDto> ChildGroups { get; set; } = null!;
}
