using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_zavetisce.Data;
using E_zavetisce.Models;
using E_zavetisce.Filters;

namespace E_zavetisce.Controllers_Api
{
    [Route("api/v1/HandOvers")]
    [ApiController]
    [ApiKeyAuth]
    public class HandOverApiController : ControllerBase
    {
        private readonly ZavetisceContext _context;

        public HandOverApiController(ZavetisceContext context)
        {
            _context = context;
        }

        // GET: api/HandOverApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HandOver>>> GetHandOvers()
        {
            return await _context.HandOvers.ToListAsync();
        }

        // GET: api/HandOverApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HandOver>> GetHandOver(int id)
        {
            var handOver = await _context.HandOvers.FindAsync(id);

            if (handOver == null)
            {
                return NotFound();
            }

            return handOver;
        }

        // PUT: api/HandOverApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHandOver(int id, HandOver handOver)
        {
            if (id != handOver.HandOverID)
            {
                return BadRequest();
            }

            _context.Entry(handOver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HandOverExists(id))
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

        // POST: api/HandOverApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HandOver>> PostHandOver(HandOver handOver)
        {
            _context.HandOvers.Add(handOver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHandOver", new { id = handOver.HandOverID }, handOver);
        }

        // DELETE: api/HandOverApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHandOver(int id)
        {
            var handOver = await _context.HandOvers.FindAsync(id);
            if (handOver == null)
            {
                return NotFound();
            }

            _context.HandOvers.Remove(handOver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HandOverExists(int id)
        {
            return _context.HandOvers.Any(e => e.HandOverID == id);
        }
    }
}
