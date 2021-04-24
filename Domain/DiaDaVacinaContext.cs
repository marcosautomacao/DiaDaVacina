using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class DiaDaVacinaContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<DataVacina> DataVacina { get; set; }

        public DiaDaVacinaContext(DbContextOptions<DiaDaVacinaContext> options) :
            base(options)
        {
        }
    }
}
