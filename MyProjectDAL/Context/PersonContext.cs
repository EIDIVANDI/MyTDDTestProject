using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace MyProjectDAL.Context
{
    public class PersonContext : DbContext
    {
        public PersonContext() : base()
        { }

        public virtual DbSet<Entity.Person> Persons { get; set; }

    }
}
