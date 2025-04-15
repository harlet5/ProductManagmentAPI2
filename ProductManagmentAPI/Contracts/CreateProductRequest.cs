using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace ProductManagmentAPI.Contracts;

public class CreateProductRequest
{
    [Required]
    public required string Name { get; init; }
    [Required]
    public int ProductGroupId { get; set; }
    public float? Price { get; init; } = null;
    public float? PriceWithVAT { get; init; } = null;
    public float? VAT { get; init; } = null;
    public List<int>? StoreIds { get; init; } = [];

    public CreateProductRequest(string name, int productGroupId, float? price, float? priceWithVAT, float? vat, List<int>? storeIds)
    {
        Name = name;
        ProductGroupId = productGroupId;
        StoreIds = storeIds;
        Price = price;
        PriceWithVAT = priceWithVAT;
        VAT = vat;

        VAT ??= ((PriceWithVAT - Price) * 100) / Price;
        Price ??= 100 * PriceWithVAT / (100 + VAT);
        PriceWithVAT ??= Price + (VAT * Price) / 100;
    }
}