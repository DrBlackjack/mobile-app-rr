using Microsoft.EntityFrameworkCore;
using APIFilR.Model;

namespace APIFilR.Context
{
    public class UtilisateurContext : DbContext
    {
        public UtilisateurContext()
        {
        }

        public UtilisateurContext(DbContextOptions<RessourcesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Helper.ConVal("MaConnection"));
        }

        public DbSet<UTILISATEUR> utilisateur { get; set; }
    }
}