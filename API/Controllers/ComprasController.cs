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
    public class ComprasController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public ComprasController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> GetCompras()
        {
            return await _context.Compras.ToListAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compras>> GetCompras(int id)
        {
            var compras = await _context.Compras.FindAsync(id);

            if (compras == null)
            {
                return NotFound();
            }

            return Ok(compras);
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, Compras compras)
        {
            if (id != compras.idCompra)
            {
                return BadRequest("El id no coincide");
            }

            _context.Entry(compras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasExists(id))
                {
                    return NotFound("No se encontro la compra");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(compras);
        }

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompras(Compras compras)
        { 
            _context.Compras.Add(compras);
            await _context.SaveChangesAsync();

            return Ok(compras);
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompras(int id)
        {
            
            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound("No se encontro la orden de compra");
            }

            _context.Compras.Remove(compras);
            await _context.SaveChangesAsync();

            return Ok("Compra eliminada");
        }

        private bool ComprasExists(int id)
        {
            return (_context.Compras?.Any(e => e.idCompra == id)).GetValueOrDefault();
        }
    }
}
