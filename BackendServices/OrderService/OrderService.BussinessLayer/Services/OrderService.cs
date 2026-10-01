using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using OrdersService.DataAccessLayer.Entities;
using OrdersService.DataAccessLayer.RepositoriesContracts;
using OrdersService.BusinessLogicLayer.ServiceContracts;
using OrdersService.BussinessLayer.DTOs;
using System.Linq.Expressions;

namespace OrdersService.BusinessLogicLayer.Services;

public class OrderService : IOrderService
{
    #region INITIALIZATION
    private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
    private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator;
    private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _ordersRepository;
    #endregion

    #region CONSTRUCTOR
    public OrderService(
                        IOrderRepository ordersRepository,
                        IMapper mapper,
                        IValidator<OrderAddRequest> orderAddRequestValidator,
                        IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
                        IValidator<OrderUpdateRequest> orderUpdateRequestValidator)
    {
        _orderAddRequestValidator = orderAddRequestValidator;
        _orderItemAddRequestValidator = orderItemAddRequestValidator;
        _orderUpdateRequestValidator = orderUpdateRequestValidator;
        _mapper = mapper;
        _ordersRepository = ordersRepository;
    }
    #endregion
    public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
    {
        // Check for null parameter
        if (orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }

        // Validate OrderAddRequest
        ValidationResult orderAddRequestValidationResult =
            await _orderAddRequestValidator.ValidateAsync(orderAddRequest);

        if (!orderAddRequestValidationResult.IsValid)
        {
            string errors = string.Join(", ",
                orderAddRequestValidationResult.Errors
                    .Select(vf => vf.ErrorMessage));

            throw new ArgumentException(errors);
        }

        // Validate OrderItems
        foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItem)
        {
            ValidationResult validationResult = await _orderItemAddRequestValidator.ValidateAsync(orderItemAddRequest);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(
                    ", ",
                    validationResult.Errors
                        .Select(temp => temp.ErrorMessage));

                throw new ArgumentException(errors);
            }
        }

        // TODO:
        // Add logic for checking if UserID exists in Users microservice

        // Map OrderAddRequest to Order
        Order orderInput = _mapper.Map<Order>(orderAddRequest);

        // Generate OrderID
        orderInput.OrderID = Guid.NewGuid();

        // Generate OrderItem values
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.OrderID = orderInput.OrderID;

            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        // Calculate TotalBill
        orderInput.TotalBill = orderInput.OrderItems.Sum(oi => oi.TotalPrice);

        // Invoke repository
        Order? addedOrder = await _ordersRepository.AddOrder(orderInput);

        if (addedOrder == null)
        {
            return null;
        }

        // Map Order to OrderResponse
        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);

        return addedOrderResponse;
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        // Check for null parameter
        if (orderUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(orderUpdateRequest));
        }

        // Validate OrderUpdateRequest
        ValidationResult orderUpdateRequestValidationResult =
            await _orderUpdateRequestValidator.ValidateAsync(orderUpdateRequest);

        if (!orderUpdateRequestValidationResult.IsValid)
        {
            string errors = string.Join(", ", orderUpdateRequestValidationResult.Errors
                                            .Select(temp => temp.ErrorMessage));

            throw new ArgumentException(errors);
        }

        // Validate OrderItems
        foreach (OrderItemUpdateRequest orderItemUpdateRequest in orderUpdateRequest.OrderItems)
        {
            ValidationResult validationResult =
                await _orderItemUpdateRequestValidator
                    .ValidateAsync(orderItemUpdateRequest);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ",
                                        validationResult.Errors.Select(temp => temp.ErrorMessage));

                throw new ArgumentException(errors);
            }
        }

        // TODO:
        // Add logic for checking if UserID exists in Users microservice

        // Map OrderUpdateRequest to Order
        Order orderInput = _mapper.Map<Order>(orderUpdateRequest);

        // Generate OrderItem values
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.OrderID = orderInput.OrderID;

            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        // Calculate TotalBill
        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);

        // Invoke repository
        Order? updatedOrder = await _ordersRepository.UpdateOrder(orderInput);

        if (updatedOrder == null)
        {
            return null;
        }

        // Map Order to OrderResponse
        OrderResponse updatedOrderResponse = _mapper.Map<OrderResponse>(updatedOrder);

        return updatedOrderResponse;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {

        Order? existingOrder = await _ordersRepository.GetOrderByCondition(ord => ord.OrderID == orderID);

        if (existingOrder == null)
        {
            return false;
        }

        bool isDeleted = await _ordersRepository.DeleteOrder(orderID);

        return isDeleted;
    }

    #region GET
    public async Task<OrderResponse?> GetOrderByCondition(Expression<Func<Order, bool>> condition)
    {
        Order? order = await _ordersRepository
                            .GetOrderByCondition(condition);

        if (order == null)
        {
            return null;
        }

        OrderResponse orderResponse = _mapper.Map<OrderResponse>(order);

        return orderResponse;
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(Expression<Func<Order, bool>> condition)
    {
        IEnumerable<Order> orders = await _ordersRepository.GetOrdersByCondition(condition);

        IEnumerable<OrderResponse> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);

        return orderResponses.ToList();
    }

    public async Task<List<OrderResponse?>> GetOrders()
    {
        IEnumerable<Order> orders = await _ordersRepository.GetOrders();

        IEnumerable<OrderResponse> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);

        return orderResponses.ToList();
    }
    #endregion
}