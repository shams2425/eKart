namespace OrderService.DataAccessLayer.Entities;

public class OrderItem
{
    public Guid OrderID { get; set; }
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity {  get; set; }
    public decimal TotalPrice { get; set; }
}
