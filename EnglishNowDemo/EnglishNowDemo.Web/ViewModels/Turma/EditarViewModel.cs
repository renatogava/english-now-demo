using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnglishNowDemo.Web.ViewModels.Turma
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Professor obrigatório")]
        public int ProfessorId { get; set; }
        public List<SelectListItem>? Professores { get; set; }


        [Required(ErrorMessage = "Ano obrigatório")]
        public int Ano { get; set; }
        public List<SelectListItem>? Anos { get; set; }


        [Required(ErrorMessage = "Semestre obrigatório")]
        public int Semestre { get; set; }
        public List<SelectListItem>? Semestres { get; set; }


        [Required(ErrorMessage = "Nivel obrigatório")]
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "Período obrigatório")]
        public string? Periodo { get; set; }

        public IList<AlunoTurmaViewModel>? AlunosTurma { get; set; }

        public IList<AlunoTurmaViewModel>? Alunos { get; set; }
    }

    public class AlunoTurmaViewModel
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }
    }
}
