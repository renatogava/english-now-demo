using EnglishNowDemo.Repositories;
using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Mappings;
using EnglishNowDemo.Services.Models.Professor;

namespace EnglishNowDemo.Services
{
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request);

        EditarProfessorResult Editar(EditarProfessorRequest request);

        ProfessorResult ObterPorId(int id);

        IList<ProfessorResult> Listar();
    }

    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProfessorService(IProfessorRepository professorRepository, IUsuarioRepository usuarioRepository)
        {
            _professorRepository = professorRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarProfessorResult Criar(CriarProfessorRequest request)
        {
            var result = new CriarProfessorResult();

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

            var professor = request.MapToProfessor(usuarioId.Value);

            _professorRepository.Inserir(professor);

            result.Sucesso = true;

            return result;
        }

        public EditarProfessorResult Editar(EditarProfessorRequest request)
        {
            var result = new EditarProfessorResult();

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

            var professor = request.MapToProfessor();

            var affectedRows = _professorRepository.Atualizar(professor);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Professor não foi atualizado";
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

        public IList<ProfessorResult> Listar()
        {
            var professores = _professorRepository.Listar();

            var result = professores.Select(c => c.MapToProfessorResult()).ToList();

            return result;
        }

        public ProfessorResult ObterPorId(int id)
        {
            var result = new ProfessorResult();

            var professor = _professorRepository.ObterPorId(id);

            if (professor == null)
            {
                result.MensagemErro = "Professor não localizado";
                return result;
            }

            result = professor.MapToProfessorResult();

            return result;
        }
    }
}
