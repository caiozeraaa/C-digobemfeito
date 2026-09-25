namespace Minhaapi.models;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set;}
    = true;

}    