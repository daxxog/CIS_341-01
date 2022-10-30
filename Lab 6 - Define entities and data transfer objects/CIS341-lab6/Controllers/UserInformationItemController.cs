using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab6.Data;
using CIS341_lab6.Data.Entities;

namespace CIS341_lab6.Controllers
{
    public class UserInformationItemController : Controller
    {
        private readonly SqliteContext _context;

        public UserInformationItemController(SqliteContext context)
        {
            _context = context;
        }

        // GET: UserInformationItem
        public async Task<IActionResult> Index()
        {
            var sqliteContext = _context.UserInformationItems.Include(u => u.User);
            return View(await sqliteContext.ToListAsync());
        }

        // GET: UserInformationItem/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInformationItem == null)
            {
                return NotFound();
            }

            return View(userInformationItem);
        }

        // GET: UserInformationItem/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,UserId,Title,Details")] UserInformationItem userInformationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userInformationItem.UserId);
            return View(userInformationItem);
        }

        // GET: UserInformationItem/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems.FindAsync(id);
            if (userInformationItem == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userInformationItem.UserId);
            return View(userInformationItem);
        }

        // POST: UserInformationItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,UserId,Title,Details")] UserInformationItem userInformationItem)
        {
            if (id != userInformationItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInformationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInformationItemExists(userInformationItem.Id))
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

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userInformationItem.UserId);
            return View(userInformationItem);
        }

        // GET: UserInformationItem/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInformationItem == null)
            {
                return NotFound();
            }

            return View(userInformationItem);
        }

        // POST: UserInformationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.UserInformationItems == null)
            {
                return Problem("Entity set 'SqliteContext.UserInformationItems'  is null.");
            }

            var userInformationItem = await _context.UserInformationItems.FindAsync(id);
            if (userInformationItem != null)
            {
                _context.UserInformationItems.Remove(userInformationItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInformationItemExists(long id)
        {
            return (_context.UserInformationItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}