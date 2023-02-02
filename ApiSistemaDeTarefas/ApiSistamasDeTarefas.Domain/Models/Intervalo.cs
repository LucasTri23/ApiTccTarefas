﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSistamasDeTarefas.Domain.Models
{
    public class Intervalo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime HoraInicio { get; set; }

        [Required]
        public DateTime HoraFinal { get; set; }

        [Required]
        public int Duracao { get; set; }

        [Required]
        public string NomeDesenvolvedor { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
