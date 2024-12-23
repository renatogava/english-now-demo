using EnglishNowDemo.Services.Models.Turma;
using EnglishNowDemo.Web.ViewModels.Turma;

namespace EnglishNowDemo.Web.Mappings
{
    public static class TurmaMapping
    {
        public static CriarTurmaRequest MapToCriarTurmaRequest(this CriarViewModel viewModel)
        {
            var model = new CriarTurmaRequest
            {
                Ano = viewModel.Ano!.Value,
                Semestre = viewModel.Semestre,
                ProfessorId = viewModel.ProfessorId,
                Nivel = viewModel.Nivel,
                Periodo = viewModel.Periodo
            };

            return model;
        }

        public static EditarTurmaRequest MapToEditarTurmaRequest(this EditarViewModel viewModel)
        {
            var model = new EditarTurmaRequest
            {
                Id = viewModel.Id,
                Ano = viewModel.Ano,
                Semestre = viewModel.Semestre,
                ProfessorId = viewModel.ProfessorId,
                Nivel = viewModel.Nivel,
                Periodo = viewModel.Periodo
            };

            return model;
        }

        public static ListarViewModel MapToListarViewModel(this TurmaResult model)
        {
            var viewModel = new ListarViewModel
            {
                Id = model.Id,
                SemestreAno = $"{model.Semestre}º Semestre/{model.Ano}",
                Professor = model.ProfessorNome,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return viewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this TurmaResult model)
        {
            var viewModel = new EditarViewModel
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return viewModel;
        }
    }
}
