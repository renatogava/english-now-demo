using EnglishNowDemo.Repositories;
using EnglishNowDemo.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Services
{
    public interface IUsuarioService
    {
        Task<LoginResult> LoginAsync(string usuario, string senha);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResult> LoginAsync(string nome, string senha)
        {
            var result = new LoginResult();

            if (string.IsNullOrEmpty(nome))
            {
                result.MensagemErro = "Usuário vazio";

                return result;
            }

            if (string.IsNullOrEmpty(senha))
            {
                result.MensagemErro = "Senha vazia";

                return result;
            }

            var usuario = await _usuarioRepository.ObterPorNomeAsync(nome);

            if (usuario == null)
            {
                result.MensagemErro = "Usuário não encontrado";

                return result;
            }

            if (usuario.Senha != senha)
            {
                result.MensagemErro = "Senha inválida";

                return result;
            }

            //se chegou até aqui, é pq funcionou

            return result;
        }
    }
}
