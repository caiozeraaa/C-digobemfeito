using Minhaapi.models;
using Minhaapi.Repositories;
using MySqlConnector;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly string _connectionString;

    public FuncionarioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");
    }

    public IEnumerable<Funcionario> GetAll()
    {
        var lista = new List<Funcionario>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT id, nome, descricao, cpf, ativo FROM funcionarios";
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Funcionario
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Descricao = reader.GetString("email"),
                Cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader.GetString("cpf"),
                Ativo = reader.GetBoolean("ativo")
            });
        }

        return lista;
    }

    public Funcionario? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT id, nome, descricao, cpf, ativo FROM clientes WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new Funcionario
        {
            Id = reader.GetInt32("id"),
            Nome = reader.GetString("nome"),
            Descricao = reader.GetString("email"),
            Cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader.GetString("cpf"),
            Ativo = reader.GetBoolean("ativo")
        };
    }

    public void Add(Funcionario f)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"INSERT INTO clientes (nome, descricao, cpf, ativo)
                       VALUES (@Nome, @Descricao, @Cpf, @Ativo);
                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", f.Nome);
        cmd.Parameters.AddWithValue("@Descricao", f.Descricao);
        cmd.Parameters.AddWithValue("@Cpf", (object?)f.Cpf ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Ativo", f.Ativo);

        var idGerado = cmd.ExecuteScalar();
        f.Id = Convert.ToInt32(idGerado);
    }

    public void Update(Funcionario f)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"UPDATE clientes
                       SET nome = @Nome, Descricao = @Descricao, cpf = @Cpf, ativo = @Ativo
                       WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", f.Nome);
        cmd.Parameters.AddWithValue("@Descricao", f.Descricao);
        cmd.Parameters.AddWithValue("@Cpf", (object?)f.Cpf ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Ativo", f.Ativo);
        cmd.Parameters.AddWithValue("@Id", f.Id);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "DELETE FROM clientes WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}