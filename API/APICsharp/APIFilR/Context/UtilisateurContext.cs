using Microsoft.EntityFrameworkCore;
using APIFilR.Model;
using APIFilR.Helpers;

namespace APIFilR.Context
{
    public class MainContext : DbContext
    {
        public MainContext()
        {
        }

        public MainContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var mySqlConnectionStr = Helper.ConVal("MaConnection");
            optionsBuilder.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
        }
        
        public DbSet<UTILISATEUR> utilisateur { get; set; }
        public DbSet<TYPE_RESSOURCES> Type_Ressources { get; set; }
        public DbSet<type_relation_ressource> Type_Relation_Ressource { get; set; }
        public DbSet<CATEGORIES_RESSOURCES> Categories_ressources { get; set; }
        public DbSet<STATUT_RESSOURCE> Statut_ressource { get; set; }
        public DbSet<RESSOURCES> Ressources { get; set; }
        public DbSet<COMMENTAIRES> Commentaires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<COMMENTAIRES>()
                .HasOne(p => p.Utilisateur)
                .WithMany(b => b.CommentaireList)
                .HasPrincipalKey(p => p.id_utilisateur)
                .HasForeignKey(u => u.id_utilisateur)
                .IsRequired();

            modelBuilder.Entity<UTILISATEUR>()
                .HasMany(p => p.CommentaireList)
                .WithOne(b => b.Utilisateur)
                .HasPrincipalKey(p => p.id_utilisateur)
                .HasForeignKey(u => u.id_utilisateur)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
    
}