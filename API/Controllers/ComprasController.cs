using API.Models;
using API.Models.ViewModelSP;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // <summary>
        /// Este metodo retornara el listado de compras realizadas:Administrador
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetCompras()
        {
            var result = await _context.RunSpAsync<ReporteCompras>("ReporteCompras");
            return Ok(result);
        }
        // <summary>
        /// Este metodo retornara el listado de compras realizadas:Cliente
        /// </summary>
        [HttpGet("{username}")]
        public async Task<ActionResult> GetCompras(string username)
        {
            var parameters = SqlParameterWrapper.Create(("@Username", username));
            var result = await _context.RunSpAsync<ReporteCompras>("ReporteCompras", parameters);
            return Ok(result);
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
