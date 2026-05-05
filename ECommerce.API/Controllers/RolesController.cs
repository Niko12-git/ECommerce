using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepo;

    public RolesController(IRoleRepository roleRepo)
    {
        _roleRepo = roleRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _roleRepo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto dto)
    {
        try
        {
            if (await _roleRepo.ExistsAsync(dto.Name))
                return BadRequest(new { message = "El rol ya existe" });

            var role = new Role(dto.Name);
            await _roleRepo.AddAsync(role);
            return Ok(new { message = "Rol creado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}