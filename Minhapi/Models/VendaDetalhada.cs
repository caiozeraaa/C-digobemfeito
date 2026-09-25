namespace Minhaapi.models;

public class VendaDetalhada
{
    public int Id { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataVenda { get; set; }
}
