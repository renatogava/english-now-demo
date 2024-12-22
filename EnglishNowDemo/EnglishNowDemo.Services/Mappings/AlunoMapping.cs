using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Models.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Mappings
{
    public static class AlunoMapping
    {
        public static Aluno MapToAluno(this CriarAlunoRequest model, int usuarioId)
        {
            var entity = new Aluno
            {
                Email = model.Email,
                Nome = model.Nome,
                UsuarioId = usuarioId
            };

            return entity;
        }

        public static Aluno MapToAluno(this EditarAlunoRequest model)
        {
            var entity = new Aluno
            {
                Id = model.Id,
                Email = model.Email,
                Nome = model.Nome,
                UsuarioId = model.UsuarioId
            };

            return entity;
        }

        public static Usuario MapToUsuario(this EditarAlunoRequest model)
        {
            var entity = new Usuario
            {
                Id = model.UsuarioId,
                Login = model.Login,
                Senha = model.Senha
            };

            return entity;
        }

        public static AlunoResult MapToAlunoResult(this Aluno entity)
        {
            var model = new AlunoResult
            {
                Id = entity.Id,
                Email = entity.Email,
                Nome = entity.Nome,
                Login = entity.Usuario?.Login,
                Senha = entity.Usuario?.Senha,
                Usuarioid = entity.UsuarioId
            };

            return model;
        }
    }
}
