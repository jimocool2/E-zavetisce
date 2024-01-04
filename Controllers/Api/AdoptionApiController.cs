using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_zavetisce.Data;
using E_zavetisce.Models;

namespace E_zavetisce.Controllers_Api
{
    [Route("api/v1/Adoptions")]
    [ApiController]
    public class AdoptionApiController : ControllerBase
    {
        private readonly ZavetisceContext _context;

        public AdoptionApiController(ZavetisceContext context)
        {
            _context = context;
        }

        // GET: api/AdoptionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adoption>>> GetAdoptions()
        {
            return await _context.Adoptions.ToListAsync();
        }

        // GET: api/AdoptionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adoption>> GetAdoption(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);

            if (adoption == null)
            {
                return NotFound();
            }

            return adoption;
        }

        // PUT: api/AdoptionApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoption(int id, Adoption adoption)
        {
            if (id != adoption.PetID)
            {
                return BadRequest();
            }

            _context.Entry(adoption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdoptionApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Adoption>> PostAdoption(Adoption adoption)
        {
            _context.Adoptions.Add(adoption);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdoptionExists(adoption.PetID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdoption", new { id = adoption.PetID }, adoption);
        }

        // DELETE: api/AdoptionApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoption(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }

            _context.Adoptions.Remove(adoption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoptions.Any(e => e.PetID == id);
        }
    }
}
