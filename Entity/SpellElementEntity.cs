using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM_example.Entity
{
    public class SpellElementEntity : AbstractInterface
    {
        int AbstractInterface.id { get; set; }
        string AbstractInterface.name { get; set; }
    }
}
