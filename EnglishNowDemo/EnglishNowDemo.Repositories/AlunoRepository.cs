using EnglishNowDemo.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNowDemo.Repositories
{
    public interface IAlunoRepository
    {
        int? Inserir(Aluno aluno);

        int? Atualizar(Aluno aluno);

        int? Apagar(int id);

        IList<Aluno> Listar();

        Aluno? ObterPorId(int id);

        IList<Aluno> ListarPorTurma(int turmaId);

        IList<Aluno> ListarPorUsuario(int usuarioId);
    }

    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(string connectionString) : base(connectionString) { }

        public int? Inserir(Aluno aluno)
        {
            int? alunoId = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"insert into aluno (nome, email, usuario_id) values ('{aluno.Nome}', '{aluno.Email}', {aluno.UsuarioId}); 
                                    select LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                alunoId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return alunoId;
        }

        public int? Atualizar(Aluno aluno)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"update aluno set nome = '{aluno.Nome}', email = '{aluno.Email}' where aluno_id = {aluno.Id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }

        public int? Apagar(int id)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"delete from aluno where aluno_id = {id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }

        public IList<Aluno> Listar()
        {
            var result = new List<Aluno>();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select a.aluno_id, a.nome, a.email, a.usuario_id, u.login, u.senha, u.papel_id from 
                                    aluno a inner join usuario u on a.usuario_id = u.usuario_id 
                                    order by a.aluno_id";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            UsuarioId = reader.GetInt32(3),
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32(3),
                                Login = reader.GetString(4),
                                Senha = reader.GetString(5),
                                PapelId = reader.GetInt32(6)
                            }
                        };

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public Aluno? ObterPorId(int id)
        {
            Aluno? result = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select a.aluno_id, a.nome, a.email, a.usuario_id, u.login, u.senha, u.papel_id 
                                    from aluno a inner join usuario u on a.usuario_id = u.usuario_id
                                    where a.aluno_id = {id}
                                    order by a.nome";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Aluno
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            UsuarioId = reader.GetInt32(3),
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32(3),
                                Login = reader.GetString(4),
                                Senha = reader.GetString(5),
                                PapelId = reader.GetInt32(6)
                            }
                        };
                    }
                }
            }

            return result;
        }

        public IList<Aluno> ListarPorTurma(int turmaId)
        {
            var result = new List<Aluno>();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select a.aluno_id, a.nome, a.email, a.usuario_id, u.login, u.senha, u.papel_id from 
                                    aluno_turma_boletim atb inner join
                                    aluno a on atb.aluno_id = a.aluno_id inner join 
                                    usuario u on a.usuario_id = u.usuario_id 
                                    where atb.turma_id = {turmaId}
                                    order by a.aluno_id";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            UsuarioId = reader.GetInt32(3),
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32(3),
                                Login = reader.GetString(4),
                                Senha = reader.GetString(5),
                                PapelId = reader.GetInt32(6)
                            }
                        };

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public IList<Aluno> ListarPorUsuario(int usuarioId)
        {
            var result = new List<Aluno>();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select a.aluno_id, a.nome, a.email, a.usuario_id, ua.login, ua.senha, ua.papel_id from 
                                    aluno_turma_boletim atb inner join
                                    aluno a on atb.aluno_id = a.aluno_id inner join
                                    turma t on atb.turma_id = t.turma_id inner join
                                    professor p on t.professor_id = p.professor_id inner join
                                    usuario up on p.usuario_id = up.usuario_id inner join
                                    usuario ua on a.usuario_id = ua.usuario_id 
                                    where up.usuario_id = {usuarioId}
                                    order by a.aluno_id";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            UsuarioId = reader.GetInt32(3),
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32(3),
                                Login = reader.GetString(4),
                                Senha = reader.GetString(5),
                                PapelId = reader.GetInt32(6)
                            }
                        };

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

    }
}
