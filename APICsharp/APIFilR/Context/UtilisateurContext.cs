using Microsoft.EntityFrameworkCore;
using APIFilR.Model;
using APIFilR.Helpers;

namespace APIFilR.Context
{
    public partial class MainContext : DbContext
    {
        public MainContext()
        {
        }

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                var mySqlConnectionStr = Helper.ConVal("MaConnection");
                optionsBuilder.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
            }
        }

        #region dbset
        public DbSet<UTILISATEUR> Utilisateur { get; set; }
        public DbSet<TYPE_RESSOURCES> Type_Ressources { get; set; }
        public DbSet<type_relation_ressource> Type_Relation_Ressource { get; set; }
        public DbSet<CATEGORIES_RESSOURCES> Categories_ressources { get; set; }
        public DbSet<STATUT_RESSOURCE> Statut_ressource { get; set; }
        public DbSet<RESSOURCES> Ressources { get; set; }
        public DbSet<COMMENTAIRES> Commentaires { get; set; }

        #endregion dbset

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<STATUT_RESSOURCE>(entity =>
            {
                entity.HasMany<RESSOURCES>(p => p.ListRessources)
                    .WithOne(b => b.Statut)
                    .HasForeignKey(u => u.id_statut);
            });
            
            
            modelBuilder.Entity<UTILISATEUR>(entity =>
            {
                entity.Property(t => t.id_utilisateur).HasColumnName("id_utilisateur");
                entity.HasMany<COMMENTAIRES>(util => util.CommentaireList)
                    .WithOne(com=>com.Utilisateur)
                    .HasForeignKey(com => com.id_utilisateur)
                    .IsRequired();
            });
            
                

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }    
}