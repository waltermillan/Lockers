using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace API.Controllers;
[ApiController]
[Route("api/administrators")] // Usamos el plural en la ruta para seguir la convención RESTful
public class UserController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Login(string userName, string password)
    {
        try
        {
            // Intentamos autenticar al usuario
            var user = await _unitOfWork.Users.AuthenticateAsync(userName, password);

            // Si no encontramos el usuario, devolvemos un error
            if (user is null)
            {
                return BadRequest(new
                {
                    IsAuthenticated = false,
                    Message = "Incorrect username or password.",
                    ErrorCode = 400  // Código de error
                });
            }

            // Si el usuario existe, autenticación exitosa
            return Ok(new
            {
                IsAuthenticated = true,
                Message = "Successful authentication.",
                ErrorCode = 200  // Código de éxito
            });
        }
        catch (Exception exception)
        {
            // En caso de error, retornamos un error genérico
            Log.Logger.Error("Error authenticating user.", exception);

            return StatusCode(500, new
            {
                IsAuthenticated = false,
                Message = "There was a problem trying to authenticate the user.",
                ErrorCode = 500
            });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return _mapper.Map<List<User>>(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return _mapper.Map<User>(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Post(User oUser)
    {
        var msg = string.Empty;
        try
        {
            var user = _mapper.Map<User>(oUser);
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveAsync();

            if (user is null)
                return BadRequest();

            oUser.Id = user.Id;

            object[] obj = { user.Id, user.UserName, user.IdPerfil };
            msg = string.Format(Constants.MSG_USER_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oUser.Id }, oUser);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_USER_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Put([FromBody] User oUser)
    {
        var msg = string.Empty;
        try
        {
            if (oUser is null)
                return NotFound();

            var user = _mapper.Map<User>(oUser);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            object[] obj = { user.Id, user.UserName, user.IdPerfil };
            msg = string.Format(Constants.MSG_USER_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oUser;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_USER_UPDATED_ERROR;
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
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveAsync();

            object[] obj = { user.Id, user.UserName, user.IdPerfil };
            msg = string.Format(Constants.MSG_USER_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);
            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_USER_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}