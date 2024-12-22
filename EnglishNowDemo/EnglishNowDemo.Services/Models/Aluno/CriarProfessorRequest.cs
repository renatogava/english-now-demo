using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models.Aluno
{
    public class CriarAlunoRequest
    {
        public string? Login { get; set; }

        public string? Senha { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }
    }
}