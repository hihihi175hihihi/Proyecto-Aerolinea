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
    public class AccessesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public AccessesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Accesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Access>>> GetAccesses()
        {
          if (_context.Access == null)
          {
              return NotFound();
          }
            return Ok(await _context.Access.ToListAsync());
        }

        // GET: api/Accesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Access>> GetAccess(int id)
        {
            var access = await _context.Access.FindAsync(id);

            if (access == null)
            {
                return NotFound();
            }

            return Ok(access);
        }

        // PUT: api/Accesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccess(int id, Access access)
        {
            if (id != access.idAccess)
            {
                return BadRequest("El acceso no es valido");
            }

            _context.Entry(access).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessExists(id))
                {
                    return NotFound("Error al actualizar el acceso");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(access);
        }

        // POST: api/Accesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Access>> PostAccess(Access access)
        {
          
            _context.Access.Add(access);
            await _context.SaveChangesAsync();

            return Ok(access);
        }

        // DELETE: api/Accesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccess(int id)
        {
            var access = await _context.Access.FindAsync(id);
            if (access == null)
            {
                return NotFound();
            }

            _context.Access.Remove(access);
            await _context.SaveChangesAsync();

            return Ok("Acceso eliminado");
        }

        private bool AccessExists(int id)
        {
            return (_context.Access?.Any(e => e.idAccess == id)).GetValueOrDefault();
        }
    }
}
