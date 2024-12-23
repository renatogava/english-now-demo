using EnglishNowDemo.Repositories;
using EnglishNowDemo.Services.Mappings;
using EnglishNowDemo.Services.Models.Aluno;
using EnglishNowDemo.Services.Models.Turma;

namespace EnglishNowDemo.Services
{
    public interface ITurmaService
    {
        CriarTurmaResult Criar(CriarTurmaRequest request);

        EditarTurmaResult Editar(EditarTurmaRequest request);

        ExcluirTurmaResult Excluir(int id);

        TurmaResult ObterPorId(int id);

        IList<TurmaResult> Listar();
    }

    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public CriarTurmaResult Criar(CriarTurmaRequest request)
        {
            var result = new CriarTurmaResult();

            var turma = request.MapToTurma();

            _turmaRepository.Inserir(turma);

            result.Sucesso = true;

            return result;
        }

        public EditarTurmaResult Editar(EditarTurmaRequest request)
        {
            var result = new EditarTurmaResult();

            var turma = request.MapToTurma();

            var affectedRows = _turmaRepository.Atualizar(turma);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Turma não foi atualizada";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirTurmaResult Excluir(int id)
        {
            var result = new ExcluirTurmaResult();

            var professor = _turmaRepository.ObterPorId(id);

            if (professor == null)
            {
                result.MensagemErro = "Turma não existe";
                return result;
            }

            var affectedRows = _turmaRepository.Apagar(id);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Turma não foi excluída";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<TurmaResult> Listar()
        {
            var professores = _turmaRepository.Listar();

            var result = professores.Select(c => c.MapToTurmaResult()).ToList();

            return result;
        }

        public TurmaResult ObterPorId(int id)
        {
            var result = new TurmaResult();

            var turma = _turmaRepository.ObterPorId(id);

            if (turma == null)
            {
                result.MensagemErro = "Turma não localizada";
                return result;
            }

            result = turma.MapToTurmaResult();

            return result;
        }
    }
}
