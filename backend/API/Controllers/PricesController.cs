using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PricesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PricesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Price>>> Get()
    {
        var prices = await _unitOfWork.Prices.GetAllAsync();
        return _mapper.Map<List<Price>>(prices);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Price>> Get(int id)
    {
        var price = await _unitOfWork.Prices.GetByIdAsync(id);

        if (price is null)
            return NotFound();

        return _mapper.Map<Price>(price);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Price>> Post(Price oPrice)
    {
        var msg = string.Empty;
        try
        {
            var price = _mapper.Map<Price>(oPrice);
            _unitOfWork.Prices.Add(price);
            await _unitOfWork.SaveAsync();

            if (price is null)
                return BadRequest();

            oPrice.Id = price.Id;

            object[] obj = { price.Id, price.Value };
            msg = string.Format(Constants.MSG_PRICE_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oPrice.Id }, oPrice);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_PRICE_ADDED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Price>> Put([FromBody] Price oPrice)
    {
        var msg = string.Empty;
        try
        {
            if (oPrice is null)
                return NotFound();

            var price = _mapper.Map<Price>(oPrice);
            _unitOfWork.Prices.Update(price);
            await _unitOfWork.SaveAsync();

            object[] obj = { price.Id, price.Value };
            msg = string.Format(Constants.MSG_PRICE_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oPrice;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_PRICE_UPDATED_ERROR;
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
            var price = await _unitOfWork.Prices.GetByIdAsync(id);

            if (price is null)
                return NotFound();

            _unitOfWork.Prices.Remove(price);
            await _unitOfWork.SaveAsync();

            object[] obj = { price.Id, price.Value };
            msg = string.Format(Constants.MSG_PRICE_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_PRICE_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}