using Microsoft.EntityFrameworkCore;
using APIFilR.Model;
using APIFilR.Helpers;

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
        public DbSet<TYPE_RESSOURCES> Type_Ressources { get; set; }
        public DbSet<type_relation_ressource> Type_Relation_Ressource { get; set; }
        public DbSet<CATEGORIES_RESSOURCES> Categories_ressources { get; set; }
    }
    
}