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
    public class EmpresaRepositorio : Contexto
    {
        public EmpresaRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public void Inserir(Empresa model)
        {
            string comandoSql = @"INSERT INTO Empresa 
                                    (Cnpj, Nome, DataDeCadastro, RazaoSocial) 
                                        VALUES
                                    (@Cnpj, @Nome, @DataDeCadastro, @RazaoSocial);";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                cmd.Parameters.AddWithValue("@Nome", model.Nome);
                cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                cmd.Parameters.AddWithValue("@DataDeCadastro", model.DataDeCadastro);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Empresa model)
        {
            string comandoSql = @"UPDATE Empresa 
                                SET 
                                    Nome = @Nome,
                                    DataDeCadastro = @DataDeCadastro, 
                                    RazaoSocial = @RazaoSocial 
                                WHERE Cnpj = @Cnpj;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", model.Cnpj);
                cmd.Parameters.AddWithValue("@Nome", model.Nome);
                cmd.Parameters.AddWithValue("@RazaoSocial", model.RazaoSocial);
                cmd.Parameters.AddWithValue("@DataDeCadastro", model.DataDeCadastro);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cnpj {model.Cnpj}");
            }
        }
        public bool SeExiste(string cnpjEmpresa)
        {
            string comandoSql = @"SELECT COUNT(Cnpj) as total FROM Empresa WHERE Cnpj = @Cnpj";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CnpjEmpresa", cnpjEmpresa);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public Empresa? Obter(string cnpj)
        {
            string comandoSql = @"SELECT Cnpj, Nome, DataDeCadastro, RazaoSocial
            FROM Empresa WHERE Cnpj = @Cnpj;";
            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", cnpj);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var empresa = new Empresa();
                        empresa.Cnpj = Convert.ToString(rdr["Cnpj"]);
                        empresa.Nome = Convert.ToString(rdr["Nome"]);
                        empresa.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        empresa.DataDeCadastro = Convert.ToDateTime(rdr["DataDeCadastro"]);

                        return empresa;
                    }
                    else
                        return null;
                }
            }
        }

        public List<Empresa> ProcurarEmpresaPorCnpj(string? cnpj)
        {
            string comandoSql = @"SELECT Cnpj FROM Empresa";

            if (!string.IsNullOrWhiteSpace(cnpj))
                comandoSql += " WHERE cnpj LIKE @cnpj";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(cnpj))
                    cmd.Parameters.AddWithValue("@cnpj", "%" + cnpj + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var empresa = new List<Empresa>();
                    while (rdr.Read())
                    {
                        var empresas = new Empresa();
                        empresas.Cnpj = Convert.ToString(rdr["CpfCliente"]);
                        empresas.Nome = Convert.ToString(rdr["Nome"]);
                        empresas.DataDeCadastro = Convert.ToDateTime(rdr["Nascimento"]);
                        empresas.RazaoSocial = Convert.ToString(rdr["RazaoSocial"]);
                        empresa.Add(empresas);
                    }
                    return empresa;
                }
            }
        }

        public void Deletar(string cnpj)
        {
            string comandoSql = @"DELETE FROM Empresa 
                                WHERE Cnpj = @Cnpj;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cnpj", cnpj);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cpf {cnpj}");
            }
        }
    }
}
