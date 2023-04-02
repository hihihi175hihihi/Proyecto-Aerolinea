using API.Models;
using API.Models.ViewModelSP;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public DashBoardController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        [Route("OcupacionxDia")]
        [HttpGet]
        public async Task<IActionResult> GetOcupacionxDia()
        {
            var parameters = SqlParameterWrapper.Create(("@FECHAACTUAL", DateTime.Now.ToString("yyyy-MM-dd")));
            var result = await _context.RunSpAsync<TotalOcupacionxDia>("TotalOcupacionxDia", parameters);
            return Ok(result);

        }

        [Route("TotalVentasAnual")]
        [HttpGet]
        public async Task<IActionResult> GetVentasAnual()
        {
            var parameters = SqlParameterWrapper.Create(("@YEAR", DateTime.Now.ToString("yyyy")));
            var result = await _context.RunSpAsync<TotalVentasAnual>("TotalVentasAnual", parameters);
            return Ok(result);

        }
        [Route("TotalVentasxDia")]
        [HttpGet]
        public async Task<IActionResult> GetTotalVentasxDia()
        {
            var parameters = SqlParameterWrapper.Create(("@FECHAACTUAL", DateTime.Now.ToString("yyyy-MM-dd")));
            var result = await _context.RunSpAsync<TotalVentasxDia>("TotalVentasxDia", parameters);
            return Ok(result);

        }
    }
}
