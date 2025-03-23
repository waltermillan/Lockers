using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Reflection.Metadata;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LocationsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocationsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Location>>> Get()
    {
        var location = await _unitOfWork.Locations.GetAllAsync();
        return _mapper.Map<List<Location>>(location);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Location>> Get(int id)
    {
        var location = await _unitOfWork.Locations.GetByIdAsync(id);

        if (location is null)
            return NotFound();

        return _mapper.Map<Location>(location);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Location>> Post(Location oLocation)
    {
        var msg = string.Empty;
        try
        {
            var location = _mapper.Map<Location>(oLocation);
            _unitOfWork.Locations.Add(location);
            await _unitOfWork.SaveAsync();

            if (location is null)
                return BadRequest();

            oLocation.Id = location.Id;

            object[] obj = { location.Id, location.Address, location.PostalCode };
            msg = string.Format(Constants.MSG_LOCATION_ADDED_SUCCESS, obj );
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oLocation.Id }, oLocation);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCATION_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Location>> Put([FromBody] Location oLocation)
    {
        var msg = string.Empty;
        try
        {
            if (oLocation is null)
                return NotFound();

            var location = _mapper.Map<Location>(oLocation);
            _unitOfWork.Locations.Update(location);
            await _unitOfWork.SaveAsync();

            object[] obj = { location.Id, location.Address, location.PostalCode };
            msg = string.Format(Constants.MSG_LOCATION_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oLocation;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCATION_UPDATED_ERROR;
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
            var location = await _unitOfWork.Locations.GetByIdAsync(id);

            if (location is null)
                return NotFound();

            _unitOfWork.Locations.Remove(location);
            await _unitOfWork.SaveAsync();

            object[] obj = { location.Id, location.Address, location.PostalCode };
            msg = string.Format(Constants.MSG_LOCATION_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCATION_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}