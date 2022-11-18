using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab8.Data;
using CIS341_lab8.Data.Entities;

namespace CIS341_lab8.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly SqliteContext _context;

        public FavoriteController(SqliteContext context)
        {
            _context = context;
        }

        // GET: Favorites
        [Route("/Favorites")]
        public async Task<IActionResult> Index()
        {
            var sqliteContext = _context.Favorites.Include(f => f.InformationItemSharedInformationItem)
                .Include(f => f.User);
            return View(await sqliteContext.ToListAsync());
        }

        // GET: Favorite/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites
                .Include(f => f.InformationItemSharedInformationItem)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // GET: Favorite/Create
        public IActionResult Create()
        {
            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Favorite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,InformationItemId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details",
                favorite.InformationItemId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", favorite.UserId);
            return View(favorite);
        }

        // GET: Favorite/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details",
                favorite.InformationItemId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", favorite.UserId);
            return View(favorite);
        }

        // POST: Favorite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,InformationItemId")] Favorite favorite)
        {
            if (id != favorite.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteExists(favorite.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details",
                favorite.InformationItemId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", favorite.UserId);
            return View(favorite);
        }

        // GET: Favorite/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites
                .Include(f => f.InformationItemSharedInformationItem)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: Favorite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Favorites == null)
            {
                return Problem("Entity set 'SqliteContext.Favorites'  is null.");
            }

            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteExists(long id)
        {
            return (_context.Favorites?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}