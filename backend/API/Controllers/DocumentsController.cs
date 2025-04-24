using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;

namespace API.Controllers;
public class DocumentsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DocumentsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Document>>> Get()
    {
        var documents = await _unitOfWork.Documents.GetAllAsync();
        return _mapper.Map<List<Document>>(documents);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Document>> Get(int id)
    {
        var document = await _unitOfWork.Documents.GetByIdAsync(id);

        if (document is null)
            return NotFound();

        return _mapper.Map<Document>(document);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Document>> Post(Document oDocument)
    {
        var msg = string.Empty;
        try
        {
            var document = _mapper.Map<Document>(oDocument);
            _unitOfWork.Documents.Add(document);
            await _unitOfWork.SaveAsync();

            if (document is null)
                return BadRequest();

            oDocument.Id = document.Id;

            object[] obj = { document.Id, document.Description };
            msg = string.Format(Constants.MSG_DOCUMENT_ADDED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return CreatedAtAction(nameof(Post), new { id = oDocument.Id }, oDocument);
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_DOCUMENT_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Document>> Put([FromBody] Document oDocument)
    {
        var msg = string.Empty;
        try
        {
            if (oDocument is null)
                return NotFound();

            var document = _mapper.Map<Document>(oDocument);
            _unitOfWork.Documents.Update(document);
            await _unitOfWork.SaveAsync();

            object[] obj = { document.Id, document.Description };
            msg = string.Format(Constants.MSG_DOCUMENT_UPDATED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return oDocument;
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_DOCUMENT_UPDATED_ERROR;
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
            var document = await _unitOfWork.Documents.GetByIdAsync(id);

            if (document is null)
                return NotFound();

            _unitOfWork.Documents.Remove(document);
            await _unitOfWork.SaveAsync();

            object[] obj = { document.Id, document.Description };
            msg = string.Format(Constants.MSG_DOCUMENT_DELETED_SUCCESS, obj);
            Log.Logger.Information(msg);

            return NoContent();
        }
        catch (Exception exception)
        {
            msg = Constants.MSG_DOCUMENT_DELETED_ERROR;
            Log.Logger.Error(msg, exception);
            return BadRequest(msg);
        }
    }
}