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

        public TurmaController(ITurmaService turmaService, IProfessorService professorService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
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

            var model = turma.MapToEditarViewModel();

            model.Professores = ListarProfessores(_professorService.Listar());
            model.Semestres = ListarSemestres();

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