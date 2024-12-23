using EnglishNowDemo.Repositories.Entities;
using EnglishNowDemo.Services.Models.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Mappings
{
    public static class TurmaMapping
    {
        public static Turma MapToTurma(this CriarTurmaRequest model)
        {
            var entity = new Turma
            {
                Ano = Convert.ToInt32(model.Ano),
                Semestre = Convert.ToInt32(model.Semestre),
                Nivel = model.Nivel,
                Periodo = model.Periodo,
                ProfessorId = model.ProfessorId
            };

            return entity;
        }

        public static Turma MapToTurma(this EditarTurmaRequest model)
        {
            var entity = new Turma
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                Nivel = model.Nivel,
                Periodo = model.Periodo,
                ProfessorId = model.ProfessorId
            };

            return entity;
        }

        public static TurmaResult MapToTurmaResult(this Turma entity)
        {
            var model = new TurmaResult
            {
                Id = entity.Id,
                ProfessorId = entity.ProfessorId,
                ProfessorNome = entity.Professor.Nome,
                Ano = entity.Ano,
                Semestre = entity.Semestre,
                Nivel = entity.Nivel,
                Periodo = entity.Periodo
            };

            return model;
        }
    }
}
