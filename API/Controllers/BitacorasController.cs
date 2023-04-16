using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacorasController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public BitacorasController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Bitacoras
        [HttpGet]
        public async Task<IActionResult> GetBitacoras()
        {
            return Ok(await _context.Bitacoras.ToListAsync());
        }

        // GET: api/Bitacoras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bitacoras>> GetBitacoras(int id)
        {
            if (_context.Bitacoras == null)
            {
                return NotFound();
            }
            var bitacoras = await _context.Bitacoras.FindAsync(id);

            if (bitacoras == null)
            {
                return NotFound();
            }

            return Ok(bitacoras);
        }
    }
}
