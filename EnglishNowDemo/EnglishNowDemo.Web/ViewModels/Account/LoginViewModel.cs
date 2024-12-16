using System.ComponentModel.DataAnnotations;

namespace EnglishNowDemo.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        public string? Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
