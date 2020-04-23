using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ORM_example.Entity;

namespace ORM_example.Dto
{
    public class ExtendedSpellDto
    {
        public int id { get; set; }

        public int cost { get; set; }

        public string name { get; set; }

        public string elementName { get; set; }

        public ExtendedSpellDto(SpellEntity spell, SpellElementEntity spellElement)
        {
            this.id = spell.id;
            this.name = spell.name;
            this.elementName = spellElement.name;
            this.cost = spell.cost;
        }
    }
}
