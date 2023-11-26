using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_zavetisce.Data;
using E_zavetisce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace E_zavetisce.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly ZavetisceContext _context;
        private readonly UserManager<ApplicationUser> _userMannager;

        public AdoptionController(ZavetisceContext context, UserManager<ApplicationUser> userMannager)
        {
            _context = context;
            _userMannager = userMannager;
        }

        // GET: Adoption
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userMannager.GetUserAsync(User);
            var isEmployee = User.IsInRole("Employee");

            IQueryable<Adoption> adoptions;

            if (isEmployee)
            {
                adoptions = _context.Adoptions.Include(a => a.Client).Include(a => a.Pet);
            }
            else
            {
                adoptions = _context.Adoptions.Where(a => a.ClientID == currentUser.Id).Include(a => a.Client).Include(a => a.Pet);
            }

            return View(await adoptions.ToListAsync());
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
        [Authorize]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            ViewData["Pet"] = pet;
            return View();
        }

        // POST: Adoption/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            var currentUser = await _userMannager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var adoption = new Adoption
            {
                PetID = pet.PetID,
                ClientID = currentUser.Id, // Replace with the appropriate client ID
                DateAdopted = DateTime.Now
            };

            _context.Add(adoption);

            pet.Adopted = true;
            _context.Update(pet);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoptions.Any(e => e.PetID == id);
        }
    }
}
