using AutoMapper;
using Core.Constants;
using Core.DTOs;
using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RentsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly RentDTOService _rentDTOService;

    public RentsController(IUnitOfWork unitOfWork, IMapper mapper, RentDTOService rentDTOService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _rentDTOService = rentDTOService;
    }

    [HttpGet("dto")]
    public async Task<IActionResult> GetAllRentsDTO()
    {
        var rentsDTO = await _rentDTOService.GetAllRentsAsync();

        if (rentsDTO is null)
            rentsDTO = new List<RentDTO>();

        return Ok(rentsDTO);
    }


    [HttpGet("{id}/dto")]
    public async Task<IActionResult> GetRentDTO(int id)
    {
        var rentDTO = await _rentDTOService.GetRentDTOAsync(id);

        if (rentDTO is null)
            return NotFound();

        return Ok(rentDTO);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Rent>>> Get()
    {
        var rents = await _unitOfWork.Rents.GetAllAsync();
        return _mapper.Map<List<Rent>>(rents);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Rent>> Get(int id)
    {
        var rent = await _unitOfWork.Rents.GetByIdAsync(id);

        if (rent is null)
            return NotFound();

        return _mapper.Map<Rent>(rent);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Rent>> Post(Rent oRent)
    {
        var msg = string.Empty;
        try
        {
            var rent = _mapper.Map<Rent>(oRent);
            _unitOfWork.Rents.Add(rent);
            await _unitOfWork.SaveAsync();

            if (rent is null)
                return BadRequest();

            oRent.Id = rent.Id;

            object[] obj = new object[] { rent.Id, rent.IdCustomer, rent.IdLocker, rent.RentalDate, rent.ReturnDate };
            msg = string.Format(Constants.MSG_RENT_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oRent.Id }, oRent);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_RENT_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Rent>> Put([FromBody] Rent oRent)
    {
        var msg = string.Empty;
        try
        {
            if (oRent is null)
                return NotFound();

            var rent = _mapper.Map<Rent>(oRent);
            _unitOfWork.Rents.Update(rent);
            await _unitOfWork.SaveAsync();

            object[] obj = new object[] { rent.Id, rent.IdCustomer, rent.IdLocker, rent.RentalDate, rent.ReturnDate }; 
            msg = string.Format(Constants.MSG_RENT_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oRent;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_RENT_UPDATED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var msg = string.Empty;
        try
        {
            var rent = await _unitOfWork.Rents.GetByIdAsync(id);

            if (rent is null)
                return NotFound();

            _unitOfWork.Rents.Remove(rent);
            await _unitOfWork.SaveAsync();

            object[] obj = new object[] { rent.Id, rent.IdCustomer, rent.IdLocker, rent.RentalDate, rent.ReturnDate }; 
            msg = string.Format(Constants.MSG_RENT_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_RENT_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}