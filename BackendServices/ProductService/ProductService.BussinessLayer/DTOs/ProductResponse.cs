namespace ProductService.BussinessLayer.DTOs;

public record ProductResponse
    (Guid ProductID, string ProductName, CategoryOptions Category, double? UnitPrice, int? QuantityStock)
{
    public ProductResponse() : this(default, default, default, default, default)
    {

    }
}


