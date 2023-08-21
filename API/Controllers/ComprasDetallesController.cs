using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.ViewModelSP;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasDetalleController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public ComprasDetalleController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Este metodo retornara el detalle de una compra la cual se mandara como parametro(id).
        /// </summary>
        /// <param name="id">id de la compra</param>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetComprasDetalle(int id)
        {
            var parameters = SqlParameterWrapper.Create(("@Compra", id));
            var result = await _context.RunSpAsync<DetalleCompra>("DetalleCompra", parameters);
            result.DeserializeEscalasJson();
            return Ok(result);
        }

        
        // POST: api/ComprasDetalle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComprasDetalle>> PostComprasDetalle(ComprasDetalle comprasDetalle)
        {
          
            _context.ComprasDetalle.Add(comprasDetalle);
            await _context.SaveChangesAsync();

            return Ok(comprasDetalle);
        }

        // DELETE: api/ComprasDetalle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComprasDetalle(int id)
        {
            if (_context.ComprasDetalle == null)
            {
                return NotFound();
            }
            var comprasDetalle = await _context.ComprasDetalle.FindAsync(id);
            if (comprasDetalle == null)
            {
                return NotFound();
            }

            _context.ComprasDetalle.Remove(comprasDetalle);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool ComprasDetalleExists(int id)
        {
            return (_context.ComprasDetalle?.Any(e => e.idComprasDetalle == id)).GetValueOrDefault();
        }
    }
}
