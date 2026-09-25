using Minhaapi.models;

namespace Minhaapi.Repositories;

public interface IProdutoRepository
{
    IEnumerable<Produto> GetAll();
    Produto? GetById(int id);
    void Add(Produto produto);
    void Update(Produto produto);
    void Delete(int id);
    void DecrementarEstoque(int produtoId, int quantidade);

}