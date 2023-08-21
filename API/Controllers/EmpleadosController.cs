﻿using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public EmpleadosController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult> GetEmpleados()
        {
            var listado = _context.Empleados.ToList().Join(_context.Usuarios,
             e => e.idUsuario,
             u => u.idUsuario,
             (e, u) => new
             {
                 idEmpleado = e.idEmpleado,
                 CodigoEmpleado = e.CodigoEmpleado,
                 idUsuario = e.idUsuario,
                 Username=u.Username,
                 Nombres = e.Nombres,
                 Apellidos = e.Apellidos,
                 Direccion = e.Direccion,
                 Telefono = e.Telefono,
                 idCargo = e.idCargo
             }).Join(_context.Cargos,
              e => e.idCargo,
             c => c.idCargo,
             (e, c) => new
             {
                 idEmpleado = e.idEmpleado,
                 CodigoEmpleado = e.CodigoEmpleado,
                 idUsuario = e.idUsuario,
                 Username = e.Username,
                 Nombres = e.Nombres,
                 Apellidos = e.Apellidos,
                 Direccion = e.Direccion,
                 Telefono = e.Telefono,
                 idCargo = e.idCargo,
                 Cargo=c.Cargo
             }).ToList();


            return Ok(listado);
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleados>> GetEmpleados(int id)
        {
          var empleados = await _context.Empleados.FindAsync(id);

            if (empleados == null)
            {
                return NotFound();
            }

            return Ok(empleados);
        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleados(int id, Empleados empleados)
        {
            if (id != empleados.idEmpleado)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(empleados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadosExists(id))
                {
                    return NotFound("No se encontro el cliente");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(empleados);
        }

        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleados>> PostEmpleados(Empleados empleados)
        {
            _context.Empleados.Add(empleados);
            await _context.SaveChangesAsync();
            var oUser = await _context.Usuarios.FindAsync(empleados.idUsuario);
            oUser.Active = true;
            _context.Entry(oUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(empleados);
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleados(int id)
        {
            if (_context.Empleados == null)
            {
                return NotFound();
            }
            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados == null)
            {
                return NotFound("Error al eliminar");
            }

            _context.Empleados.Remove(empleados);
            await _context.SaveChangesAsync();

            return Ok("Registro Eliminado");
        }

        private bool EmpleadosExists(int id)
        {
            return (_context.Empleados?.Any(e => e.idEmpleado == id)).GetValueOrDefault();
        }
    }
}
