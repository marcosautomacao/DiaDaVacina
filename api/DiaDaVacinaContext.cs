using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api
{
    public class DiaDaVacinaContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

        public DiaDaVacinaContext(DbContextOptions<DiaDaVacinaContext> options) :
            base(options)
        {
        }
    }
}
