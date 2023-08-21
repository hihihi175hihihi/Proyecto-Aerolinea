﻿using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public RolesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return Ok(await _context.Roles.ToListAsync());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
          
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, Roles roles)
        {
            if (id != roles.idRol)
            {
                return BadRequest("El id no coincide, intente de nuevo C:");
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
                {
                    return NotFound("No se encontro el Rol");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(roles);
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetRoles", new { id = roles.idRol }, roles);
            return Ok(roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
      
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound("El id no coincide, intente de nuevo C:");
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool RolesExists(int id)
        {
            return (_context.Roles?.Any(e => e.idRol == id)).GetValueOrDefault();
        }
    }
}
