using OrderService.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace OrderService.DataAccessLayer.RepositoriesContracts;

public interface IOrderRepository
{
    Task<Order?> AddOrder(Order order);
    Task<bool> DeleteOrder(Guid orderID);
    Task<Order?> GetOrderByCondition(Expression<Func<Order, bool>> conditionExpression);
    Task<IEnumerable<Order>> GetOrders();
    Task<IEnumerable<Order?>> GetOrdersByCondition(Expression<Func<Order, bool>> conditionExpression);
    Task<Order?> UpdateOrder(Order order);
}
    