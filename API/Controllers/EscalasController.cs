using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscalasController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public EscalasController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Escalas
        [HttpGet]
        public async Task<IActionResult> GetEscalas()
        {
            var listado = _context.Escalas.ToList().Join(_context.Ciudades,
             e=>e.idCiudadEscala,
             c=>c.idCiudad,
             (e, c) => new
             {
                 idEscala = e.idEscala,
                 idVuelo = e.idVuelo,
                 idCiudadEscala=e.idCiudadEscala,
                 CiudadEscala=c.Ciudad,
                 DuracionEscala=e.DuracionEscala,
                 DuracionLlegada=e.DuracionLlegada
             }).ToList();


            return Ok(listado);
        }

        // GET: api/Escalas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Escalas>> GetEscalas(int id)
        {
            var escalas = await _context.Escalas.FindAsync(id);

            if (escalas == null)
            {
                return NotFound();
            }

            return Ok(escalas);
        }

        // PUT: api/Escalas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscalas(int id, Escalas escalas)
        {
            if (id != escalas.idEscala)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(escalas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscalasExists(id))
                {
                    return NotFound("No se encontro la Escala");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(escalas);
        }

        // POST: api/Escalas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Escalas>> PostEscalas(Escalas escalas)
        {
            _context.Escalas.Add(escalas);
            await _context.SaveChangesAsync();

            return Ok(escalas);
        }

        // DELETE: api/Escalas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscalas(int id)
        {
            var escalas = await _context.Escalas.FindAsync(id);
            if (escalas == null)
            {
                return NotFound("El id no coincide, intente de nuevo");
            }

            _context.Escalas.Remove(escalas);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool EscalasExists(int id)
        {
            return (_context.Escalas?.Any(e => e.idEscala == id)).GetValueOrDefault();
        }
    }
}
