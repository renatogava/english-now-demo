using EnglishNowDemo.Repositories;
using EnglishNowDemo.Services;
using EnglishNowDemo.Web.Mappings;
using EnglishNowDemo.Web.ViewModels.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNowDemo.Web.Controllers
{
    [Route("turma")]
    [Authorize]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IProfessorService _professorService;
        private readonly IAlunoService _alunoService;

        public TurmaController(ITurmaService turmaService, IProfessorService professorService, IAlunoService alunoService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
            _alunoService = alunoService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            var model = new CriarViewModel
            {
                Professores = ListarProfessores(_professorService.Listar()),
                Semestres = ListarSemestres()
            };

            return View(model);
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToCriarTurmaRequest();

            var result = _turmaService.Criar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Turma");
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var turma = _turmaService.ObterPorId(id);

            if (!turma.Sucesso)
            {
                return RedirectToAction("Listar", "Turma");
            }

            var model = turma.MapToEditarViewModel();

            model.Professores = ListarProfessores(_professorService.Listar());

            model.Semestres = ListarSemestres();

            model.AlunosTurma = _alunoService.ListarPorTurma(id)
                .Select(c => c.MapToAlunoTurmaViewModel())
                .ToList();

            model.Alunos = _alunoService.Listar()
                .Select(c => c.MapToAlunoTurmaViewModel())
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToEditarTurmaRequest();

            var result = _turmaService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Turma");
        }

        [HttpPost]
        [Route("selecionarAlunos")]
        public IActionResult SelecionarAlunos(int turmaId)
        {
            var alunosSelecioados = new List<int>();

            foreach (var formItem in Request.Form)
            {
                if (formItem.Key.StartsWith("aluno_"))
                {
                    var alunoId = Convert.ToInt32(formItem.Key.Split('_')[1]);

                    alunosSelecioados.Add(alunoId);
                }
            }

            _turmaService.AssociarAlunos(turmaId, alunosSelecioados);

            return RedirectToAction("Editar", "Turma", new { id = turmaId });
        }

        [HttpPost]
        [Route("excluir/{id}")]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _turmaService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Turma");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var turmas = _turmaService.Listar();

            var turmasViewModel = turmas.Select(c => c.MapToListarViewModel()).ToList();

            return View(turmasViewModel);
        }

        private List<SelectListItem> ListarSemestres()
        {
            var semestres = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "1º Semestre"
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = "2º Semestre"
                }
            };

            return semestres;
        }

        private List<SelectListItem> ListarProfessores(IList<Services.Models.Professor.ProfessorResult> professores)
        {
            var lista = professores
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }).ToList();

            return lista;
        }
    }
}