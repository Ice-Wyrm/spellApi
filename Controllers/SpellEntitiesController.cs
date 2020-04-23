using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM_example.Entity;
using ORM_example.Dto;

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
        public ActionResult<IEnumerable<string>> GetspellEntities()
        {
            var spells = _context.spell.ToList();

            return spells.Select((spell, index) => spell.name).ToList();
        }

        // GET: api/SpellEntities/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<SpellEntity> GetSpellEntity(int id)
        {
            var spells = _context.spell.ToList();

            var spell = spells.FirstOrDefault(tmp => tmp.id == id); ;

            if (spell == null)
            {
                return NotFound();
            }

            return spell;
        }

        // GET: api/SpellEntities/element/1
        [Authorize]
        [HttpGet("element/{elementId}")]
        public ActionResult<IEnumerable<SpellEntity>> GetSpellsOfCertainElement(int elemendId)
        {
            var spells = _context.spell.ToList();

            return Ok(spells.Select(tmp => tmp.elementId = elemendId).ToList());
        }

        [Authorize]
        [HttpGet("extended")]
        public ActionResult<IEnumerable<ExtendedSpellDto>>GetExtendedSpellInfo()
        {
            var spells = _context.spell.ToList();
            var spellElements = _context.spellElement.ToList();

            var extendedSpellDtos = spells.Join(spellElements, spell => spell.elementId, spellElement => spellElement.id, 
                (spell, spellElement) => new ExtendedSpellDto(spell, spellElement)).ToList();

            return Ok(extendedSpellDtos);
        }

        // PUT: api/SpellEntities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult PutSpellEntity(int id, SpellEntity spell)
        {
            if (id != spell.id)
            {
                return BadRequest();
            }

            _context.Entry(spell).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
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
        public ActionResult<SpellEntity> PostSpellEntity(SpellEntity spell)
        {
            _context.spell.Add(spell);
            _context.SaveChanges();

            //return CreatedAtAction("GetSpellEntity", new { id = spellEntity.id }, spellEntity);
            return Ok(spell);
        }

        // DELETE: api/SpellEntities/5
        [HttpDelete("{id}")]
        public ActionResult<SpellEntity> DeleteSpellEntity(int id)
        {
            var spell = _context.spell.Find(id);
            if (spell == null)
            {
                return NotFound();
            }

            _context.spell.Remove(spell);
            _context.SaveChanges();

            return Ok(spell);
        }

        private bool SpellEntityExists(int id)
        {
            return _context.spell.Any(e => e.id == id);
        }
    }
}
