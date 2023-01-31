using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ApiSistemaDeTarefas.Repositories
{
    public class Contexto
    {
        internal readonly SqlConnection _conn;
        public Contexto(IConfiguration configuration)
        {
            _conn = new SqlConnection(configuration["DbCredentials"]);
        }

        public void AbrirConexao()
        {
            _conn.Open();
        }

        public void FecharConexao()
        {
            _conn.Close();
        }

    }
}