namespace OrderService.DataAccessLayer.Entities;

public class Order
{
    public Guid OrderID { get; set; }
    public Guid UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}
