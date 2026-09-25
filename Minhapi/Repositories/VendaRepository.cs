using Minhaapi.models;
using Minhaapi.Repositories;
using MySqlConnector;

public class VendaRepository : IVendaRepository
{
    private readonly string _connectionString;

    public VendaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");
    }

  public IEnumerable<VendaDetalhada> GetAll(string? clienteNome = null)
{
    var lista = new List<VendaDetalhada>();
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    string sql = @"SELECT v.id, c.nome AS cliente_nome, p.nome AS produto_nome,
                          v.quantidade, v.preco_unitario, v.valor_total, v.data_venda
                   FROM vendas v
                   JOIN clientes c ON v.cliente_id = c.id
                   JOIN produtos p ON v.produto_id = p.id";

    if (!string.IsNullOrWhiteSpace(clienteNome))
        sql += " WHERE c.nome LIKE @ClienteNome";

    using var cmd = new MySqlCommand(sql, conn);
    if (!string.IsNullOrWhiteSpace(clienteNome))
        cmd.Parameters.AddWithValue("@ClienteNome", $"%{clienteNome}%");

    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        lista.Add(new VendaDetalhada
        {
            Id = reader.GetInt32("id"),
            ClienteNome = reader.GetString("cliente_nome"),
            ProdutoNome = reader.GetString("produto_nome"),
            Quantidade = reader.GetInt32("quantidade"),
            PrecoUnitario = reader.GetDecimal("preco_unitario"),
            ValorTotal = reader.GetDecimal("valor_total"),
            DataVenda = reader.GetDateTime("data_venda")
        });
    }

    return lista;
}


    public Venda? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT id, cliente_id, produto_id, quantidade, preco_unitario, valor_total, data_venda FROM vendas WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new Venda
        {
            Id = reader.GetInt32("id"),
            ClienteId = reader.GetInt32("cliente_id"),
            ProdutoId = reader.GetInt32("produto_id"),
            Quantidade = reader.GetInt32("quantidade"),
            PrecoUnitario = reader.GetDecimal("preco_unitario"),
            ValorTotal = reader.GetDecimal("valor_total"),
            DataVenda = reader.GetDateTime("data_venda")
        };
    }

    public void Add(Venda venda)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"INSERT INTO vendas (cliente_id, produto_id, quantidade, preco_unitario, valor_total, data_venda)
                       VALUES (@ClienteId, @ProdutoId, @Quantidade, @PrecoUnitario, @ValorTotal, @DataVenda);
                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
        cmd.Parameters.AddWithValue("@ProdutoId", venda.ProdutoId);
        cmd.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
        cmd.Parameters.AddWithValue("@PrecoUnitario", venda.PrecoUnitario);
        cmd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
        cmd.Parameters.AddWithValue("@DataVenda", venda.DataVenda);

        var idGerado = cmd.ExecuteScalar();
        venda.Id = Convert.ToInt32(idGerado);
    }
}
