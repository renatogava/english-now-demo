using System.ComponentModel.DataAnnotations;

namespace EnglishNowDemo.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login obrigatório")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        public string? Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
