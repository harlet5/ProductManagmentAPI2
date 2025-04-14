namespace ProductManagmentAPI.Contracts;

public record GetProductResponse(
    string Name,
    string ProductGroupName,
    DateTime DateAdded,
    float Price,
    float PriceWithVAT,
    float VAT);

