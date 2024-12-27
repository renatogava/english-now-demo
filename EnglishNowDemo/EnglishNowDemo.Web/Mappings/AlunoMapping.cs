using EnglishNowDemo.Services.Models.Aluno;
using EnglishNowDemo.Web.ViewModels.Aluno;

namespace EnglishNowDemo.Web.Mappings
{
    public static class AlunoMapping
    {
        public static CriarAlunoRequest MapToCriarAlunoRequest(this CriarViewModel viewModel)
        {
            var model = new CriarAlunoRequest
            {
                Email = viewModel.Email,
                Login = viewModel.Login,
                Nome = viewModel.NomeCompleto,
                Senha = viewModel.Senha
            };

            return model;
        }

        public static EditarAlunoRequest MapToEditarAlunoRequest(this EditarViewModel viewModel)
        {
            var model = new EditarAlunoRequest
            {
                Id = viewModel.Id,
                UsuarioId = viewModel.UsuarioId,
                Email = viewModel.Email,
                Login = viewModel.Login,
                Nome = viewModel.NomeCompleto,
                Senha = viewModel.Senha
            };

            return model;
        }

        public static AlunoViewModel MapToListarViewModel(this AlunoResult model)
        {
            var viewModel = new AlunoViewModel
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                Nome = model.Nome
            };

            return viewModel;
        }

        public static ViewModels.Turma.AlunoTurmaViewModel MapToAlunoTurmaViewModel(this AlunoResult model)
        {
            var viewModel = new ViewModels.Turma.AlunoTurmaViewModel
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                Nome = model.Nome
            };

            return viewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this AlunoResult model)
        {
            var viewModel = new EditarViewModel
            {
                Id = model.Id,
                UsuarioId = model.Usuarioid,
                Email = model.Email,
                Login = model.Login,
                NomeCompleto = model.Nome,
                Senha = model.Senha
            };

            return viewModel;
        }
    }
}
