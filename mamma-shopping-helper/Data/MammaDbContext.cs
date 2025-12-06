using mamma_shopping_helper.Model;
using Microsoft.EntityFrameworkCore;

namespace mamma_shopping_helper.Data
{
    public class MammaDbContext : DbContext
    {
        public MammaDbContext(DbContextOptions<MammaDbContext> options) : base(options)
        {


        }
        public DbSet<Model.ListaDellaSpesa> ListeDellaSpesa { get; set; }
        public DbSet<Model.Prodotto> Prodotti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prodotto>()
                .HasOne(p => p.ListaDellaSpesa)           
                .WithMany(l => l.Prodotti)                 
                .HasForeignKey(p => p.ListaDellaSpesaId)   
                .OnDelete(DeleteBehavior.Cascade);        

           
            modelBuilder.Entity<Prodotto>()
                .HasIndex(p => p.ListaDellaSpesaId);      

            modelBuilder.Entity<ListaDellaSpesa>()
                .HasIndex(l => l.DataCreazione);         
        }
    }
}