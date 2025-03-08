//using API.DTOs;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;
[ApiController]
[Route("api/lockers")] // Usamos el plural en la ruta para seguir la convención RESTful
public class LockerController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LockerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    [HttpPut("{id}")]
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
}