using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace API.Controllers;
[ApiController]
[Route("api/roles")] // Usamos el plural en la ruta para seguir la convención RESTful
public class RoleController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoleController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Core.Entities.Role>>> Get()
    {
        var role = await _unitOfWork.Roles.GetAllAsync();
        return _mapper.Map<List<Core.Entities.Role>>(role);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Role>> Get(int id)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id);

        if (role is null)
            return NotFound();

        return _mapper.Map<Role>(role);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Role>> Post(Role oRole)
    {
        var msg = string.Empty;
        try
        {
            var role = _mapper.Map<Role>(oRole);
            _unitOfWork.Roles.Add(role);
            await _unitOfWork.SaveAsync();

            if (role is null)
                return BadRequest();

            oRole.Id = role.Id;

            object[] obj = { role.Id, role.Description };
            msg = string.Format(Constants.MSG_ROLE_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oRole.Id }, oRole);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_ROLE_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Role>> Put([FromBody] Role oRole)
    {
        var msg = string.Empty;
        try
        {
            if (oRole is null)
                return NotFound();

            var role = _mapper.Map<Role>(oRole);
            _unitOfWork.Roles.Update(role);
            await _unitOfWork.SaveAsync();

            object[] obj = { role.Id, role.Description };
            msg = string.Format(Constants.MSG_ROLE_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oRole;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_ROLE_UPDATED_ERROR;
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
            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role is null)
                return NotFound();

            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.SaveAsync();

            object[] obj = { role.Id, role.Description };
            msg = string.Format(Constants.MSG_ROLE_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_ROLE_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}