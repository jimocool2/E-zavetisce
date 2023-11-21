using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_zavetisce.Data;
using E_zavetisce.Models;

namespace E_zavetisce.Controllers
{
    public class HandOverController : Controller
    {
        private readonly ZavetisceContext _context;

        public HandOverController(ZavetisceContext context)
        {
            _context = context;
        }

        // GET: HandOver
        public async Task<IActionResult> Index()
        {
            var zavetisceContext = _context.HandOvers.Include(h => h.Client).Include(h => h.Pet);
            return View(await zavetisceContext.ToListAsync());
        }

        // GET: HandOver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handOver = await _context.HandOvers
                .Include(h => h.Client)
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(m => m.HandOverID == id);
            if (handOver == null)
            {
                return NotFound();
            }

            return View(handOver);
        }

        // GET: HandOver/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName");
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name");
            return View();
        }

        // POST: HandOver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HandOverID,ClientID,PetID,DateCreated")] HandOver handOver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handOver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", handOver.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", handOver.PetID);
            return View(handOver);
        }

        // GET: HandOver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handOver = await _context.HandOvers.FindAsync(id);
            if (handOver == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", handOver.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", handOver.PetID);
            return View(handOver);
        }

        // POST: HandOver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HandOverID,ClientID,PetID,DateCreated")] HandOver handOver)
        {
            if (id != handOver.HandOverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handOver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandOverExists(handOver.HandOverID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", handOver.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", handOver.PetID);
            return View(handOver);
        }

        // GET: HandOver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handOver = await _context.HandOvers
                .Include(h => h.Client)
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(m => m.HandOverID == id);
            if (handOver == null)
            {
                return NotFound();
            }

            return View(handOver);
        }

        // POST: HandOver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handOver = await _context.HandOvers.FindAsync(id);
            if (handOver != null)
            {
                _context.HandOvers.Remove(handOver);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HandOverExists(int id)
        {
            return _context.HandOvers.Any(e => e.HandOverID == id);
        }
    }
}
