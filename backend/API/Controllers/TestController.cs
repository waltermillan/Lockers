using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Context _context;

        public TestController(Context context)
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
                return Ok("Conexión exitosa a la base de datos.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error connecting to database\r\n: {ex.Message}");
            }
        }
    }
}
