using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ORM_example.Entity;

namespace ORM_example.Entity
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
        {
        }
        public DbSet<SpellEntity> spell {get; set;}
        public DbSet<SpellElementEntity> spellElement { get; set; }
    }
}
