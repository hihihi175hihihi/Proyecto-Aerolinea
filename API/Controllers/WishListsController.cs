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
    public class WishListsController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public WishListsController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/WishLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishList>>> GetWishLists()
        {
            return Ok(await _context.WishLists.ToListAsync());
        }

        // GET: api/WishLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishList>> GetWishList(int id)
        {
          var wishList = await _context.WishLists.FindAsync(id);

            if (wishList == null)
            {
                return NotFound();
            }

            return Ok(wishList);
        }

        // PUT: api/WishLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishList(int id, WishList wishList)
        {
            if (id != wishList.idWishList)
            {
                return BadRequest("El id no coincide, intente de nuevo");
            }

            _context.Entry(wishList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListExists(id))
                {
                    return NotFound("No se encontro el cliente");
                }
                else
                {
                    return BadRequest("Error al actualizar");
                }
            }

            return Ok(wishList);
        }

        // POST: api/WishLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WishList>> PostWishList(WishList wishList)
        {
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
