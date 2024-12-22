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
        Usuario? ObterPorLogin(string login);

        int? Inserir(Usuario usuario);

        int? Apagar(int id);

        int? Atualizar(Usuario usuario);
    }

    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(string connectionString) : base(connectionString) { }

        public Usuario? ObterPorLogin(string login)
        {
            Usuario? usuario = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = $"select usuario_id, login, senha from usuario where login = '{login}'";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            Senha = reader.GetString(2),
                        };
                    }
                }
            }

            return usuario;
        }

        public int? Inserir(Usuario usuario)
        {
            int? usuarioId = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"insert into usuario (login, senha, papel_id) values ('{usuario.Login}', '{usuario.Senha}', {usuario.PapelId});
                                    select LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                usuarioId = Convert.ToInt32(cmd.ExecuteScalar());

                usuario.Id = usuarioId.Value;
            }

            return usuarioId;
        }

        public int? Apagar(int id)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"delete from usuario where usuario_id = {id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }

        public int? Atualizar(Usuario usuario)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"update usuario set login = '{usuario.Login}', senha = '{usuario.Senha}' where usuario_id = {usuario.Id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }
    }
}
