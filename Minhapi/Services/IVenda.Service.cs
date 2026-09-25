using Minhaapi.models;

namespace Minhaapi.Services;

public interface IVendaService
{
    IEnumerable<VendaDetalhada> GetAll(string? clienteNome = null);
    Venda? GetById(int id);
    Venda Create(int clienteId, int produtoId, int quantidade);
}
