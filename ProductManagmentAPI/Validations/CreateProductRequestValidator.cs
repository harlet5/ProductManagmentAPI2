using FluentValidation;
using ProductManagmentAPI.Contracts;
using ProductManagmentAPI.Data;

namespace ProductManagmentAPI.Validations;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly ApiDB _dbContext;
    public CreateProductRequestValidator(ApiDB dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ProductGroupId)
            .NotEmpty()
            .Must(BeAValidProductGroupId)
            .WithMessage("The specified ProductGroupId does not exist.");

        RuleFor(x => x).Must(x =>
            (x.Price != null && x.PriceWithVAT != null) ||
            (x.Price != null && x.VAT != null) ||
            (x.PriceWithVAT != null && x.VAT != null))
            .WithMessage("At least two of Price, PriceWithVAT, or VAT must be provided.");
        
        RuleFor(x => x).Must(x =>
            (x.Price != null && x.PriceWithVAT != null && x.VAT != null) &&
            (x.Price + (x.Price * (x.VAT / 100)) == x.PriceWithVAT))
            .WithMessage("VAT calculation error: PriceWithVAT ≠ Price + (VAT * Price) / 100");

        RuleFor(x => x.StoreIds)
            .Must(storeIds => storeIds == null || storeIds.All(BeValidStoreId))
            .WithMessage("One or more specified StoreIds do not exist");

    }
    private bool BeAValidProductGroupId(int productGroupId)
    {
        return _dbContext.ProductGroups.Any(pg => pg.Id == productGroupId);
    }

    private bool BeValidStoreId(int storeId)
    {
        return _dbContext.Stores.Any(st => st.Id == storeId);
    }
}
