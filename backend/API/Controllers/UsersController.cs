using API.DTOs;
using API.Responses;
using API.Services;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserDTOService _userDtoService;

    public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, UserDTOService userDtoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _userDtoService = userDtoService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] LoginRequest request)
    {
        var usr = request.Usr.ToUpper();
        var psw = request.Psw;

        try
        {
            var user = await _unitOfWork.Users.GetByUserAsync(usr);

            if (user is null || !_passwordHasher.VerifyPassword(psw, user.Password))
            {
                Log.Logger.Information($"Login attempt failed for user: {usr}");
                return Unauthorized(new { Code = 401, Message = "Invalid username or password" });
            }

            Log.Logger.Information($"User '{usr}' authenticated successfully.");

            var rol = await _unitOfWork.Roles.GetByIdAsync(user.IdPerfil);

            var data = new
            {
                user.Id,
                user.UserName,
                user.IdPerfil,
                rol.Description
            };

            return Ok(ApiResponseFactory.Success<object>(data, "User authenticated successfully"));
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Authentication error", ex);
            return StatusCode(500, ApiResponseFactory.Fail<object>($"There was an issue authenticating the user. Details ${ex.Message}"));
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
    {
        var users = await _userDtoService.GetAllAsync();
        return _mapper.Map<List<UserDTO>>(users);
    }
    //public async Task<ActionResult<IEnumerable<User>>> Get()
    //{
    //    var users = await _unitOfWork.Users.GetAllAsync();
    //    return _mapper.Map<List<User>>(users);
    //}

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO>> Get(int id)
    {
        var user = await _userDtoService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return _mapper.Map<UserDTO>(user);
    }
    //{
    //    var user = await _unitOfWork.Users.GetByIdAsync(id);

    //    if (user is null)
    //        return NotFound();

    //    return _mapper.Map<User>(user);
    //}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Post(User oUser)
    {
        var msg = string.Empty;
        try
        {
            var user = _mapper.Map<User>(oUser);

            user.Password = _passwordHasher.HashPassword(user.Password);

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

    [HttpPut]
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

            user.Password = _passwordHasher.HashPassword(user.Password);

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