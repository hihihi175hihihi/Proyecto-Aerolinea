using API.Models;
using API.Models.ViewModelSP;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public VuelosController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/Vuelos
        [HttpGet]
        public async Task<ActionResult> GetVuelos()
        {
           var result= await _context.RunSpAsync<FiltrosVuelos>("FiltrosVuelos");
            result.DeserializeEscalasJson();
            return Ok(result);
        }

        // GET: api/Vuelos/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVuelos(int id)
        {
            var parameters = SqlParameterWrapper.Create(("@VUELO", id));
            var result = await _context.RunSpAsync<FiltrosVuelos>("FiltrosVuelos", parameters);
            result.DeserializeEscalasJson();
            return Ok(result.FirstOrDefault());
        }



        [Route("Filtros")]
        [HttpPost]
        public async Task<ActionResult> GetVuelosFiltered(filtrosParaVuelos filtros)
        {
            var parameters = SqlParameterWrapper.Create(
                ("@VUELO", filtros.idVuelo??null),
                ("@CIUDADORIGEN", filtros.CIUDAD_ORIGEN??null),
                ("@CIUDADDESTINO", filtros.CIUDAD_DESTINO??null),
                ("@PAISORIGEN", filtros.PAIS_ORIGEN??null),
                ("@PAISDESTINO", filtros.PAIS_DESTINO??null),
                ("@HASESCALAS", filtros.hasEscalas??null),
                ("@DIASEMANA", filtros.DiaSemana??null),
                ("@PRECIOMIN", filtros.PrecioMin??null),
                ("@PRECIOMAX", filtros.PrecioMax??null),
                ("@ORDENARPRECIOAS", filtros.ORDENARPRECIOAS??null),
                ("@idUsuario", filtros.idUsuario ?? null)
                );
           
            var result = await _context.RunSpAsync<FiltrosVuelos>("FiltrosVuelos", parameters);
            result.DeserializeEscalasJson();
            return Ok(result);
        }

        // PUT: api/Vuelos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVuelos(int id, Vuelos vuelos)
        {
            if (id != vuelos.idVuelo)
            {
                return BadRequest();
            }

            _context.Entry(vuelos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VuelosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(vuelos);
        }

        // POST: api/Vuelos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vuelos>> PostVuelos(Vuelos vuelos)
        {
          if (_context.Vuelos == null)
          {
              return Problem("Entity set 'Aerolinea_DesarrolloContext.Vuelos'  is null.");
          }
            _context.Vuelos.Add(vuelos);
            await _context.SaveChangesAsync();

            return  Ok(vuelos);
        }

        // DELETE: api/Vuelos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVuelos(int id)
        {
            if (_context.Vuelos == null)
            {
                return NotFound();
            }
            var vuelos = await _context.Vuelos.FindAsync(id);
            if (vuelos == null)
            {
                return NotFound();
            }

            _context.Vuelos.Remove(vuelos);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool VuelosExists(int id)
        {
            return (_context.Vuelos?.Any(e => e.idVuelo == id)).GetValueOrDefault();
        }
    }
}
