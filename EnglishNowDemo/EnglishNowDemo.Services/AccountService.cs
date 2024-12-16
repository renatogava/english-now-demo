using EnglishNowDemo.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services
{
    public interface IAccountService
    {
        Task<LoginResult> LoginAsync(string usuario, string senha);
    }

    public class AccountService : IAccountService
    {
        public Task<LoginResult> LoginAsync(string usuario, string senha)
        {
            var result = new LoginResult();

            if (string.IsNullOrEmpty(usuario))
            {
                result.MensagemErro = "Usuário vazio";

                return Task.FromResult(result);
            }

            if (string.IsNullOrEmpty(senha))
            {
                result.MensagemErro = "Senha vazia";

                return Task.FromResult(result);
            }

            if (usuario != "teste" || senha != "123")
            {
                result.MensagemErro = "Usuário ou senha inválidos";

                return Task.FromResult(result);
            }

            //se chegou até aqui, é pq funcionou

            return Task.FromResult(result);
        }
    }
}
