using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public PagosController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagos>>> GetPagos()
        {
            return Ok(await _context.Pagos.ToListAsync());
        }

        // GET: api/Pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagos>> GetPagos(int id)
        {   
            var pagos = await _context.Pagos.FindAsync(id);

            if (pagos == null)
            {
                return NotFound();
            }

            return Ok(pagos);
        }

        // PUT: api/Pagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagos(int id, Pagos pagos)
        {
            if (id != pagos.idPago)
            {
                return BadRequest("El id no coincide");
            }

            _context.Entry(pagos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagosExists(id))
                {
                    return NotFound("No se Encontro el pago");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(pagos);
        }

        // POST: api/Pagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagos>> PostPagos(Pagos pagos)
        {
          
            _context.Pagos.Add(pagos);
            await _context.SaveChangesAsync();

            return Ok(pagos);
        }

        // DELETE: api/Pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagos(int id)
        { 
            var pagos = await _context.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return NotFound("No se encontro el pago");
            }

            _context.Pagos.Remove(pagos);
            await _context.SaveChangesAsync();

            return Ok("Pago eliminado");
        }

        private bool PagosExists(int id)
        {
            return (_context.Pagos?.Any(e => e.idPago == id)).GetValueOrDefault();
        }
    }
}
