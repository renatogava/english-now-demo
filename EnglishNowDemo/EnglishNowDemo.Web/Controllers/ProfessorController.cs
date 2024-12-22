using EnglishNowDemo.Services;
using EnglishNowDemo.Services.Models.Professor;
using EnglishNowDemo.Web.Mappings;
using EnglishNowDemo.Web.ViewModels.Professor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNowDemo.Web.Controllers
{
    [Route("professor")]
    [Authorize]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService) 
        {
            _professorService = professorService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToCriarProfessorRequest();

            var result = _professorService.Criar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Professor");
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var professor = _professorService.ObterPorId(id);

            var model = professor.MapToEditarViewModel();

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

            var request = model.MapToEditarProfessorRequest();

            var result = _professorService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }   

            return RedirectToAction("Listar", "Professor");
        }

        [HttpPost]
        [Route("excluir/{id}")]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _professorService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Professor");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var professores = _professorService.Listar();

            var professoresViewModel = professores.Select(c => c.MapToListarViewModel()).ToList();

            return View(professoresViewModel);
        }
    }
}