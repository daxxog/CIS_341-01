using Newtonsoft.Json;
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
    /// <summary>
    /// Controller class for accessing data in Entity 'UserInformationItem'.
    /// </summary>
    [Authorize]
    public class UserInformationItemController : Controller
    {
        private readonly SqliteContext _context;
        private readonly Func<AuthorizationStatus> _getAuthorizationStatus;

        public UserInformationItemController(SqliteContext context)
        {
            _context = context;
            _getAuthorizationStatus = () => (AuthorizationStatus)HttpContext.Items["AuthorizationStatus"];
        }

        // GET: UserInformationItem
        /// <summary>
        /// List UserInformationItems associated with the currently logged in User.
        /// </summary>
        /// <returns>
        /// On success: A view containing a List of UserInformationItems associated with the currently logged in User.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            var sqliteContext = _context.UserInformationItems.Where(m => m.UserId == authStatus.UserId)
                .Include(u => u.User);
            return View(await sqliteContext.ToListAsync());
        }

        // GET: UserInformationItem/Details/5
        /// <summary>
        /// Get details about a specific UserInformationItem.
        /// Not accessible by content managers.
        /// Not accessible by other users except the owner.
        /// </summary>
        /// <param name="id">The ID of the UserInformationItem.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// If not the owner: A 404.
        /// On success: A view containing the UserInformationItem.
        /// </returns>
        public async Task<IActionResult> Details(long? id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems
                .Where(m => m.UserId == authStatus.UserId)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInformationItem == null)
            {
                return NotFound();
            }

            return View(userInformationItem);
        }

        // GET: UserInformationItem/Create
        /// <summary>
        /// Display the form for creating a new UserInformationItem.
        /// Not accessible by content managers.
        /// </summary>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// On success: A view with the containing the form to create a new UserInformationItems.
        /// </returns>
        public IActionResult Create()
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            return View();
        }

        // POST: UserInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a new UserInformationItem.
        /// Not accessible by content managers.
        /// </summary>
        /// <param name="userInformationItem">The UserInformationItem and add to the database.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// On success: A redirection back to the UserInformationItem listing view.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Details")] UserInformationItem userInformationItem)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            userInformationItem.User = await _context.Users.Where(m => m.Id == authStatus.UserId).FirstAsync();

            if (ModelState.IsValid)
            {
                _context.Add(userInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userInformationItem);
        }

        // GET: UserInformationItem/Edit/5
        /// <summary>
        /// Display the form for editing a specific UserInformationItem.
        /// Not accessible by content managers.
        /// Not accessible by other users except the owner.
        /// </summary>
        /// <param name="id">The ID of the UserInformationItem to edit.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// If not the owner: A 404.
        /// On success: A view with the containing the form to edit a specific UserInformationItem.
        /// </returns>
        public async Task<IActionResult> Edit(long? id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems.Where(m => m.Id == id)
                .Where(m => m.UserId == authStatus.UserId).FirstOrDefaultAsync();
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
        /// <summary>
        /// Edit an existing UserInformationItem.
        /// Not accessible by content managers.
        /// Not accessible by other users except the owner.
        /// </summary>
        /// <param name="id">The ID of the UserInformationItem to edit.</param>
        /// <param name="userInformationItem">The UserInformationItem to modify in the database.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// If not the owner: A 404.
        /// On success: A redirection back to the UserInformationItem details view.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,Title,Details")] UserInformationItem userInformationItem)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (id != userInformationItem.Id)
            {
                return NotFound();
            }

            var _userInformationItem = userInformationItem;
            userInformationItem = await _context.UserInformationItems.Where(m => m.Id == id)
                .Where(m => m.UserId == authStatus.UserId).Include(u => u.User).FirstOrDefaultAsync();
            if (userInformationItem == null)
            {
                return NotFound();
            }

            userInformationItem += _userInformationItem;

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

                return RedirectToAction(nameof(Details), new RouteValueDictionary { { "Id", id.ToString() } });
            }

            return View(userInformationItem);
        }

        // GET: UserInformationItem/Delete/5
        /// <summary>
        /// Display the confirmation message for deleting a specific UserInformationItem.
        /// Not accessible by content managers.
        /// Not accessible by other users except the owner.
        /// </summary>
        /// <param name="id">The ID of the UserInformationItem to delete.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// If not the owner: A 404.
        /// On success: A view with the containing the confirmation message to delete a specific UserInformationItems.
        /// </returns>
        public async Task<IActionResult> Delete(long? id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            var userInformationItem = await _context.UserInformationItems.Where(m => m.Id == id)
                .Where(m => m.UserId == authStatus.UserId).FirstOrDefaultAsync();
            if (userInformationItem == null)
            {
                return NotFound();
            }

            return View(userInformationItem);
        }

        // POST: UserInformationItem/Delete/5
        /// <summary>
        /// Delete a specific UserInformationItem.
        /// Not accessible by content managers.
        /// Not accessible by other users except the owner.
        /// </summary>
        /// <param name="id">The ID of the UserInformationItem to delete.</param>
        /// <returns>
        /// If a content manager: A redirection to the Tag controller Index.
        /// If not the owner: A 404.
        /// On success: A redirection back to the UserInformationItem listing view.
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            if (authStatus.IsContentManager == true)
            {
                return RedirectToAction("Index", "Tag");
            }

            if (_context.UserInformationItems == null)
            {
                return Problem("Entity set 'SqliteContext.UserInformationItems'  is null.");
            }

            var userInformationItem = await _context.UserInformationItems.Where(m => m.Id == id)
                .Where(m => m.UserId == authStatus.UserId).FirstOrDefaultAsync();
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