using ProductService.BussinessLayer.DTOs;
using ProductService.BussinessLayer.ServiceContracts;

namespace ProductsService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            return Results.Ok(await productService.GetProducts());
        });


        app.MapGet("/api/products/search/product-id/{productID:guid}", async (IProductService service, Guid productID) =>
        {
            ProductResponse? productResponse = await service.GetProductByCondition(p => p.ProductID == productID);
            return Results.Ok(productResponse);
        });

        app.MapGet("/api/products/search/{searchString}", async (IProductService service, string searchString) =>
        {
            List<ProductResponse> productsByName = await service
                                                        .GetProductsByCondition(p => p.ProductName != null && p.ProductName.Contains(searchString)
                                                                                    );

            List<ProductResponse> productByCategory = await service
                                                        .GetProductsByCondition(p => p.Category != null && p.Category.Contains(searchString)
                                                                                    );


            IEnumerable<ProductResponse?> products = productsByName.Union(productByCategory);

            return Results.Ok(products);
        });

        app.MapPost("/api/products", async (IProductService service, ProductAddRequest productAddRequest) =>
        {
            if (productAddRequest is null)
            {
                return Results.BadRequest("Product is null");
            }

            ProductResponse? addedProductResponce = await service.AddProduct(productAddRequest);

            if (addedProductResponce is not null)
            {
                return Results.Created($"/api/products/search/product-id/{addedProductResponce.ProductID}", addedProductResponce);
            }
            else
            {
                return Results.Problem("Error while adding product.");
            }

        });

        app.MapPut("/api/products", async (IProductService service,
                                           ProductUpdateRequest productUpdateRequest
                                           ) =>
        {
            if (productUpdateRequest is null)
            {
                return Results.BadRequest("productUpdateRequest is null");
            }

            ProductResponse? updatedProductResponce = await service.UpdateProduct(productUpdateRequest);

            if (updatedProductResponce != null)
            {
                return Results.Ok(updatedProductResponce);
            }
            else
            {
                return Results.Problem("Error while updating the product.");
            }



        });


        app.MapDelete("/api/products/{productId:guid}", async (IProductService service, Guid productId) =>
        {
            var isDeleted = await service.DeleteProduct(productId);

            if (isDeleted)
                return Results.Ok(isDeleted);
            else
                return Results.Problem("Error while deleting the record.");
        });

        return app;
    }
}