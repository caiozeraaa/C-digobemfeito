using Minhaapi.models;
using Minhaapi.Repositories;
using Minhaapi.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repo;

    public FuncionarioService(IFuncionarioRepository repo)
        => _repo = repo;

    public IEnumerable<Funcionario> GetAll()
        => _repo.GetAll();

    public Funcionario? GetById(int id)
        => _repo.GetById(id);

    public Funcionario Create(Funcionario funcionario)
    {
        if (string.IsNullOrWhiteSpace(funcionario.Nome) || string.IsNullOrWhiteSpace(funcionario.Descricao))
            throw new ArgumentException("Nome e Descricao são obrigatórios.");

        _repo.Add(funcionario);
        return funcionario;
    }

    public Funcionario? Update(int id, Funcionario funcionario)
    {
        if (string.IsNullOrWhiteSpace(funcionario.Nome) || string.IsNullOrWhiteSpace(funcionario.Descricao))
            throw new ArgumentException("Nome e Descri são obrigatórios.");

        if (_repo.GetById(id) == null)
            return null;

        funcionario.Id = id;
        _repo.Update(funcionario);
        return funcionario;
    }

    public bool Delete(int id)
    {
        var funcionario = _repo.GetById(id);
        if (funcionario == null)
            return false;

        funcionario.Ativo = false;
        _repo.Update(funcionario);
        return true;
    }
}