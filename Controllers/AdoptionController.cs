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
    public class AdoptionController : Controller
    {
        private readonly ZavetisceContext _context;

        public AdoptionController(ZavetisceContext context)
        {
            _context = context;
        }

        // GET: Adoption
        public async Task<IActionResult> Index()
        {
            var zavetisceContext = _context.Adoptions.Include(a => a.Client).Include(a => a.Pet);
            return View(await zavetisceContext.ToListAsync());
        }

        // GET: Adoption/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Client)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(m => m.PetID == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Adoption/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName");
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name");
            return View();
        }

        // POST: Adoption/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetID,ClientID,DateAdopted")] Adoption adoption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", adoption.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", adoption.PetID);
            return View(adoption);
        }

        // GET: Adoption/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", adoption.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", adoption.PetID);
            return View(adoption);
        }

        // POST: Adoption/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,ClientID,DateAdopted")] Adoption adoption)
        {
            if (id != adoption.PetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.PetID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "FirstMidName", adoption.ClientID);
            ViewData["PetID"] = new SelectList(_context.Pets, "PetID", "Name", adoption.PetID);
            return View(adoption);
        }

        // GET: Adoption/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Client)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(m => m.PetID == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _context.Adoptions.Remove(adoption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoptions.Any(e => e.PetID == id);
        }
    }
}
