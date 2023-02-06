using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemaDeTarefas.Repositories.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemasDeTarefas.Services
{
    public class TarefaService
    {
        private readonly TarefaRepositorio _repositorio;
        public TarefaService(TarefaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Tarefa> Listar(int usuarioId)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ObterTarefas(usuarioId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Atualizar(Tarefa model)
        {
            try
            {
                ValidarTarefa(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(string tarafa)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(tarafa);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Tarefa model)
        {
            try
            {
                ValidarTarefa(model);
                _repositorio.AbrirConexao();
                _repositorio.CadastrarTarefa(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private void ValidarTarefa(Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Nome))
                throw new Exception("O nome da tarefa é obrigatório.");
            if (string.IsNullOrWhiteSpace(tarefa.Empresa))
                throw new Exception("A empresa da tarefa é obrigatória.");
            if (tarefa.HoraInicio == default)
                throw new Exception("A hora de inicio é obrogatória!");
            if (tarefa == null)
                throw new ArgumentNullException(nameof(tarefa));
            if (string.IsNullOrWhiteSpace(tarefa.Nome))
                throw new ArgumentException("O título da tarefa é obrigatório.", nameof(tarefa.Nome));
            if (tarefa.HoraInicio < tarefa.HoraFinal)
                throw new ArgumentException("A data de conclusão não pode ser anterior à data de criação.", nameof(tarefa.HoraFinal));
        }
    }
}
