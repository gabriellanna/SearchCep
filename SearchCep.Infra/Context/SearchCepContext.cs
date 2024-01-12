using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Models;

namespace SearchCep.Infra.Context
{
    public class SearchCepContext : DbContext
    {
        public SearchCepContext(DbContextOptions<SearchCepContext> options) : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Street> Streets { get; set; }
    }
}