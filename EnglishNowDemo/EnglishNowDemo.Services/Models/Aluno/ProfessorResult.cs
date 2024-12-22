using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models.Aluno
{
    public class AlunoResult : BaseResult
    {
        public int Id { get; set; }

        public int Usuarioid { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }

        public string? Senha { get; set; }
    }
}
