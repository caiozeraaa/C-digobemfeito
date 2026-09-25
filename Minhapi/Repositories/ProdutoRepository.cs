using Microsoft.EntityFrameworkCore;
using Minhaapi.Data;
using Minhaapi.models;
using Minhaapi.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> GetAll()
        => _context.Produtos.AsNoTracking().ToList();

    public Produto? GetById(int id)
        => _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == id);

    public void Add(Produto p)
    {
        _context.Produtos.Add(p);
        _context.SaveChanges();
    }

    public void Update(Produto p)
    {
        _context.Produtos.Update(p);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto is not null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
    public void DecrementarEstoque(int produtoId, int quantidade)
{
    _context.Produtos
        .Where(p => p.Id == produtoId)
        .ExecuteUpdate(setters => setters
            .SetProperty(p => p.Estoque, p => p.Estoque - quantidade));
}

}
