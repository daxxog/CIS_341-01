using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly DbContext _context;

        public TodoItemController(DbContext context)
        {
            _context = context;
        }

        // GET: TodoItem
        public async Task<IActionResult> Index()
        {
              return _context.TodoItem != null ? 
                          View(await _context.TodoItem.ToListAsync()) :
                          Problem("Entity set 'DbContext.TodoItem'  is null.");
        }

        // GET: TodoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
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
            return View(todoItem);
        }

        // GET: TodoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TodoItem == null)
            {
                return Problem("Entity set 'DbContext.TodoItem'  is null.");
            }
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItem.Remove(todoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
          return (_context.TodoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
