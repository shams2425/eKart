namespace ProductService.BussinessLayer.DTOs;

public record ProductUpdateRequest
    (Guid? ProductID, string? ProductName, CategoryOptions? Category, double? UnitPrice, int? QuantityStock)
{
    public ProductUpdateRequest() : this(default,default,default,default,default)
    {
        
    }
}
 