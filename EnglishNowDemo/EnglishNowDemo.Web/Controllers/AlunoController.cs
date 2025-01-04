using EnglishNowDemo.Services;
using EnglishNowDemo.Services.Models.Aluno;
using EnglishNowDemo.Web.Mappings;
using EnglishNowDemo.Web.ViewModels.Aluno;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNowDemo.Web.Controllers
{
    [Route("aluno")]
    [Authorize]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService) 
        {
            _alunoService = alunoService;
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

            var request = model.MapToCriarAlunoRequest();

            var result = _alunoService.Criar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Aluno");
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);

            var model = aluno.MapToEditarViewModel();

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

            var request = model.MapToEditarAlunoRequest();

            var result = _alunoService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }   

            return RedirectToAction("Listar", "Aluno");
        }

        [HttpPost]
        [Route("excluir/{id}")]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _alunoService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", "Aluno");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            IList<AlunoResult>? alunos = null;

            if (User.IsInRole("Professor"))
            {
                var usuarioId = Convert.ToInt32(User.FindFirst("Id")?.Value);

                alunos = _alunoService.ListarPorUsuarioProfessor(usuarioId);
            }
            else
            {
                alunos = _alunoService.Listar();
            }

            var viewModel = new ListarViewModel
            {
                Alunos = alunos.Select(c => c.MapToListarViewModel()).ToList(),
                ExibirBotaoInserirEditar = User.IsInRole("Administrador")
            };

            return View(viewModel);
        }
    }
}