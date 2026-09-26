using Microsoft.EntityFrameworkCore;
using OrderService.DataAccessLayer.Context;
using OrderService.DataAccessLayer.Entities;
using OrderService.DataAccessLayer.RepositoriesContracts;
using System.Linq.Expressions;

namespace OrderService.DataAccessLayer.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Order?> AddOrder(Order order)
    {
       order.OrderID = Guid.NewGuid();
        await _db.AddAsync(order);
        await _db.SaveChangesAsync();

        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
      Order? existingOrder =  await _db.Orders
            .FirstOrDefaultAsync(temp => temp.OrderID == orderID);

        if (existingOrder == null)
        {
            return false;
        }

        _db.Orders .Remove(existingOrder);

       int affectedRows = await _db.SaveChangesAsync();
       return affectedRows > 0;
    }

    public async Task<Order?> GetOrderByCondition(Expression<Func<Order, bool>> conditionExpression)
    {
       return await _db.Orders
            .Include(temp => temp.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
     return await _db.Orders
            .Include(temp => temp.OrderItems)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order?>> GetOrdersByCondition(Expression<Func<Order, bool>> conditionExpression)
    {
        return await _db.Orders
            .Include(ord => ord.OrderItems)
            .Where(conditionExpression)
            .ToListAsync();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
       Order? existingOrder = await _db.Orders
            .Include(ord => ord.OrderItems)
            .FirstOrDefaultAsync(ord => ord.OrderID == order.OrderID);

        if(existingOrder == null)
        {
            return null;
        }

        _db.Entry(existingOrder).CurrentValues.SetValues(order);
        _db.OrderItem.RemoveRange(existingOrder.OrderItems);
        existingOrder.OrderItems = order.OrderItems;
        await _db.SaveChangesAsync();
        return order;
    }
}
