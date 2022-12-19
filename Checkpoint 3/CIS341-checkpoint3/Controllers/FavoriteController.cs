using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_checkpoint3.Data;
using CIS341_checkpoint3.Data.Entities;
using CIS341_checkpoint3.Models;

namespace CIS341_checkpoint3.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly SqliteContext _context;
        private readonly Func<AuthorizationStatus> _getAuthorizationStatus;

        public FavoriteController(SqliteContext context)
        {
            _context = context;
            _getAuthorizationStatus = () => (AuthorizationStatus)HttpContext.Items["AuthorizationStatus"];
        }

        // GET: Favorites
        [Route("/Favorites")]
        public async Task<IActionResult> Index()
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            var sqliteContext = _context.Favorites.Where(m => m.UserId == authStatus.UserId)
                .Include(f => f.InformationItemSharedInformationItem)
                .Include(f => f.User);
            return View(await sqliteContext.ToListAsync());
        }

        // POST: Favorite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string? ReturnTo)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (_context.Favorites == null)
            {
                return Problem("Entity set 'SqliteContext.Favorites'  is null.");
            }

            var favorite = await _context.Favorites.Where(m => m.UserId == authStatus.UserId)
                .Where(m => m.InformationItemId == id).FirstOrDefaultAsync();
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }

            await _context.SaveChangesAsync();

            if (ReturnTo == "SharedInformationItem")
            {
                return RedirectToAction("Details", ReturnTo,
                    new RouteValueDictionary { { "Id", favorite.InformationItemId.ToString() } });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Favorite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformationItemId")] Favorite favorite)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            favorite.UserId = authStatus.UserId;

            if (ModelState.IsValid)
            {
                var maybeExistsFavorite = await _context.Favorites.Where(m => m.UserId == authStatus.UserId)
                    .Where(m => m.InformationItemId == favorite.InformationItemId).FirstOrDefaultAsync();

                if (maybeExistsFavorite == null)
                {
                    _context.Add(favorite);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Details", "SharedInformationItem",
                new RouteValueDictionary { { "Id", favorite.InformationItemId.ToString() } });
        }

        // GET: Favorite/Create
        // used for login page redirect
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Index", "Tag");
        }
    }
}