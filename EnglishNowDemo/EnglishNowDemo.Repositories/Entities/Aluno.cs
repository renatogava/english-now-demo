using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Repositories.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
