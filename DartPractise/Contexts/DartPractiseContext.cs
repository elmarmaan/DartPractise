using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Domain;

namespace DartPractise.Contexts
{
    public class DartPractiseContext : DbContext
    {
        public DbSet<Player> Players { get; set; }  
    }
}