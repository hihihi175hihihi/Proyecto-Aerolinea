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
    public class CargoController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public CargoController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Cargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargos>>> GetCargo()
        {
            if (_context.Cargos == null)
            {
                return NotFound();
            }
            return Ok(await _context.Cargos.ToListAsync());
        }

        // GET: api/Cargo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargos>> GetCargo(int id)
        {

            var Cargo = await _context.Cargos.FindAsync(id);

            if (Cargo == null)
            {
                return NotFound();
            }

            return Ok(Cargo);
        }

        // PUT: api/Cargo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, Cargos Cargo)
        {
            if (id != Cargo.idCargo)
            {
                return BadRequest("El id no es valido");
            }

            _context.Entry(Cargo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
                {
                    return NotFound("No se encontro el Cargo");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(Cargo);
        }

        // POST: api/Cargo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cargos>> PostRoles(Cargos Cargo)
        {
            _context.Cargos.Add(Cargo);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCargo", new { id = Cargo.idRol }, roles);
            return Ok(Cargo);
        }

        // DELETE: api/Cargo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {

            var Cargo = await _context.Cargos.FindAsync(id);
            if (Cargo == null)
            {
                return NotFound("El id no es valido");
            }

            _context.Cargos.Remove(Cargo);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool CargoExists(int id)
        {
            return (_context.Cargos?.Any(e => e.idCargo == id)).GetValueOrDefault();
        }
    }
}
