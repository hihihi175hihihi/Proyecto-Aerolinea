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
        /// Este metodo retornara el listado de compras realizadas por cada mes en el anio actual:Administrador
        /// </summary>
        [Route("GetComprasMesActual")]
        [HttpGet]
        public async Task<ActionResult> GetComprasMesActual()
        {
            var parameters = SqlParameterWrapper.Create(("@Anio", Convert.ToInt32(DateTime.Now.ToString("yyyy"))));
            var result = await _context.RunSpAsync<ComprasPorMesAnio>("GetTotalComprasPorMes",parameters);
            var resultado = new List<ComprasPorMesAnioViewModel>();
            foreach (var item in result)
            {
                resultado.Add(new ComprasPorMesAnioViewModel()
                {
                    Mes = GetMonthNameFromNumber(item.Mes.ToString()),
                    Total=item.Total
                });  
            }
            return Ok(resultado);
        }
        public static string GetMonthNameFromNumber(string MonthNumber)
        {
            return MonthNumber switch
            {
                "1" => "Enero",
                "2" => "Febrero",
                "3" => "Marzo",
                "4" => "Abril",
                "5" => "Mayo",
                "6" => "Junio",
                "7" => "Julio",
                "8" => "Agosto",
                "9" => "Septiembre",
                "10" => "Octubre",
                "11" => "Noviembre",
                "12" => "Diciembre"
            };
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
