using ApiSistamasDeTarefas.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemaDeTarefas.Repositories.Repositorios
{
    public class IntervaloRepositorio : Contexto
    {
        private readonly Contexto _contexto;
        public IntervaloRepositorio(IConfiguration configuration) : base(configuration)
        {
            _contexto= new Contexto(configuration);
        }

        public void Inserir(Intervalo model)
        {
            string comandoSql = @"INSERT INTO Intervalo 
                                (HoraInicio, HoraFinal, NomeDesenvolvedor) 
                                    VALUES
                                (@HoraInicio, @HoraFinal, @NomeDesenvolvedor);";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Inicio", model.HoraInicio);
                cmd.Parameters.AddWithValue("@Fim", model.HoraFinal);
                cmd.Parameters.AddWithValue("@Fim", model.NomeDesenvolvedor);
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Intervalo model)
        {
            string comandoSql = @"UPDATE Intervalo 
                            SET 
                                HoraInicio = @HoraInicio,
                                HoraFinal = @HoraFinal
                            WHERE Id = @Id;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Inicio", model.HoraInicio);
                cmd.Parameters.AddWithValue("@Fim", model.HoraFinal);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o ID {model.Id}");
            }
        }

        public bool SeExiste(int id)
        {
            string comandoSql = @"SELECT COUNT(Id) as total FROM Intervalo WHERE Id = @Id";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public Intervalo? Obter(int id)
        {
            string comandoSql = @"SELECT Id, HoraInicio, HoraFinal, NomeDesenvolvedor
                FROM Intervalo WHERE Id = @Id;";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var intervalo = new Intervalo();
                        intervalo.Id = Convert.ToInt32(rdr["Id"]);
                        intervalo.HoraInicio = Convert.ToDateTime(rdr["HoraInicio"]);
                        intervalo.HoraFinal = Convert.ToDateTime(rdr["HoraFinal"]);
                        intervalo.NomeDesenvolvedor = Convert.ToString(rdr["NomeDesenvolvedor"]);

                        return intervalo;
                    }
                    else
                        return null;
                }
            }
        }

        public List<Intervalo> ObterTodos(int id)
        {
            List<Intervalo> intervalo = new List<Intervalo>();

            string comandoSql = @"SELECT Id, HoraInicio, HoraFinal, NomeDesenvolvedor FROM Intervalo";
            using (var cmd = new SqlCommand(comandoSql, _contexto, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        intervalo.Add(new Intervalo
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            HoraInicio = Convert.ToDateTime(rd["HoraInicio"]),
                            HoraFinal = Convert.ToDateTime(rd["HoraFinal"]),
                            NomeDesenvolvedor = rd["Nome"].ToString(),
                        });
                    }
                }
            }
            return intervalo;
        }
                
    }
}
