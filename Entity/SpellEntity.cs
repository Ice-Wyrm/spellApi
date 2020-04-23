using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM_example.Entity
{
    public class SpellEntity : AbstractInterface
    { 
        public int cost { get; set; }
        public int elementId { get; set; }
        int AbstractInterface.id { get; set ; }
        string AbstractInterface.name { get; set; }
    }
}
