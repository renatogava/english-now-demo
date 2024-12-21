using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models.Professor
{
    public class EditarProfessorRequest
    {
        public int Id { get; set; }

        public string? Login { get; set; }

        public string? Senha { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public int UsuarioId { get; set; }

    }
}
