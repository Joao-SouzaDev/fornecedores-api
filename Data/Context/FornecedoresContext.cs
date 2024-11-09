using fornecedor_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fornecedor_api.Data.Context;

public class FornecedoresContext : DbContext
{
    public FornecedoresContext(DbContextOptions<FornecedoresContext> options) : base(options) { }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}