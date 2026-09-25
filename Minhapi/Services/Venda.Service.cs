using Minhaapi.models;
using Minhaapi.Repositories;
using Minhaapi.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepo;
    private readonly IProdutoRepository _produtoRepo;
    private readonly IClienteRepository _clienteRepo;

    public VendaService(IVendaRepository vendaRepo, IProdutoRepository produtoRepo, IClienteRepository clienteRepo)
    {
        _vendaRepo = vendaRepo;
        _produtoRepo = produtoRepo;
        _clienteRepo = clienteRepo;
    }
    public IEnumerable<VendaDetalhada> GetAll(string? clienteNome = null)
    => _vendaRepo.GetAll(clienteNome);


    public Venda? GetById(int id)
        => _vendaRepo.GetById(id);

    public Venda Create(int clienteId, int produtoId, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        var cliente = _clienteRepo.GetById(clienteId)
            ?? throw new KeyNotFoundException("Cliente não encontrado.");

        if (!cliente.Ativo)
            throw new ArgumentException("Cliente inativo não pode realizar compras.");

        var produto = _produtoRepo.GetById(produtoId)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        if (!produto.Ativo)
            throw new ArgumentException("Produto inativo não pode ser vendido.");

        if (produto.Estoque < quantidade)
            throw new ArgumentException("Estoque insuficiente para o produto informado.");

        var venda = new Venda
        {
            ClienteId = clienteId,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            PrecoUnitario = produto.Preco,
            ValorTotal = produto.Preco * quantidade,
            DataVenda = DateTime.Now
        };

        _produtoRepo.DecrementarEstoque(produtoId, quantidade);
        _vendaRepo.Add(venda);

        return venda;
    }
}
