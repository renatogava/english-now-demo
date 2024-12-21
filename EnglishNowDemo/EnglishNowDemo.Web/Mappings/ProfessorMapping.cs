using EnglishNowDemo.Services.Models.Professor;
using EnglishNowDemo.Web.ViewModels.Professor;

namespace EnglishNowDemo.Web.Mappings
{
    public static class ProfessorMapping
    {
        public static CriarProfessorRequest MapToCriarProfessorRequest(this CriarViewModel viewModel)
        {
            var model = new CriarProfessorRequest
            {
                Email = viewModel.Email,
                Login = viewModel.Login,
                Nome = viewModel.NomeCompleto,
                Senha = viewModel.Senha
            };

            return model;
        }

        public static EditarProfessorRequest MapToEditarProfessorRequest(this EditarViewModel viewModel)
        {
            var model = new EditarProfessorRequest
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

        public static ListarViewModel MapToListarViewModel(this ProfessorResult model)
        {
            var viewModel = new ListarViewModel
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                Nome = model.Nome
            };

            return viewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this ProfessorResult model)
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
