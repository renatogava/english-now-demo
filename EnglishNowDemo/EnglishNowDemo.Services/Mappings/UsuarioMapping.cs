using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Models.Professor;
using EnglishNowDemo.Services.Models.Aluno;
using EnglishNowDemo.Services.Models.Usuario;
using EnglishNowDemo.Services.Enums;

namespace EnglishNowDemo.Services.Mappings
{
    public static class UsuarioMapping
    {
        public static Usuario MapToUsuario(this CriarProfessorRequest request)
        {
            var entity = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            };

            return entity;
        }

        public static Usuario MapToUsuario(this CriarAlunoRequest request)
        {
            var entity = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            };

            return entity;
        }

        public static UsuarioResult MapToUsuarioResult(this Usuario entity)
        {
            var result = new UsuarioResult
            {
                Id = entity.Id,
                Login = entity.Login,
                Senha = entity.Senha,
                Papel = (Papel)entity.PapelId
            };

            return result;
        }
    }
}
