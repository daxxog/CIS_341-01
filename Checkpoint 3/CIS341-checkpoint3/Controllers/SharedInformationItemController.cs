using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_checkpoint3.Models;
using System.ComponentModel.DataAnnotations;
using CIS341_checkpoint3.Data;
using CIS341_checkpoint3.Data.Entities;

namespace CIS341_checkpoint3.Controllers
{
    /// <summary>
    /// Controller class for accessing data in Entity 'SharedInformationItem'.
    /// </summary>
    [Authorize]
    public class SharedInformationItemController : Controller
    {
        private readonly SqliteContext _context;
        private readonly Func<SharedInformationItemCreateUpdateModel, List<TaggedInformationItem>> _deriveTags;
        private readonly Func<AuthorizationStatus> _getAuthorizationStatus;

        public SharedInformationItemController(SqliteContext context)
        {
            _context = context;
            _getAuthorizationStatus = () => (AuthorizationStatus)HttpContext.Items["AuthorizationStatus"];
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
        /// <summary>
        /// List SharedInformationItems.
        /// </summary>
        /// <returns>
        /// On success: A view containing a List of SharedInformationItems.
        /// </returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.SharedInformationItems != null
                ? View(await _context.SharedInformationItems.ToListAsync())
                : Problem("Entity set 'SqliteContext.SharedInformationItems'  is null.");
        }

        // GET: SharedInformationItem/Details/5
        /// <summary>
        /// Get details about a specific SharedInformationItem.
        /// </summary>
        /// <param name="id">The ID of the SharedInformationItem.</param>
        /// <returns>
        /// On success: A view containing the SharedInformationItem.
        /// </returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SharedInformationItems == null)
            {
                return NotFound();
            }

            var sharedInformationItem = await _context.SharedInformationItems.Where(m => m.Id == id)
                .Include(m => m.InformationItemTaggedInformationItems).FirstAsync();
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            var sharedInformationItemIsFavorite = await _context.Favorites.Where(m => m.UserId == authStatus.UserId)
                .Where(m => m.InformationItemId == id).FirstOrDefaultAsync();

            if (sharedInformationItem == null)
            {
                return NotFound();
            }

            return View(new MyFavoriteItemModel
            {
                SharedInformationItem = sharedInformationItem, MyFavorite = sharedInformationItemIsFavorite != null
            });
        }

        // GET: SharedInformationItem/Create
        /// <summary>
        /// Display the form for creating a new SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <returns>
        /// If not a content manager: A redirection to the Tag controller Index.
        /// On success: A view with the containing the form to create a new SharedInformationItems.
        /// </returns>
        public IActionResult Create()
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

            return View();
        }

        // POST: SharedInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a new SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <param name="sharedInformationItemCreateUpdate">The SharedInformationItemCreateUpdateModel to convert to a SharedInformationItem and add to the database.</param>
        /// <returns>
        /// If not a content manager: A redirection to the Tag controller Index.
        /// On success: A redirection back to the SharedInformationItem listing view.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Tags,Details")] SharedInformationItemCreateUpdateModel sharedInformationItemCreateUpdate)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

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
        /// <summary>
        /// Display the form for editing a specific SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <param name="id">The ID of the SharedInformationItem to edit.</param>
        /// <returns>
        /// On success: A view with the containing the form to edit a specific SharedInformationItem.
        /// If not a content manager: A redirection to the Tag controller Index.
        /// </returns>
        public async Task<IActionResult> Edit(long? id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

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
        /// <summary>
        /// Edit an existing SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <param name="id">The ID of the SharedInformationItem to edit.</param>
        /// <param name="sharedInformationItemCreateUpdate">The SharedInformationItemCreateUpdateModel to convert to a SharedInformationItem and modify in the database.</param>
        /// <returns>
        /// If not a content manager: A redirection to the Tag controller Index.
        /// On success: A redirection back to the SharedInformationItem details view.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,Title,Tags,Details")] SharedInformationItemCreateUpdateModel sharedInformationItemCreateUpdate)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

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
        /// <summary>
        /// Display the confirmation message for deleting a specific SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <param name="id">The ID of the SharedInformationItem to delete.</param>
        /// <returns>
        /// On success: A view with the containing the confirmation message to delete a specific SharedInformationItem.
        /// If not a content manager: A redirection to the Tag controller Index.
        /// </returns>
        public async Task<IActionResult> Delete(long? id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

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
        /// <summary>
        /// Delete a specific SharedInformationItem.
        /// Only accessible by content managers.
        /// </summary>
        /// <param name="id">The ID of the SharedInformationItem to delete.</param>
        /// <returns>
        /// On success: A redirection back to the SharedInformationItem listing view.
        /// If not a content manager: A redirection to the Tag controller Index.
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == false)
            {
                return RedirectToAction("Index", "Tag");
            }

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