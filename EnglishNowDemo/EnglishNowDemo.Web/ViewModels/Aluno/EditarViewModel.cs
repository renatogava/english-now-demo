using System.ComponentModel.DataAnnotations;

namespace EnglishNowDemo.Web.ViewModels.Aluno
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Login obrigatório")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Nome Completo obrigatório")]
        public string? NomeCompleto { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        public string? Email { get; set; }
    }
}
