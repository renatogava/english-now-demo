using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Models.Professor;
using EnglishNowDemo.Services.Models.Aluno;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
