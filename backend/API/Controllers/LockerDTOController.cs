//using API.DTOs;
using AutoMapper;
using Core.Constants;
using Core.DTOs;
using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;
[ApiController]
[Route("api/lockers")] // Usamos el plural en la ruta para seguir la convención RESTful
public class LockerDTOController : BaseApiController
{
    private readonly LockerDTOService _lockerDTOService;

    public LockerDTOController(LockerDTOService lockerDTOService)
    {
        _lockerDTOService = lockerDTOService;
    }

    [HttpGet("dto")]
    public async Task<IActionResult> GetAllLockersDTO()
    {
        var lockersDTO = await _lockerDTOService.GetAllLockersAsync();

        if (lockersDTO is null || !lockersDTO.Any())
            return NotFound(); // Retorna NotFound si no hay lockers

        return Ok(lockersDTO); // Retorna los lockers en formato DTO
    }


    [HttpGet("{id}/dto")]
    public async Task<IActionResult> GetLockerDTO(int lockerId)
    {
        var lockerDTO = await _lockerDTOService.GetLockerDTOAsync(lockerId);

        if (lockerDTO is null)
            return NotFound();

        return Ok(lockerDTO);
    }
}