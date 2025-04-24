using API.DTOs;
using API.Services;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

public class LockersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly LockerDTOService _lockerDTOService;

    public LockersController(IUnitOfWork unitOfWork, IMapper mapper, LockerDTOService lockerDTOService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _lockerDTOService = lockerDTOService;
    }

    [HttpGet("dto")]
    public async Task<IActionResult> GetAllLockersDTO()
    {
        var lockersDTO = await _lockerDTOService.GetAllLockersAsync();

        if (lockersDTO is null || !lockersDTO.Any())
            return NotFound();

        return Ok(lockersDTO);
    }


    [HttpGet("dto/{id}")]
    public async Task<IActionResult> GetLockerDTO(int id)
    {
        var lockerDTO = await _lockerDTOService.GetLockerDTOAsync(id);

        if (lockerDTO is null)
            return NotFound();

        return Ok(lockerDTO);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Locker>>> Get()
    {
        var lockers = await _unitOfWork.Lockers.GetAllAsync();
        return _mapper.Map<List<Locker>>(lockers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Locker>> Get(int id)
    {
        var locker = await _unitOfWork.Lockers.GetByIdAsync(id);

        if (locker is null)
            return NotFound();

        return _mapper.Map<Locker>(locker);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Locker>> Post(Locker oLocker)
    {
        var msg = string.Empty;
        try
        {
            var locker = _mapper.Map<Locker>(oLocker);
            _unitOfWork.Lockers.Add(locker);
            await _unitOfWork.SaveAsync();

            if (locker is null)
                return BadRequest();

            oLocker.Id = locker.Id;

			object[] obj = { locker.Id, locker.SerialNumber, locker.IdLocation, locker.IdPrice, locker.Rented };
			msg = string.Format(Constants.MSG_LOCKER_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);
            return CreatedAtAction(nameof(Post), new { id = oLocker.Id }, oLocker);
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateException exception)
        {
            msg = Constants.MSG_LOCKER_ADDED_ERROR;
            Log.Logger.Error(msg, exception);

            var response = new
            {
                Code = 400,
                Message = exception.Message
            };

            return BadRequest(response);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCKER_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Locker>> Put([FromBody] Locker oLocker)
    {
        var msg = string.Empty;
        try
        {
            if (oLocker is null)
                return NotFound();

            var car = _mapper.Map<Locker>(oLocker);
            _unitOfWork.Lockers.Update(car);
            await _unitOfWork.SaveAsync();

			object[] obj = { oLocker.Id, oLocker.SerialNumber, oLocker.IdLocation, oLocker.IdPrice, oLocker.Rented };
			msg = string.Format(Constants.MSG_LOCKER_UPDATED_SUCCESS, obj);
            Log.Information(msg);
            return oLocker;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCKER_UPDATED_ERROR;
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
            var locker = await _unitOfWork.Lockers.GetByIdAsync(id);

            if (locker is null)
                return NotFound();

            _unitOfWork.Lockers.Remove(locker);
            await _unitOfWork.SaveAsync();

			object[] obj = { locker.Id, locker.SerialNumber, locker.IdLocation, locker.IdPrice, locker.Rented };
			msg = string.Format(Constants.MSG_LOCKER_DELETED_SUCCESS, obj);
            Log.Information(msg);
            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_LOCKER_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Locker>> Patch(int id, [FromBody] LockerPatchDTO lockerPatch)
    {
        string msg = string.Empty;

        try
        {
            if (lockerPatch is null)
                return BadRequest("Invalid input.");

            var locker = await _unitOfWork.Lockers.GetByIdAsync(id);

            if (locker is null)
                return NotFound();

            if (lockerPatch.Rented.HasValue)
                locker.Rented = lockerPatch.Rented.Value;

            _unitOfWork.Lockers.Update(locker);
            await _unitOfWork.SaveAsync();
            msg = string.Format("Locker with ID {0} updated successfully.", id);
            Log.Information(msg);

            return Ok(locker);
        }
        catch (Exception exception)
        {
            msg = "Error updating Locker.";
            Log.Error(msg, exception);
            return BadRequest(msg);
        }
    }

}