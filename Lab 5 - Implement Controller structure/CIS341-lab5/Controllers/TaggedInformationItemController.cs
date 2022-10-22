using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab5.Data;
using CIS341_lab5.Data.Entities;

namespace CIS341_lab5.Controllers
{
    public class TaggedInformationItemController : Controller
    {
        private readonly SqliteContext _context;

        public TaggedInformationItemController(SqliteContext context)
        {
            _context = context;
        }

        // GET: TaggedInformationItem
        public async Task<IActionResult> Index()
        {
            var sqliteContext = _context.TaggedInformationItems.Include(t => t.InformationItemSharedInformationItem);
            return View(await sqliteContext.ToListAsync());
        }

        // GET: TaggedInformationItem/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TaggedInformationItems == null)
            {
                return NotFound();
            }

            var taggedInformationItem = await _context.TaggedInformationItems
                .Include(t => t.InformationItemSharedInformationItem)
                .FirstOrDefaultAsync(m => m.TagName == id);
            if (taggedInformationItem == null)
            {
                return NotFound();
            }

            return View(taggedInformationItem);
        }

        // GET: TaggedInformationItem/Create
        public IActionResult Create()
        {
            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details");
            return View();
        }

        // POST: TaggedInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("TagName,InformationItemId")] TaggedInformationItem taggedInformationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taggedInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details",
                taggedInformationItem.InformationItemId);
            return View(taggedInformationItem);
        }

        // GET: TaggedInformationItem/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TaggedInformationItems == null)
            {
                return NotFound();
            }

            var taggedInformationItem = await _context.TaggedInformationItems.FindAsync(id);
            if (taggedInformationItem == null)
            {
                return NotFound();
            }

            ViewData["InformationItemId"] = new SelectList(_context.SharedInformationItems, "Id", "Details",
                taggedInformationItem.InformationItemId);
            return View(taggedInformationItem);
        }

        // POST: TaggedInformationItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind("TagName,InformationItemId")] TaggedInformationItem taggedInformationItem)
        {
            if (id != taggedInformationItem.TagName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taggedInformationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaggedInformationItemExists(taggedInformationItem.TagName))
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
                taggedInformationItem.InformationItemId);
            return View(taggedInformationItem);
        }

        // GET: TaggedInformationItem/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TaggedInformationItems == null)
            {
                return NotFound();
            }

            var taggedInformationItem = await _context.TaggedInformationItems
                .Include(t => t.InformationItemSharedInformationItem)
                .FirstOrDefaultAsync(m => m.TagName == id);
            if (taggedInformationItem == null)
            {
                return NotFound();
            }

            return View(taggedInformationItem);
        }

        // POST: TaggedInformationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TaggedInformationItems == null)
            {
                return Problem("Entity set 'SqliteContext.TaggedInformationItems'  is null.");
            }

            var taggedInformationItem = await _context.TaggedInformationItems.FindAsync(id);
            if (taggedInformationItem != null)
            {
                _context.TaggedInformationItems.Remove(taggedInformationItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaggedInformationItemExists(string id)
        {
            return (_context.TaggedInformationItems?.Any(e => e.TagName == id)).GetValueOrDefault();
        }
    }
}