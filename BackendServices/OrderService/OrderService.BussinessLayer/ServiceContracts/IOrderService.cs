using OrdersService.DataAccessLayer.Entities;
using OrdersService.BussinessLayer.DTOs;
using System.Linq.Expressions;

namespace OrdersService.BusinessLogicLayer.ServiceContracts;

public interface IOrderService
{
    Task<List<OrderResponse?>> GetOrders();

    Task<List<OrderResponse?>> GetOrdersByCondition(Expression<Func<Order, bool>> condition);

    Task<OrderResponse?> GetOrderByCondition(Expression<Func<Order, bool>> condition);

    Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);

    Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);

    Task<bool> DeleteOrder(Guid orderID);
}
