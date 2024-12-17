using EnglishNowDemo.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorNomeAsync(string nome);
    }

    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(string connectionString) : base(connectionString) { }

        public async Task<Usuario?> ObterPorNomeAsync(string nome)
        {
            Usuario? usuario = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = $"select usuario_id, nome, senha from usuario where nome = '{nome}'";

                var cmd = new MySqlCommand(query, cnn);

                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Senha = reader.GetString(2),
                        };
                    }
                }

                await cnn.CloseAsync();
            }

            return usuario;
        }
    }
}
