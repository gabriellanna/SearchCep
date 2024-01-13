using Microsoft.EntityFrameworkCore;
using Cep.Domain.Models;

namespace Cep.Infra.Context
{
    public class CepContext : DbContext
    {
        public CepContext(DbContextOptions<CepContext> options) : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Street> Streets { get; set; }
    }
}