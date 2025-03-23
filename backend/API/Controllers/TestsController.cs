using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
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
                return Ok("Successful connection to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error connecting to database\r\n: {ex.Message}");
            }
        }
    }
}
