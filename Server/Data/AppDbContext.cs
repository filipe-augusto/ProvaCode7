using ProvaCode7.Models;
using Microsoft.EntityFrameworkCore;


namespace ProvaCode7.Server
{
    public class AppDbContext : DbContext
    {

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<CategoriaProduto> CategoriaProduto { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            #region Cliente
            modelBuilder.Entity<Cliente>()
                   .Property(p => p.Id)
                     .IsRequired()
                  .ValueGeneratedOnAdd();

            modelBuilder.Entity<Cliente>().
                Property(x => x.IdEndereco).
                HasColumnName("id_endereco").IsRequired();

            // 1 PRA 1 
            modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco)
            .WithOne(e => e.Cliente)
            .HasForeignKey<Cliente>(x => x.IdEndereco)
            .HasConstraintName("ForeignKey_Cliente_Endereco");
         
            modelBuilder.Entity<Cliente>()
          .Property(p => p.IsAtivo).ValueGeneratedNever()
          .HasDefaultValue(true);


            #endregion

            #region endereco
            modelBuilder.Entity<Endereco>()
              .Property(p => p.Id)
             .ValueGeneratedOnAdd();

            modelBuilder.Entity<Endereco>()
        .Property(p => p.IsAtivo).ValueGeneratedNever()
        .HasDefaultValue(true);
            #endregion

            #region   Produto
            modelBuilder.Entity<Produto>()
           .Property(p => p.Id)
           .IsRequired()
           .ValueGeneratedOnAdd();
  //  1 PARA VARIOS 
            modelBuilder.Entity<Produto>()
           .HasOne(c => c.CategoriaProduto)
           .WithMany(e => e.Produtos)
           .HasForeignKey(x => x.IdCategoria)
           .HasConstraintName("ForeignKey_Produto_CategoriaProduto");

            modelBuilder.Entity<Produto>()
            .Property(p => p.IsAtivo).ValueGeneratedNever()
            .HasDefaultValue(true);

            #endregion
        }


        //SUBSTITUIR O METODO ONCONFIGURE PARA SET PARA ACESSAR O DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    }
}
