using EnglishNowDemo.Services.Enums;

namespace EnglishNowDemo.Services.Models.Usuario
{
    public class ValidarLoginResult : BaseResult
    {
        public UsuarioResult? Usuario { get; set; }
    }

    public class UsuarioResult
    {
        public int Id { get; set; }

        public string? Login { get; set; }

        public string? Senha { get; set; }

        public Papel? Papel { get; set; }
    }
}