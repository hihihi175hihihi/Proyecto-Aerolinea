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
    public class ComprasDetallesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public ComprasDetallesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/ComprasDetalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComprasDetalle>>> GetComprasDetalles()
        {
          if (_context.ComprasDetalles == null)
          {
              return NotFound();
          }
            return Ok(await _context.ComprasDetalles.ToListAsync());
        }

        // GET: api/ComprasDetalles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComprasDetalle>> GetComprasDetalle(int id)
        {
          if (_context.ComprasDetalles == null)
          {
              return NotFound();
          }
            var comprasDetalle = await _context.ComprasDetalles.FindAsync(id);

            if (comprasDetalle == null)
            {
                return NotFound();
            }

            return Ok(comprasDetalle);
        }

        // PUT: api/ComprasDetalles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComprasDetalle(int id, ComprasDetalle comprasDetalle)
        {
            if (id != comprasDetalle.idComprasDetalle)
            {
                return BadRequest();
            }
            
            _context.Entry(comprasDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasDetalleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(comprasDetalle);
        }

        // POST: api/ComprasDetalles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComprasDetalle>> PostComprasDetalle(ComprasDetalle comprasDetalle)
        {
          if (_context.ComprasDetalles == null)
          {
              return Problem("Entity set 'Aerolinea_DesarrolloContext.ComprasDetalles'  is null.");
          }
            _context.ComprasDetalles.Add(comprasDetalle);
            await _context.SaveChangesAsync();

            return Ok(comprasDetalle);
        }

        // DELETE: api/ComprasDetalles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComprasDetalle(int id)
        {
            if (_context.ComprasDetalles == null)
            {
                return NotFound();
            }
            var comprasDetalle = await _context.ComprasDetalles.FindAsync(id);
            if (comprasDetalle == null)
            {
                return NotFound();
            }

            _context.ComprasDetalles.Remove(comprasDetalle);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool ComprasDetalleExists(int id)
        {
            return (_context.ComprasDetalles?.Any(e => e.idComprasDetalle == id)).GetValueOrDefault();
        }
    }
}
