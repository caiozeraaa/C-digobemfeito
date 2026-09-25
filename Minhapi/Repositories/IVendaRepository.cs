using Minhaapi.models;

namespace Minhaapi.Repositories;

public interface IVendaRepository
{
    IEnumerable<VendaDetalhada> GetAll(string? clienteNome = null);
    Venda? GetById(int id);
    void Add(Venda venda);
}
