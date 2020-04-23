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
        public ActionResult<IEnumerable<string>> GetSpellElementEntity()
        {
            var spellElements = _context.spellElement.ToList();

            return spellElements.Select(spellElement => spellElement.name).ToList();
        }

        // GET: api/SpellElement/extended
        [Authorize]
        [HttpGet("extended")]
        public ActionResult<IEnumerable<SpellElementEntity>> GetExtendedSpellElementList()
        {
            return Ok(_context.spellElement.ToList());
        }

        // GET: api/SpellElement/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<SpellElementEntity> GetSpellElementEntity(int id)
        {
            var spellElements =  _context.spellElement.ToList();
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
        public IActionResult PutSpellElementEntity(int id, SpellElementEntity spellElement)
        {
            if (id != spellElement.id)
            {
                return BadRequest();
            }

            _context.Entry(spellElement).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
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
        public ActionResult<SpellElementEntity> PostSpellElementEntity(SpellElementEntity spellElement)
        {
            _context.spellElement.Add(spellElement);
            _context.SaveChanges();

            return Ok(spellElement);
        }

        // DELETE: api/SpellElement/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<SpellElementEntity> DeleteSpellElementEntity(int id)
        {
            var spellElement = _context.spellElement.Find(id);
            if (spellElement == null)
            {
                return NotFound();
            }

            _context.spellElement.Remove(spellElement);
            _context.SaveChanges();

            return Ok(spellElement);
        }

        private bool SpellElementEntityExists(int id)
        {
            return _context.spellElement.Any(e => e.id == id);
        }
    }
}
