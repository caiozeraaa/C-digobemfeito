using Minhaapi.models;

namespace Minhaapi.Services
{
    public interface IFuncionarioService
    {
        IEnumerable<Funcionario> GetAll();
        Funcionario? GetById (int id);
        Funcionario Create(Funcionario funcionario);
        Funcionario? Update (int id, Funcionario funcionario);
        bool Delete (int id);
        
    }
}