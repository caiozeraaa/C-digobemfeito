using Microsoft.EntityFrameworkCore;
using Minhaapi.models;

namespace Minhaapi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Venda> Vendas => Set<Venda>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("produtos");
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Nome).HasColumnName("nome");
            entity.Property(p => p.Preco).HasColumnName("preco").HasPrecision(10, 2);
            entity.Property(p => p.Estoque).HasColumnName("estoque");
            entity.Property(p => p.Ativo).HasColumnName("ativo");
        });

        // cliente volta a ser mapeado aqui só pra o EF saber que a tabela
        // "clientes" já existe e como ela é — sem expor DbSet<Cliente>,
        // pq quem le/escreve Cliente continua sendo o ClienteRepository (SQL puro)
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Nome).HasColumnName("nome");
            entity.Property(c => c.Email).HasColumnName("email");
            entity.Property(c => c.Cpf).HasColumnName("cpf");
            entity.Property(c => c.Ativo).HasColumnName("ativo");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.ToTable("funcionario");
            entity.Property(f => f.Id).HasColumnName("id");
            entity.Property(f => f.Nome).HasColumnName("nome");
            entity.Property(f => f.Descricao).HasColumnName("descricao");
            entity.Property(f => f.Cpf).HasColumnName("cpf");
            entity.Property(f => f.Ativo).HasColumnName("ativo");
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.ToTable("vendas");
            entity.Property(v => v.Id).HasColumnName("id");
            entity.Property(v => v.ClienteId).HasColumnName("cliente_id");
            entity.Property(v => v.ProdutoId).HasColumnName("produto_id");
            entity.Property(v => v.Quantidade).HasColumnName("quantidade");
            entity.Property(v => v.PrecoUnitario).HasColumnName("preco_unitario").HasPrecision(10, 2);
            entity.Property(v => v.ValorTotal).HasColumnName("valor_total").HasPrecision(10, 2);
            entity.Property(v => v.DataVenda).HasColumnName("data_venda");

            entity.HasOne<Cliente>()
                  .WithMany()
                  .HasForeignKey(v => v.ClienteId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Produto>()
                  .WithMany()
                  .HasForeignKey(v => v.ProdutoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
