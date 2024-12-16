using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models
{
    public class LoginResult
    {
        public bool LoginEfetuado => string.IsNullOrEmpty(MensagemErro);

        public string? MensagemErro { get; set; }
    }
}
