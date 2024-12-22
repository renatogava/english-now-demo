using EnglishNowDemo.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNowDemo.Repositories
{
    public interface IProfessorRepository
    {
        int? Inserir(Professor professor);

        int? Atualizar(Professor professor);

        int? Apagar(int id);

        IList<Professor> Listar();

        Professor? ObterPorId(int id);
    }

    public class ProfessorRepository : BaseRepository, IProfessorRepository
    {
        public ProfessorRepository(string connectionString) : base(connectionString) { }

        public int? Inserir(Professor professor)
        {
            int? professorId = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"insert into professor (nome, email, usuario_id) values ('{professor.Nome}', '{professor.Email}', {professor.UsuarioId}); 
                                    select LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                professorId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return professorId;
        }

        public int? Atualizar(Professor professor)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"update professor set nome = '{professor.Nome}', email = '{professor.Email}' where professor_id = {professor.Id}";

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
                string query = @$"delete from professor where professor_id = {id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }

        public IList<Professor> Listar()
        {
            var result = new List<Professor>();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = $"select p.professor_id, p.nome, p.email, p.usuario_id, u.login, u.senha, u.papel_id from professor p inner join usuario u on p.usuario_id = u.usuario_id order by p.professor_id";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var professor = new Professor
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

                        result.Add(professor);
                    }
                }
            }

            return result;
        }

        public Professor? ObterPorId(int id)
        {
            Professor? result = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select p.professor_id, p.nome, p.email, p.usuario_id, u.login, u.senha, u.papel_id 
                                    from professor p inner join usuario u on p.usuario_id = u.usuario_id
                                    where p.professor_id = {id}
                                    order by p.nome";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Professor
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
    }
}
