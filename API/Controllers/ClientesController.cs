using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public ClientesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
          
            return Ok(await _context.Clientes.ToListAsync());
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return NotFound();
            }

            return Ok(clientes);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            if (id != clientes.idCliente)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound("No se encontro el cliente");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(clientes);
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return Ok(clientes);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {  
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound("Error al eliminar");
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool ClientesExists(int id)
        {
            return (_context.Clientes?.Any(e => e.idCliente == id)).GetValueOrDefault();
        }
    }
}
