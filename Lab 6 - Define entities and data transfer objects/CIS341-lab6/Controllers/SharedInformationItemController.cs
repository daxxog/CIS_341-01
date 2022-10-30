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
    public class SharedInformationItemController : Controller
    {
        private readonly SqliteContext _context;

        public SharedInformationItemController(SqliteContext context)
        {
            _context = context;
        }

        // GET: SharedInformationItem
        public async Task<IActionResult> Index()
        {
            return _context.SharedInformationItems != null
                ? View(await _context.SharedInformationItems.ToListAsync())
                : Problem("Entity set 'SqliteContext.SharedInformationItems'  is null.");
        }

        // GET: SharedInformationItem/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedInformationItem == null)
            {
                return NotFound();
            }

            return View(sharedInformationItem);
        }

        // GET: SharedInformationItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SharedInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Details")] SharedInformationItem sharedInformationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sharedInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sharedInformationItem);
        }

        // GET: SharedInformationItem/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems.FindAsync(id);
            if (sharedInformationItem == null)
            {
                return NotFound();
            }

            return View(sharedInformationItem);
        }

        // POST: SharedInformationItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,Title,Details")] SharedInformationItem sharedInformationItem)
        {
            if (id != sharedInformationItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharedInformationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharedInformationItemExists(sharedInformationItem.Id))
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

            return View(sharedInformationItem);
        }

        // GET: SharedInformationItem/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedInformationItem == null)
            {
                return NotFound();
            }

            return View(sharedInformationItem);
        }

        // POST: SharedInformationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.SharedInformationItems == null)
            {
                return Problem("Entity set 'SqliteContext.SharedInformationItems'  is null.");
            }

            var sharedInformationItem = await _context.SharedInformationItems.FindAsync(id);
            if (sharedInformationItem != null)
            {
                _context.SharedInformationItems.Remove(sharedInformationItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharedInformationItemExists(long id)
        {
            return (_context.SharedInformationItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}