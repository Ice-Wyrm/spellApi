using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM_example.Entity
{
    public class SpellEntity
    { 
        public int cost { get; set; }
        public int elementId { get; set; }
        public int id { get; set ; }
        public string name { get; set; }
    }
}
