using Microsoft.EntityFrameworkCore;
using APIFilR.Model;

namespace APIFilR.Context
{
    public class RessourcesContext : DbContext
    {
        public RessourcesContext(DbContextOptions<RessourcesContext> options)
            : base(options)
        {
        }

        public DbSet<RESSOURCES> DbRESSOURCES { get; set; }
    }
}