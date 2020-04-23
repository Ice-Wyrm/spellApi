using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORM_example.Entity
{
    public interface AbstractInterface
    {
        public int id { get; protected set; }
        public string name { get; protected set; }

    }
}
