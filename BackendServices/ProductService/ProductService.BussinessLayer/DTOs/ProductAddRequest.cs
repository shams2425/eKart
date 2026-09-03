namespace ProductService.BussinessLayer.DTOs;

public record ProductAddRequest
    (string? ProductName, CategoryOptions? Category, double? UnitPrice, int? QuantityStock)
{
    public ProductAddRequest() : this(default,default,default,default)
    {
        
    }
}
