using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM_example.Entity;

namespace ORM_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellElementController : ControllerBase
    {
        private readonly ExampleContext _context;

        public SpellElementController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/SpellElement
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetSpellElementEntity()
        {
            var spellElements = _context.SpellElementEntity.ToList();

            return spellElements.Select(spellElement => spellElement.name).ToList();
        }

        [Authorize]
        // GET: api/SpellElement/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SpellElementEntity>> GetSpellElementEntity(int id)
        {
            var spellElements = await _context.SpellElementEntity.ToListAsync();
            //var spellElements = _context.SpellElementEntity.ToList();
            var spellElement = spellElements.FirstOrDefault(tmp => tmp.id == id);

            if (spellElement == null)
            {
                return NotFound();
            }

            return Ok(spellElement);
        }

        // PUT: api/SpellElement/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpellElementEntity(int id, SpellElementEntity spellElementEntity)
        {
            if (id != spellElementEntity.id)
            {
                return BadRequest();
            }

            _context.Entry(spellElementEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpellElementEntityExists(id))
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

        // POST: api/SpellElement
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<SpellElementEntity>> PostSpellElementEntity(SpellElementEntity spellElementEntity)
        {
            _context.SpellElementEntity.Add(spellElementEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpellElementEntity", new { id = spellElementEntity.id }, spellElementEntity);
        }

        // DELETE: api/SpellElement/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SpellElementEntity>> DeleteSpellElementEntity(int id)
        {
            var spellElementEntity = await _context.SpellElementEntity.FindAsync(id);
            if (spellElementEntity == null)
            {
                return NotFound();
            }

            _context.SpellElementEntity.Remove(spellElementEntity);
            await _context.SaveChangesAsync();

            return spellElementEntity;
        }

        private bool SpellElementEntityExists(int id)
        {
            return _context.SpellElementEntity.Any(e => e.id == id);
        }
    }
}
