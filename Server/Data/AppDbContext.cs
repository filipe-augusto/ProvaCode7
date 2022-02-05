using Microsoft.EntityFrameworkCore;
using ProvaCode7.Shared;

namespace ProvaCode7.Server
{
    public class AppDbContext : DbContext
    {

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<CategoriaProduto> CategoriaProduto { get; set; }
        public DbSet<StatusCliente> StatusCliente { get; set; }
        public DbSet<ProdutoOfertadoCliente> ProdutoOfertadoCliente { get; set; }
        public DbSet<RegistroAtendimentos> RegistroAtendimentos { get; set; }

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
            //1 para muitos
            modelBuilder.Entity<Cliente>()
            .HasOne(c => c.StatusCliente)
            .WithMany(e => e.Clientes)
            .HasForeignKey(x => x.IdStatus)
            .HasConstraintName("ForeignKey_Cliente_StatusCliente");

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

            #region ProdutoOfertado

            modelBuilder.Entity<ProdutoOfertadoCliente>()
            .Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProdutoOfertadoCliente>()
            .HasOne(c => c.Cliente)
            .WithMany(e => e.ProdutoOfertadoCliente)
            .HasForeignKey(x => x.IdCliente)
            .HasConstraintName("ForeignKey_ProdutoOfertadoCliente_Cliente");

            modelBuilder.Entity<ProdutoOfertadoCliente>()
          .HasOne(c => c.RegistroAtendimentos)
          .WithMany(e => e.ProdutoOfertadoCliente)
          .HasForeignKey(x => x.IdRegistroAtendimento)
          .HasConstraintName("ForeignKey_ProdutoOfertadoCliente_RegistroAtendimentos");

          


            modelBuilder.Entity<ProdutoOfertadoCliente>()
            .HasOne(c => c.Produto)
            .WithMany(e => e.ProdutoOfertadoCliente)
            .HasForeignKey(x => x.IdProduto)
            .HasConstraintName("ForeignKey_ProdutoOfertadoCliente_Produto");


            #endregion

            #region RegistroAtendimentos
            modelBuilder.Entity<RegistroAtendimentos>()
             .Property(p => p.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<RegistroAtendimentos>()
            .HasOne(c => c.Cliente)
            .WithMany(e => e.RegistroAtendimentos)
            .HasForeignKey(x => x.IdCliente)
            .HasConstraintName("ForeignKey_RegistroAtendimentoso_Cliente");



            #endregion
        }


        //SUBSTITUIR O METODO ONCONFIGURE PARA SET PARA ACESSAR O DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    }
}
