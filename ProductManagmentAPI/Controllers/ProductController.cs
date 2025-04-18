﻿using Microsoft.AspNetCore.Mvc;
using ProductManagmentAPI.Contracts;
using ProductManagmentAPI.Models;
using ProductManagmentAPI.Services;

namespace ProductManagmentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    // POST: api/product/CreateProduct
    [HttpPost("AddProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        Product product = new()
        {
            Name = request.Name,
            ProductGroupId = request.ProductGroupId,
            DateAdded = DateTime.Now,
            Price = request.Price!.Value,
            PriceWithVAT = request.PriceWithVAT!.Value,
            VAT = request.VAT!.Value
        };
        Product? createdProduct = await _productService.CreateProduct(product, request.StoreIds);

        if (createdProduct == null)
        {
            return Problem();
        }
        var responseDto = new CreateProductResponse(
            Id: createdProduct.Id,
            Name: createdProduct.Name,
            ProductGroupId: createdProduct.ProductGroupId,
            DateAdded: createdProduct.DateAdded,
            Price: createdProduct.Price,
            PriceWithVAT: createdProduct.PriceWithVAT,
            VAT: createdProduct.VAT,
            StoreIds: createdProduct.ProductStores?.Select(ps => ps.StoreId).ToList()
        );
        return CreatedAtAction(
            nameof(GetProduct),
            new { id = createdProduct.Id },
            responseDto); 
    }

    // GET: api/GetProduct or api/GetProduct/{id}
    [HttpGet("{id?}")]
    public async Task<IActionResult> GetProduct(int? id)
    {
        if (id == null)
        {
            return Ok(await _productService.GetAllProducts());
        }
        return Ok(await _productService.GetProductById((int)id));

    }

    // GET: api/GetProduct/GetProductGroupTree 
    [HttpGet("GetProductGroupTree")]
    public async Task<IActionResult> GetProductGroupTree()
    {
        List<ProductGroup> productGroups = await _productService.GetAllProductGroups();
        var tree = BuildProductGroupTree(productGroups);

        return Ok(tree);
    }

    private static List<ProductGroupDto> BuildProductGroupTree(List<ProductGroup> allGroups)
    {
        var groupDtos = allGroups.Select(g => new ProductGroupDto
        {
            Id = g.Id,
            Name = g.Name,
            ParentGroupId = g.ParentGroupId,
            ChildGroups = new List<ProductGroupDto>()
        }).ToList();

        var lookup = groupDtos.ToLookup(g => g.ParentGroupId);

        foreach (var group in groupDtos)
        {
            group.ChildGroups = lookup[group.Id].ToList();
        }

        return [.. lookup[null]];
    }
}