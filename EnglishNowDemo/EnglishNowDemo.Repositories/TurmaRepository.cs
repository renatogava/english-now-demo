using EnglishNowDemo.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNowDemo.Repositories
{
    public interface ITurmaRepository
    {
        int? Inserir(Turma turma);

        int? Atualizar(Turma turma);

        int? Apagar(int id);

        IList<Turma> Listar();

        Turma? ObterPorId(int id);
    }

    public class TurmaRepository : BaseRepository, ITurmaRepository
    {
        public TurmaRepository(string connectionString) : base(connectionString) { }

        public int? Inserir(Turma turma)
        {
            int? turmaId = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"insert into turma (professor_id, semestre, ano, nivel, periodo) values ({turma.ProfessorId}, {turma.Semestre}, {turma.Ano}, '{turma.Nivel}', '{turma.Periodo}'); 
                                    select LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                turmaId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return turmaId;
        }

        public int? Atualizar(Turma turma)
        {
            int? affectedRows = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"update turma set professor_id = {turma.ProfessorId}, semestre = {turma.Semestre}, ano = {turma.Ano}, nivel = '{turma.Nivel}', periodo = '{turma.Periodo}' where turma_id = {turma.Id}";

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
                string query = @$"delete from turma where turma_id = {id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }

        public IList<Turma> Listar()
        {
            var result = new List<Turma>();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select t.turma_id, p.professor_id, p.nome, p.email, p.usuario_id, t.ano, t.semestre, t.periodo, t.nivel
                                    from turma t inner join 
                                    professor p on t.professor_id = p.professor_id order by t.ano, t.semestre";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var turma = new Turma
                        {
                            Id = reader.GetInt32(0),
                            ProfessorId = reader.GetInt32(1),
                            Professor = new Professor
                            {
                                Id = reader.GetInt32(1),
                                Nome = reader.GetString(2),
                                Email = reader.GetString(3),
                                UsuarioId = reader.GetInt32(4),
                            },
                            Ano = reader.GetInt32(5),
                            Semestre = reader.GetInt32(6),
                            Periodo = reader.GetString(7),
                            Nivel = reader.GetString(8)
                        };

                        result.Add(turma);
                    }
                }
            }

            return result;
        }

        public Turma? ObterPorId(int id)
        {
            Turma? result = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select t.turma_id, p.professor_id, p.nome, p.email, p.usuario_id, t.ano, t.semestre, t.periodo, t.nivel
                                    from turma t inner join 
                                    professor p on t.professor_id = p.professor_id where t.turma_id = {id}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Turma
                        {
                            Id = reader.GetInt32(0),
                            ProfessorId = reader.GetInt32(1),
                            Professor = new Professor
                            {
                                Id = reader.GetInt32(1),
                                Nome = reader.GetString(2),
                                Email = reader.GetString(3),
                                UsuarioId = reader.GetInt32(4),
                            },
                            Ano = reader.GetInt32(5),
                            Semestre = reader.GetInt32(6),
                            Periodo = reader.GetString(7),
                            Nivel = reader.GetString(8)
                        };
                    }
                }
            }

            return result;
        }
    }
}
