namespace OrderService.BussinessLayer.DTOs;

public record OrderUpdateRequest(Guid OrderID, Guid UserID, DateTime OrderDate, List<OrderItemAddRequest> OrderItems)
{
    public OrderUpdateRequest() : this(default, default, default, default)
    {

    }
}
