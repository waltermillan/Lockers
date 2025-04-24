using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers;

public class TestsController : BaseApiController
{
    private readonly Context _context;

    public TestsController(Context context)
    {
        _context = context;
    }

    [HttpGet("test-db-connection")]
    public async Task<IActionResult> TestDbConnection()
    {
        try
        {
            // Try open database conection
            await _context.Database.OpenConnectionAsync();
            var data = new { Code = 0, Message= "Successful connection to the database."};
            return Ok(data);
        }
        catch (Exception ex)
        {
            var data = new { Code = ex.InnerException.Data["Server Error Code"], Message = "Error connecting to the database", Error = ex.Message };
            return StatusCode(500, data);
        }
    }
}
