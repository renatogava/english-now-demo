using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Models.Professor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Mappings
{
    public static class ProfessorMapping
    {
        public static Professor MapToProfessor(this CriarProfessorRequest model, int usuarioId)
        {
            var entity = new Professor
            {
                Email = model.Email,
                Nome = model.Nome,
                UsuarioId = usuarioId
            };

            return entity;
        }

        public static Professor MapToProfessor(this EditarProfessorRequest model)
        {
            var entity = new Professor
            {
                Id = model.Id,
                Email = model.Email,
                Nome = model.Nome,
                UsuarioId = model.UsuarioId
            };

            return entity;
        }

        public static Usuario MapToUsuario(this EditarProfessorRequest model)
        {
            var entity = new Usuario
            {
                Id = model.UsuarioId,
                Login = model.Login,
                Senha = model.Senha
            };

            return entity;
        }

        public static ProfessorResult MapToProfessorResult(this Professor entity)
        {
            var model = new ProfessorResult
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
