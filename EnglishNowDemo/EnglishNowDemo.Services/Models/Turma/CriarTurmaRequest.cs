using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models.Turma
{
    public class CriarTurmaRequest
    {
        public int Ano { get; set; }

        public int Semestre { get; set; }

        public int ProfessorId { get; set; }

        public string? Nivel { get; set; }

        public string? Periodo { get; set; }
    }
}