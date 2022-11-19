using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_lab8.Models;
using System.ComponentModel.DataAnnotations;
using CIS341_lab8.Data;
using CIS341_lab8.Data.Entities;

namespace CIS341_lab8.Controllers
{
    [Authorize]
    public class SharedInformationItemController : Controller
    {
        private readonly SqliteContext _context;
        private readonly Func<SharedInformationItemCreateUpdateModel, List<TaggedInformationItem>> _deriveTags;

        public SharedInformationItemController(SqliteContext context)
        {
            _context = context;
            _deriveTags = (sharedInformationItemCreateUpdate) =>
            {
                // derive the tags from a comma separated string and validate them
                List<TaggedInformationItem> Tags = new List<TaggedInformationItem>();
                foreach (String stringTag in sharedInformationItemCreateUpdate.Tags.Split(","))
                {
                    TaggedInformationItem tag = new TaggedInformationItem
                    {
                        TagName = stringTag,
                    };
                    try
                    {
                        var validationContext = new ValidationContext(tag);
                        Validator.ValidateObject(tag, validationContext, validateAllProperties: true);
                        Tags.Add(tag);
                    }
                    catch (ValidationException e)
                    {
                        ModelState.AddModelError("Tags", e.ValidationResult.ErrorMessage);
                    }
                }

                return Tags;
            };
        }

        // GET: SharedInformationItem
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.SharedInformationItems != null
                ? View(await _context.SharedInformationItems.ToListAsync())
                : Problem("Entity set 'SqliteContext.SharedInformationItems'  is null.");
        }

        // GET: SharedInformationItem/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems.Where(m => m.Id == id)
                .Include(m => m.InformationItemTaggedInformationItems).FirstAsync();
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
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Tags,Details")] SharedInformationItemCreateUpdateModel sharedInformationItemCreateUpdate)
        {
            List<TaggedInformationItem> Tags = _deriveTags(sharedInformationItemCreateUpdate);

            if (ModelState.IsValid)
            {
                SharedInformationItem sharedInformationItem = sharedInformationItemCreateUpdate.DeriveBase();
                sharedInformationItem.InformationItemTaggedInformationItems = Tags;
                _context.Add(sharedInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sharedInformationItemCreateUpdate);
        }

        // GET: SharedInformationItem/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems.Where(m => m.Id == id)
                .Include(m => m.InformationItemTaggedInformationItems).FirstAsync();
            if (sharedInformationItem == null)
            {
                return NotFound();
            }

            return View(SharedInformationItemCreateUpdateModel.FromBase(sharedInformationItem));
        }

        // POST: SharedInformationItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,Title,Tags,Details")] SharedInformationItemCreateUpdateModel sharedInformationItemCreateUpdate)
        {
            if (id != sharedInformationItemCreateUpdate.Id)
            {
                return NotFound();
            }

            List<TaggedInformationItem> Tags = _deriveTags(sharedInformationItemCreateUpdate);

            if (ModelState.IsValid)
            {
                var sharedInformationItem = await _context.SharedInformationItems.Where(m => m.Id == id)
                    .Include(m => m.InformationItemTaggedInformationItems).FirstAsync();
                if (sharedInformationItem == null)
                {
                    return NotFound();
                }

                sharedInformationItem = sharedInformationItem + sharedInformationItemCreateUpdate.DeriveBase();
                sharedInformationItem.InformationItemTaggedInformationItems = Tags;
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

                return RedirectToAction(nameof(Details), new RouteValueDictionary { { "Id", id.ToString() } });
            }

            return View(sharedInformationItemCreateUpdate);
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