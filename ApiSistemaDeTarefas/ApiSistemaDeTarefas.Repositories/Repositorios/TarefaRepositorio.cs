using ApiSistamasDeTarefas.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemaDeTarefas.Repositories.Repositorios
{
    public class TarefaRepositorio : Contexto
    {
        private List<Tarefa> tarefasA;

        private readonly Contexto _contexto;
        public TarefaRepositorio(IConfiguration configuration) : base(configuration)
        {
            _contexto = new Contexto(configuration);
            tarefasA = new List<Tarefa>();

        }

        public void CadastrarTarefa(Tarefa tarefa)
        {
            string sql = "INSERT INTO Tarefa (Nome, Empresa, HoraInicio, HoraFinal, UsuarioId) " +
                         "VALUES (@Nome, @Empresa, @HoraInicio, @HoraFinal, @UsuarioId)";

            using (var cmd = new SqlCommand(sql, _contexto._conn))
            {
                cmd.Parameters.AddWithValue("@Nome", tarefa.Nome);
                cmd.Parameters.AddWithValue("@Empresa", tarefa.Empresa);
                cmd.Parameters.AddWithValue("@HoraInicio", tarefa.HoraInicio);
                cmd.Parameters.AddWithValue("@HoraFinal", tarefa.HoraFinal);
                cmd.Parameters.AddWithValue("@UsuarioId", tarefa.UsuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Tarefa model)
        {
            string comandoSql = @"UPDATE Tarefa 
                                SET 
                                    Nome = @Nome,
                                    HoraInicio = @HoraInicio,
                                    HoraFinal = @HoraFinal, 
                                    Empresa = @Empresa
                                WHERE Nome = @Nome;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Nome", model.Nome);
                cmd.Parameters.AddWithValue("@HoraInicio", model.HoraInicio);
                cmd.Parameters.AddWithValue("@HoraFinal", model.HoraFinal);
                cmd.Parameters.AddWithValue("@Empresa", model.Empresa);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cnpj {model.Nome}");
            }
        }


        public bool SeExiste(string tarefa)
        {
            string comandoSql = @"SELECT COUNT(Tarefa) as total FROM Tarefa WHERE Nome= @Nome";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Nome", tarefa);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public void Deletar(string nomeDaTarefa)
        {
            string comandoSql = @"DELETE FROM Tarefa 
                    WHERE Nome = @Nome;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Nome", nomeDaTarefa);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o nome {nomeDaTarefa}");
            }
        }

        public List<Tarefa> ObterTarefas(int usuarioId)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            string sql = "SELECT Id, Nome, Empresa, HoraInicio, HoraFinal, UsuarioId " +
                         "FROM Tarefa " +
                         "WHERE UsuarioId = @UsuarioId";

            using (var cmd = new SqlCommand(sql, _contexto._conn))
            {
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        tarefas.Add(new Tarefa
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Nome = rd["Nome"].ToString(),
                            Empresa = rd["Empresa"].ToString(),
                            HoraInicio = Convert.ToDateTime(rd["HoraInicio"]),
                            HoraFinal = Convert.ToDateTime(rd["HoraFinal"]),
                            UsuarioId = Convert.ToInt32(rd["UsuarioId"])
                        });
                    }
                }
            }

            return tarefas;
        }
    


        public abstract class ExibicaoTarefa
        {
            public abstract void ExibirTarefas(List<Tarefa> tarefasA);

            public abstract List<Tarefa> OrdenarTarefas(List<Tarefa> tarefasA);
        }


        public class ExibicaoTarefaLista : ExibicaoTarefa
        {
            public override void ExibirTarefas(List<Tarefa> tarefasA)
            {
                Console.WriteLine("Exibição da lista de tarefas:");
                foreach (var tarefa in tarefasA)
                {
                    Console.WriteLine("Data e hora Inicio: {0} - Data e hora Final: {1} - Empresa: {2} - Descrição: {3}",
                    tarefa.HoraInicio.ToString("dd/MM/yyyy"), tarefa.HoraFinal.ToString("HH:mm"), tarefa.Empresa, tarefa.Descricao);
                }
            }

            public override List<Tarefa> OrdenarTarefas(List<Tarefa> tarefas)
            {
            return tarefas.OrderBy(t => t.HoraInicio).ThenBy(t => t.HoraFinal).ToList();
            }
        }



    }
}


