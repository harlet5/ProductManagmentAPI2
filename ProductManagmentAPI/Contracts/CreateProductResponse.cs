namespace ProductManagmentAPI.Contracts;

public record CreateProductResponse(
    int Id,
    string Name,
    int ProductGroupId,
    DateTime DateAdded,
    float Price,
    float PriceWithVAT,
    float VAT,
    List<int>? StoreIds);
