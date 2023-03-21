using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ModelContext _context;

        public WeatherForecastController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Roles listado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetTblRoles()
        {
            return Ok(await _context.Cargos.ToListAsync());
        }
    }
}