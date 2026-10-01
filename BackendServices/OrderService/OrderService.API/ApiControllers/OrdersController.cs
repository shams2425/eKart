using Microsoft.AspNetCore.Mvc;
 using OrdersService.BusinessLogicLayer.ServiceContracts;
using OrdersService.BussinessLayer.DTOs;

namespace OrdersService.API.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: /api/Orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse?>>> Get()
    {
        List<OrderResponse?> orders =
            await _orderService.GetOrders();

        return Ok(orders);
    }


    // GET: /api/Orders/search/orderid/{orderID}
    [HttpGet("search/orderid/{orderID}")]
    public async Task<ActionResult<OrderResponse?>> GetOrderByOrderID(Guid orderID)
    {
        //Expression<Func<Order, bool>> condition = temp => temp.OrderID == orderID;

        OrderResponse? order = await _orderService.GetOrderByCondition(temp => temp.OrderID == orderID);

        return Ok(order);
    }


    // GET: /api/Orders/search/productid/{productID}
    [HttpGet("search/productid/{productID}")]
    public async Task<ActionResult<IEnumerable<OrderResponse?>>> GetOrdersByProductID(Guid productID)
    {
        //Expression<Func<Order, bool>> condition = temp => temp.OrderItems.Any(orderItem => orderItem.ProductID == productID);

        List<OrderResponse?> orders =
            await _orderService.GetOrdersByCondition(temp => temp.OrderItems.Any(orderItem => orderItem.ProductID == productID));

        return Ok(orders);
    }


    // GET: /api/Orders/search/userid/{userID}
    [HttpGet("search/userid/{userID}")]
    public async Task<ActionResult<IEnumerable<OrderResponse?>>> GetOrdersByUserID(Guid userID)
    {
        //Expression<Func<Order, bool>> condition = temp => temp.UserID == userID;

        List<OrderResponse?> orders =
            await _orderService.GetOrdersByCondition(temp => temp.UserID == userID);

        return Ok(orders);
    }


    // GET: /api/Orders/search/orderDate/{orderDate}
    [HttpGet("search/orderDate/{orderDate}")]
    public async Task<ActionResult<IEnumerable<OrderResponse?>>> GetOrdersByOrderDate(DateTime orderDate)
    {
        DateTime startDate = orderDate.Date;
        DateTime endDate = startDate.AddDays(1);


        List<OrderResponse?> orders = await _orderService.GetOrdersByCondition(ord => ord.OrderDate >= startDate
                                                       && ord.OrderDate < endDate);

        return Ok(orders);
    }


    // POST: /api/Orders
    [HttpPost]
    public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        OrderResponse? orderResponse = await _orderService.AddOrder(orderAddRequest);

        if (orderResponse == null)
        {
            return Problem("Error in adding order");
        }

        return Created($"api/Orders/search/orderid/{orderResponse.OrderID}", orderResponse);
    }


    // PUT: /api/Orders/{orderID}
    [HttpPut("{orderID}")]
    public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        if (orderID != orderUpdateRequest.OrderID)
        {
            return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
        }

        OrderResponse? orderResponse = await _orderService.UpdateOrder(orderUpdateRequest);

        if (orderResponse == null)
        {
            return Problem("Error in updating order");
        }

        return Ok(orderResponse);
    }


    // DELETE: /api/Orders/{orderID}
    [HttpDelete("{orderID}")]
    public async Task<IActionResult> Delete(Guid orderID)
    {
        if (orderID == Guid.Empty)
        {
            return BadRequest("Invalid order ID");
        }

        bool isDeleted = await _orderService.DeleteOrder(orderID);

        if (!isDeleted)
        {
            return Problem("Error in deleting order");
        }

        return Ok(isDeleted);
    }
}