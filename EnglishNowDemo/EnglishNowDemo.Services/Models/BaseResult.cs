using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services.Models
{
    public class BaseResult
    {
        public bool Sucesso { get; set; } = false;

        public string? MensagemErro { get; set; }

    }
}
