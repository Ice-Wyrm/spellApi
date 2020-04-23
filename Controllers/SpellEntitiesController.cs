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
    public class SpellEntitiesController : ControllerBase
    {
        private readonly ExampleContext _context;

        //public struct SpellFullInfo { SpellEntity spell; SpellElementEntity spellElement;}

        public SpellEntitiesController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/SpellEntities
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetspellEntities()
        {
            // return await _context.spellEntities.ToListAsync();

            //Join пока-что бесполезен
            var spells = _context.spellEntities.ToList();

            return spells.Select((spell, index) => spell.name).ToList();
        }

        // GET: api/SpellEntities/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SpellEntity>> GetSpellEntity(int id)
        {
            //var spellEntity = await _context.spellEntities.FindAsync(id);

            var spells = _context.spellEntities.ToList();

            var spell = spells.Where(tmp => tmp.id == id).Single();

            if (spell == null)
            {
                return NotFound();
            }

            return spell;
        }

        // PUT: api/SpellEntities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpellEntity(int id, SpellEntity spellEntity)
        {
            if (id != spellEntity.id)
            {
                return BadRequest();
            }

            _context.Entry(spellEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpellEntityExists(id))
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

        // POST: api/SpellEntities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<SpellEntity>> PostSpellEntity(SpellEntity spellEntity)
        {
            _context.spellEntities.Add(spellEntity);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSpellEntity", new { id = spellEntity.id }, spellEntity);
            return CreatedAtAction(nameof(GetSpellEntity), new { id = spellEntity.id }, spellEntity);
        }

        // DELETE: api/SpellEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpellEntity>> DeleteSpellEntity(int id)
        {
            var spellEntity = await _context.spellEntities.FindAsync(id);
            if (spellEntity == null)
            {
                return NotFound();
            }

            _context.spellEntities.Remove(spellEntity);
            await _context.SaveChangesAsync();

            return spellEntity;
        }

        private bool SpellEntityExists(int id)
        {
            return _context.spellEntities.Any(e => e.id == id);
        }
    }
}
