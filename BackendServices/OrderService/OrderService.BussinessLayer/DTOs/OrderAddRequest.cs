namespace OrderService.BussinessLayer.DTOs;

public record OrderAddRequest(Guid UserId, DateTime OrderDate, List<OrderItemAddRequest> OrderItem)
{
    public OrderAddRequest() : this(default,default,default)
    {
        
    }
}
