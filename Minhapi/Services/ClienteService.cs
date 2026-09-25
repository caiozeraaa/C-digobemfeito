using Minhaapi.models;
using Minhaapi.Repositories;
using Minhaapi.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
        => _repo = repo;

    public IEnumerable<Cliente> GetAll()
        => _repo.GetAll();

    public Cliente? GetById(int id)
        => _repo.GetById(id);

    public Cliente Create(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome) || string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Nome e Email são obrigatórios.");

        _repo.Add(cliente);
        return cliente;
    }

    public Cliente? Update(int id, Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome) || string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Nome e Email são obrigatórios.");

        if (_repo.GetById(id) == null)
            return null;

        cliente.Id = id;
        _repo.Update(cliente);
        return cliente;
    }

    public bool Delete(int id)
    {
        var cliente = _repo.GetById(id);
        if (cliente == null)
            return false;

        cliente.Ativo = false;
        _repo.Update(cliente);
        return true;
    }
}