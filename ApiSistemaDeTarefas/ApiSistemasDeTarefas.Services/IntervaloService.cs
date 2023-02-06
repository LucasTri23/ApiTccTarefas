using ApiSistamasDeTarefas.Domain.Models;
using ApiSistemaDeTarefas.Repositories.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemasDeTarefas.Services
{
    public class IntervaloService
    {
        private readonly IntervaloRepositorio _repositorio;
        public IntervaloService(IntervaloRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Intervalo Listar(int id)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(id);
                return _repositorio.Obter(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Inserir(Intervalo model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Atualizar(Intervalo model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public Intervalo? Obter(int id)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.Obter(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public bool Existe(int id)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.SeExiste(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
    }
}
