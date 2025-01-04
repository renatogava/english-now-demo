using EnglishNowDemo.Repositories;
using EnglishNowDemo.Services.Mappings;
using EnglishNowDemo.Services.Models.Aluno;

namespace EnglishNowDemo.Services
{
    public interface IAlunoService
    {
        CriarAlunoResult Criar(CriarAlunoRequest request);

        EditarAlunoResult Editar(EditarAlunoRequest request);

        ExcluirAlunoResult Excluir(int id);

        AlunoResult ObterPorId(int id);

        IList<AlunoResult> Listar();

        IList<AlunoResult> ListarPorUsuarioProfessor(int usuarioId);

        IList<AlunoResult> ListarPorTurma(int turmaId);
    }

    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AlunoService(IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository)
        {
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarAlunoResult Criar(CriarAlunoRequest request)
        {
            var result = new CriarAlunoResult();

            if (string.IsNullOrEmpty(request.Login))
            {
                result.MensagemErro = "Login vazio";
                return result;
            }

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                result.MensagemErro = "Esse login já existe";
                return result;
            }

            var usuario = request.MapToUsuario();

            var usuarioId = _usuarioRepository.Inserir(usuario);

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "Usuário não foi criado";
                return result;
            }

            var aluno = request.MapToAluno(usuarioId.Value);

            _alunoRepository.Inserir(aluno);

            result.Sucesso = true;

            return result;
        }

        public EditarAlunoResult Editar(EditarAlunoRequest request)
        {
            var result = new EditarAlunoResult();

            if (string.IsNullOrEmpty(request.Login))
            {
                result.MensagemErro = "Login vazio";
                return result;
            }

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Esse login já existe";
                return result;
            }

            var aluno = request.MapToAluno();

            var affectedRows = _alunoRepository.Atualizar(aluno);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Aluno não foi atualizado";
                return result;
            }

            var usuario = request.MapToUsuario();

            affectedRows = _usuarioRepository.Atualizar(usuario);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Usuário não foi atualizado";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirAlunoResult Excluir(int id)
        {
            var result = new ExcluirAlunoResult();

            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                result.MensagemErro = "Aluno não existe";
                return result;
            }

            var affectedRows = _alunoRepository.Apagar(id);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Aluno não foi excluído";
                return result;
            }

            affectedRows = _usuarioRepository.Apagar(aluno.UsuarioId);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Usuário não foi excluído";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<AlunoResult> Listar()
        {
            var alunos = _alunoRepository.Listar();

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorTurma(int turmaId)
        {
            var alunos = _alunoRepository.ListarPorTurma(turmaId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorUsuarioProfessor(int usuarioId)
        {
            var alunos = _alunoRepository.ListarPorUsuarioProfessor(usuarioId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public AlunoResult ObterPorId(int id)
        {
            var result = new AlunoResult();

            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                result.MensagemErro = "Aluno não localizado";
                return result;
            }

            result = aluno.MapToAlunoResult();

            return result;
        }
    }
}
