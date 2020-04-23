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
        public DbSet<SpellEntity> spellEntities {get; set;}
        public DbSet<ORM_example.Entity.SpellElementEntity> SpellElementEntity { get; set; }
    }
}
