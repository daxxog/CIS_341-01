using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS341_checkpoint2.Data;
using CIS341_checkpoint2.Data.Entities;
using CIS341_checkpoint2.Models;

namespace CIS341_checkpoint2.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            var sqliteContext = _context.UserInformationItems.Where(m => m.UserId == authStatus.UserId)
                .Include(u => u.User);
            return View(await sqliteContext.ToListAsync());
        }

        // GET: UserInformationItem/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            AuthorizationStatus authStatus = _getAuthorizationStatus();

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInformationItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Details")] UserInformationItem userInformationItem)
        {
            AuthorizationStatus authStatus = _getAuthorizationStatus();
            userInformationItem.User = await _context.Users.Where(m => m.Id == authStatus.UserId).FirstAsync();

            if (ModelState.IsValid)
            {
                _context.Add(userInformationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine(JsonConvert.SerializeObject(userInformationItem));
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine(JsonConvert.SerializeObject(allErrors));
            }

            return View(userInformationItem);
        }

        // GET: UserInformationItem/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            AuthorizationStatus authStatus = _getAuthorizationStatus();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            [Bind("Id,Title,Details")] UserInformationItem userInformationItem)
        {
            if (id != userInformationItem.Id)
            {
                return NotFound();
            }

            AuthorizationStatus authStatus = _getAuthorizationStatus();
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
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.UserInformationItems == null)
            {
                return NotFound();
            }

            AuthorizationStatus authStatus = _getAuthorizationStatus();
            var userInformationItem = await _context.UserInformationItems.Where(m => m.Id == id)
                .Where(m => m.UserId == authStatus.UserId).FirstOrDefaultAsync();
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

            AuthorizationStatus authStatus = _getAuthorizationStatus();
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