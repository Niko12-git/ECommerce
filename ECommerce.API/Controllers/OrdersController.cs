using ECommerce.Application.Services;
using ECommerce.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        try
        {
            var order = await _service.CreateOrderAsync(dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetOrderAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpGet("my-orders/{customerId}")]
    public async Task<IActionResult> GetMyOrders(int customerId)
    {
        var orders = await _service.GetOrdersByCustomerDtoAsync(customerId);
        return Ok(orders);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllOrdersDtoAsync();
        return Ok(orders);
    }
}