using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var order = await _service.CreateOrderAsync(dto);
        return Ok(order);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetOrderAsync(id);
        return order is null ? NotFound() : Ok(order);
    }
}