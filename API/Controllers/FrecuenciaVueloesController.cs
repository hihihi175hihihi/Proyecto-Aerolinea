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
    public class FrecuenciaVueloesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public FrecuenciaVueloesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/FrecuenciaVueloes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrecuenciaVuelo>>> GetFrecuenciaVuelos()
        {
            return Ok(await _context.FrecuenciaVuelos.ToListAsync());
        }

        // GET: api/FrecuenciaVueloes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FrecuenciaVuelo>> GetFrecuenciaVuelo(int id)
        {
          var frecuenciaVuelo = await _context.FrecuenciaVuelos.FindAsync(id);

            if (frecuenciaVuelo == null)
            {
                return NotFound();
            }

            return Ok(frecuenciaVuelo);
        }

        // PUT: api/FrecuenciaVueloes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrecuenciaVuelo(int id, FrecuenciaVuelo frecuenciaVuelo)
        {
            if (id != frecuenciaVuelo.idFrecuenciaVuelo)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(frecuenciaVuelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrecuenciaVueloExists(id))
                {
                    return NotFound("No se encontro el cliente");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(frecuenciaVuelo);
        }

        // POST: api/FrecuenciaVueloes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FrecuenciaVuelo>> PostFrecuenciaVuelo(FrecuenciaVuelo frecuenciaVuelo)
        {
            _context.FrecuenciaVuelos.Add(frecuenciaVuelo);
            await _context.SaveChangesAsync();

            return Ok(frecuenciaVuelo);
        }

        // DELETE: api/FrecuenciaVueloes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrecuenciaVuelo(int id)
        { 
            var frecuenciaVuelo = await _context.FrecuenciaVuelos.FindAsync(id);
            if (frecuenciaVuelo == null)
            {
                return NotFound("El id no coincide, intente de nuevo");
            }

            _context.FrecuenciaVuelos.Remove(frecuenciaVuelo);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool FrecuenciaVueloExists(int id)
        {
            return (_context.FrecuenciaVuelos?.Any(e => e.idFrecuenciaVuelo == id)).GetValueOrDefault();
        }
    }
}
