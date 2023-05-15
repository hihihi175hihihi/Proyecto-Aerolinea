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
    public class MenuCategoriesController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public MenuCategoriesController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        // GET: api/MenuCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategories>>> GetMenuCategories()
        {
          if (_context.MenuCategories == null)
          {
              return NotFound();
          }
            return await _context.MenuCategories.ToListAsync();
        }

        // GET: api/MenuCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuCategories>> GetMenuCategories(int id)
        {
          if (_context.MenuCategories == null)
          {
              return NotFound();
          }
            var menuCategories = await _context.MenuCategories.FindAsync(id);

            if (menuCategories == null)
            {
                return NotFound();
            }

            return menuCategories;
        }

        // PUT: api/MenuCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuCategories(int id, MenuCategories menuCategories)
        {
            if (id != menuCategories.idCategoriesMenu)
            {
                return BadRequest();
            }

            _context.Entry(menuCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MenuCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuCategories>> PostMenuCategories(MenuCategories menuCategories)
        {
          if (_context.MenuCategories == null)
          {
              return Problem("Entity set 'Aerolinea_DesarrolloContext.MenuCategories'  is null.");
          }
            _context.MenuCategories.Add(menuCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuCategories", new { id = menuCategories.idCategoriesMenu }, menuCategories);
        }

        // DELETE: api/MenuCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuCategories(int id)
        {
            if (_context.MenuCategories == null)
            {
                return NotFound();
            }
            var menuCategories = await _context.MenuCategories.FindAsync(id);
            if (menuCategories == null)
            {
                return NotFound();
            }

            _context.MenuCategories.Remove(menuCategories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuCategoriesExists(int id)
        {
            return (_context.MenuCategories?.Any(e => e.idCategoriesMenu == id)).GetValueOrDefault();
        }
    }
}
