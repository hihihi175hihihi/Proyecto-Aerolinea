using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public UsuariosController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var result = await _context.Usuarios.Join(_context.Roles,
                usuario => usuario.idRol,
                rol => rol.idRol,
                (usuario, rol) => new 
                {
                    idUsuario = usuario.idUsuario,
                    Username = usuario.Username,
                    Password = usuario.Password,
                    Active = usuario.Active,
                    idRol = rol.idRol,
                    NombreRol = rol.Rol
                }).ToListAsync();
            return Ok(result);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.idUsuario)
            {
                return BadRequest("El id no es valido");
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound("Error el id no existe");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(usuarios);
        }
        
        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            usuarios.Active = true;
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return Ok(usuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return Ok("usuario eliminado");
        }

        private bool UsuariosExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.idUsuario == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("ListaAccesos/{id}")]
        public async Task<IActionResult> ListadoAccesos(int id)
        {

            var listaAccesos = _context.Usuarios.Where(user => user.idUsuario == id).Join(_context.Roles,
                usuario => usuario.idRol,
                rol => rol.idRol,
                (usuario, rol) => new
                {
                    idRol = rol.idRol
                }).ToList().Join(_context.AccessRoles,
                rol => rol.idRol,
                acc => acc.idRol,
                (rol, acc) => new
                {
                    idAccess = acc.idAccess
                }).ToList().Join(_context.Access,
                acc => acc.idAccess,
                accesos => accesos.idAccess,
                (acc, accesos) => new Access
                {
                    idAccess = accesos.idAccess,
                    Name = accesos.Name,
                    URL = accesos.URL
                }).ToList();

            return Ok(listaAccesos);
        }
    }
}