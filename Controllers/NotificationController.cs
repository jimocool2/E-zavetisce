using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_zavetisce.Data;
using E_zavetisce.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace E_zavetisce.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ZavetisceContext _context;

        public NotificationController(ZavetisceContext context)
        {
            _context = context;
        }

        // GET: Notification
        public async Task<IActionResult> Index()
        {
            ViewData["IsEmployee"] = User.IsInRole("Employee");

            var zavetisceContext = _context.Notifications.Include(n => n.Employee);
            return View(await zavetisceContext.ToListAsync());
        }

        // GET: Notification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(m => m.NotificationID == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // GET: Notification/Create
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstMidName");
            return View();
        }

        // POST: Notification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationID,Title,Body,DateCreated")] Notification notification)
        {
            string employeeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            notification.EmployeeID = employeeId;
            notification.DateCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }

            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstMidName", notification.EmployeeID);
            return View(notification);
        }

        // GET: Notification/Edit/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstMidName", notification.EmployeeID);
            return View(notification);
        }

        // POST: Notification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificationID,Title,Body,DateCreated,EmployeeID")] Notification notification)
        {
            if (id != notification.NotificationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationExists(notification.NotificationID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstMidName", notification.EmployeeID);
            return View(notification);
        }

        // GET: Notification/Delete/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(m => m.NotificationID == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.NotificationID == id);
        }
    }
}
