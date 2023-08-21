﻿using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public CiudadesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Ciudades
        [HttpGet]
        public async Task<IActionResult> GetCiudades()
        {
            var listado = _context.Ciudades.ToList().Join(_context.Paises,
             c=>c.idPais,
             p => p.idPais,
             (c, p) => new
             {
                 idCiudad=c.idCiudad,
                 Ciudad=c.Ciudad,
                 idPais = c.idPais,
                 Pais = p.Pais

             }).ToList();


            return Ok(listado);
        }

        // GET: api/Ciudades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudades>> GetCiudades(int id)
        {
            var ciudades = await _context.Ciudades.FindAsync(id);

            if (ciudades == null)
            {
                return NotFound();
            }

            return Ok(ciudades);
        }

        // PUT: api/Ciudades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudades(int id, Ciudades ciudades)
        {
            if (id != ciudades.idCiudad)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(ciudades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadesExists(id))
                {
                    return NotFound("No se encontro la ciudad");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(ciudades);
        }

        // POST: api/Ciudades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ciudades>> PostCiudades(Ciudades ciudades)
        {
            _context.Ciudades.Add(ciudades);
            await _context.SaveChangesAsync();

            return Ok(ciudades);
        }

        // DELETE: api/Ciudades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudades(int id)
        {
            var ciudades = await _context.Ciudades.FindAsync(id);
            if (ciudades == null)
            {
                return NotFound("El id no coincide, intente de nuevo");
            }

            _context.Ciudades.Remove(ciudades);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool CiudadesExists(int id)
        {
            return (_context.Ciudades?.Any(e => e.idCiudad == id)).GetValueOrDefault();
        }
    }
}
