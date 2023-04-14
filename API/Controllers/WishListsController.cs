using API.Models;
using API.Models.ViewModelSP;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListsController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public WishListsController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/WishLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWishList(int id)
        {
            var parameters = SqlParameterWrapper.Create(("@Usuario", id));
            var result = await _context.RunSpAsync<WishListxUsuario>("WishListxUsuario", parameters);
            result.DeserializeEscalasJson();
            return Ok(result);
        }
        // POST: api/WishLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WishList>> PostWishList(WishList wishList)
        {
            var result = await _context.WishLists.AnyAsync(x => x.idVuelo == wishList.idVuelo && x.idUsuario == wishList.idUsuario);
            if (result)
            {
                return BadRequest("El vuelo ya se encuentra en la lista de deseos");
            }
            _context.WishLists.Add(wishList);
            await _context.SaveChangesAsync();

            return Ok(wishList);
        }

        // DELETE: api/WishLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {
            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList == null)
            {
                return NotFound("El id no coincide, intente de nuevo");
            }

            _context.WishLists.Remove(wishList);
            await _context.SaveChangesAsync();

            return Ok("Registro eliminado");
        }

        private bool WishListExists(int id)
        {
            return (_context.WishLists?.Any(e => e.idWishList == id)).GetValueOrDefault();
        }
    }
}
