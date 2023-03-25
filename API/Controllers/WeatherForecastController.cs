using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public WeatherForecastController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Roles listado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargos>>> GetTblRoles()
        {
            return Ok(await _context.Cargos.ToListAsync());
        }
    }
}