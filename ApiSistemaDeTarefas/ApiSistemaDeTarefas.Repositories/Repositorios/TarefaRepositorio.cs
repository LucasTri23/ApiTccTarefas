using ApiSistamasDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistemaDeTarefas.Repositories.Repositorios
{
    public class TarefaRepositorio
    {
        private readonly Contexto _context;

        public TarefaRepositorio(Contexto context)
        {
            _context = context;
        }

        public void Adicionar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();
        }

        public Tarefa ObterPorId(int id)
        {
            return _context.Tarefas.Find(id);
        }

        public IQueryable<Tarefa> ObterTodos()
        {
            return _context.Tarefas.Include(x => x.Usuario);
        }

        public void Remover(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
        }

        public IQueryable<Tarefa> ObterTarefasDoDia()
        {
            var hoje = DateTime.Today;
            return _context.Tarefas.Where(x => x.HoraInicio.Date == hoje).Include(x => x.Usuario);
        }

        public IQueryable<Tarefa> ObterTarefasDaSemana()
        {
            var hoje = DateTime.Today;
            var primeiroDiaDaSemana = hoje.AddDays(-(int)hoje.DayOfWeek + (int)DayOfWeek.Monday);
            var ultimoDiaDaSemana = primeiroDiaDaSemana.AddDays(7).AddSeconds(-1);
            return _context.Tarefas.Where(x => x.HoraInicio >= primeiroDiaDaSemana && x.HoraInicio <= ultimoDiaDaSemana).Include(x => x.Usuario);
        }

        public IQueryable<Tarefa> ObterTarefasDoMes()
        {
            var hoje = DateTime.Today;
            var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);
            var ultimoDiaDoMes = primeiroDiaDoMes.AddMonths(1).AddDays(-1);
            return _context.Tarefas.Where(x => x.HoraInicio >= primeiroDiaDoMes && x.HoraInicio <= ultimoDiaDoMes).Include(x => x.Usuario);
        }


    }
}
