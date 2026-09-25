using Minhaapi.models;

namespace Minhaapi.Repositories;

public interface IFuncionarioRepository
{
    IEnumerable<Funcionario> GetAll();
    Funcionario? GetById(int id);
    void Add(Funcionario funcionario);
    void Update(Funcionario funcionario);
    void Delete(int id);
}


