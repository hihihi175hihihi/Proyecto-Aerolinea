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
    public class PaisesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public PaisesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paises>>> GetPaises()
        {
            return await _context.Paises.ToListAsync();
        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paises>> GetPaises(int id)
        { 
            var paises = await _context.Paises.FindAsync(id);

            if (paises == null)
            {
                return NotFound();
            }

            return Ok(paises);
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaises(int id, Paises paises)
        {
            if (id != paises.idPais)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(paises).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisesExists(id))
                {
                    return NotFound("no se encontro el pais");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(paises);
        }

        // POST: api/Paises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paises>> PostPaises(Paises paises)
        {
            _context.Paises.Add(paises);
            await _context.SaveChangesAsync();

            return Ok(paises);
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaises(int id)
        {
            var paises = await _context.Paises.FindAsync(id);
            if (paises == null)
            {
                return NotFound("El id no coincide, intente de nuevo");
            }

            _context.Paises.Remove(paises);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool PaisesExists(int id)
        {
            return (_context.Paises?.Any(e => e.idPais == id)).GetValueOrDefault();
        }
    }
}
