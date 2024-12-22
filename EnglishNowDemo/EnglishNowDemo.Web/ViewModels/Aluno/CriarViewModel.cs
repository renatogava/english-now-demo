﻿using System.ComponentModel.DataAnnotations;

namespace EnglishNowDemo.Web.ViewModels.Aluno
{
    public class CriarViewModel
    {
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
