using ApiSistamasDeTarefas.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;


namespace ApiSistemaDeTarefas.Repositories.Repositorios
{
    public class UsuarioRepositorio : Contexto 
    {
        private readonly Contexto _contexto;
        public UsuarioRepositorio(IConfiguration configuration) : base(configuration)
        {
            _contexto = new Contexto(configuration);
        }
        public Usuario ValidarUsuario(string email, string senha)
        {
            Usuario usuario = null;

            string sql = "SELECT Id, Email, Senha, NivelDeAcesso, EmpresaId " +
                         "FROM Usuario " +
                         "WHERE Email = @Email AND Senha = @Senha";

            using (var cmd = new SqlCommand(sql, _contexto._conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Email = rd["Email"].ToString(),
                            Senha = rd["Senha"].ToString(),
                            NivelDeAcesso = rd["NivelDeAcesso"].ToString(),
                            EmpresaId = Convert.ToInt32(rd["EmpresaId"])
                        };
                    }
                }
            }

            return usuario;
        }
    }
}
