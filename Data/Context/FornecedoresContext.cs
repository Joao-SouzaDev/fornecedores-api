using fornecedor_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fornecedor_api.Data.Context;

public class FornecedoresContext : DbContext
{
    public FornecedoresContext(DbContextOptions<FornecedoresContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnderecoFornecedor>()
            .HasOne(endereco => endereco.Fornecedor)
            .WithMany(fornecedor => fornecedor.Endereco)
            .HasForeignKey(endereco => endereco.FornecedorId);
    }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<EnderecoFornecedor> EnderecoFornecedores { get; set; }
}