using ApiSistamasDeTarefas.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;


namespace ApiSistemaDeTarefas.Repositories.Repositorios
{
    public class UserRepositorio : Contexto 
    {
        public UserRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public void Login(string email, string password)
        {
            string comandoSql = @"SELECT COUNT(Username) as total FROM User WHERE Username = @Username AND Password = @Password";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Username", email);
                cmd.Parameters.AddWithValue("@Password", password);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new User()
                        {
                            Email = rdr["Nome"].ToString(),
                            Password = rdr["Email"].ToString()
                        };
                    }
                    else
                        return null;
                }
            }
        }
    }
}
