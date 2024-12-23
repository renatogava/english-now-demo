using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Repositories.Entities
{
    public class Turma
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public string? Periodo { get; set; }
        public string? Nivel { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
    }
}
