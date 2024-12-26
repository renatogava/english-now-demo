using EnglishNowDemo.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Repositories
{
    public interface IAlunoTurmaBoletimRepository
    {
        int? Inserir(AlunoTurmaBoletim alunoTurmaBoletim);

        AlunoTurmaBoletim? ObterPorAlunoTurma(int alunoId, int turmaId);
    }

    public class AlunoTurmaBoletimRepository : BaseRepository, IAlunoTurmaBoletimRepository
    {
        public AlunoTurmaBoletimRepository(string connectionString) : base(connectionString) { }

        public int? Inserir(AlunoTurmaBoletim alunoTurmaBoletim)
        {
            int? alunoTurmaBoletimId = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"INSERT INTO `aluno_turma_boletim`
                    (`aluno_id`,
                    `turma_id`,
                    `nota_bim1_escrita`,
                    `nota_bim1_leitura`,
                    `nota_bim1_conversacao`,
                    `nota_bim1_final`,
                    `nota_bim2_escrita`,
                    `nota_bim2_leitura`,
                    `nota_bim2_conversacao`,
                    `nota_bim2_final`,
                    `nota_final_semestre`,
                    `faltas_semestre`)
                    VALUES
                    ({alunoTurmaBoletim.AlunoId},
                    {alunoTurmaBoletim.TurmaId},
                    @NotaBim1Leitura,
                    @NotaBim1Escrita,
                    @NotaBim1Conversacao,
                    @NotaBim1Final,
                    @NotaBim2Leitura,
                    @NotaBim2Escrita,
                    @NotaBim2Conversacao,
                    @NotaBim2Final,
                    @NotaFinalSemestre,
                    @FaltasSemestre);select LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, cnn);

                cmd.Parameters.AddWithValue("NotaBim1Leitura", alunoTurmaBoletim.NotaBim1Leitura.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim1Escrita", alunoTurmaBoletim.NotaBim1Escrita.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim1Conversacao", alunoTurmaBoletim.NotaBim1Conversacao.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim1Final", alunoTurmaBoletim.NotaBim1Final.ValueOrDbNull());

                cmd.Parameters.AddWithValue("NotaBim2Leitura", alunoTurmaBoletim.NotaBim2Leitura.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim2Escrita", alunoTurmaBoletim.NotaBim2Escrita.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim2Conversacao", alunoTurmaBoletim.NotaBim2Conversacao.ValueOrDbNull());
                cmd.Parameters.AddWithValue("NotaBim2Final", alunoTurmaBoletim.NotaBim2Final.ValueOrDbNull());

                cmd.Parameters.AddWithValue("NotaFinalSemestre", alunoTurmaBoletim.NotaFinalSemestre.ValueOrDbNull());
                cmd.Parameters.AddWithValue("FaltasSemestre", alunoTurmaBoletim.FaltasSemestre.ValueOrDbNull());

                cnn.Open();

                alunoTurmaBoletimId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return alunoTurmaBoletimId;
        }

        public AlunoTurmaBoletim? ObterPorAlunoTurma(int alunoId, int turmaId)
        {
            AlunoTurmaBoletim? result = null;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                string query = @$"select
                    `aluno_turma_boletim_id`,
                    `aluno_id`,
                    `turma_id`,
                    `nota_bim1_escrita`,
                    `nota_bim1_leitura`,
                    `nota_bim1_conversacao`,
                    `nota_bim1_final`,
                    `nota_bim2_escrita`,
                    `nota_bim2_leitura`,
                    `nota_bim2_conversacao`,
                    `nota_bim2_final`,
                    `nota_final_semestre`,
                    `faltas_semestre`
                    from aluno_turma_boletim
                    where aluno_id = {alunoId} and turma_id = {turmaId}";

                var cmd = new MySqlCommand(query, cnn);

                cnn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AlunoTurmaBoletim
                        {
                            Id = reader.GetInt32(0),
                            AlunoId = reader.GetInt32(1),
                            TurmaId = reader.GetInt32(2),
                            NotaBim1Leitura = reader.GetDecimalOrNull(3),
                            NotaBim1Escrita = reader.GetDecimalOrNull(4),
                            NotaBim1Conversacao = reader.GetDecimalOrNull(5),
                            NotaBim1Final = reader.GetDecimalOrNull(6),
                            NotaBim2Leitura = reader.GetDecimalOrNull(7),
                            NotaBim2Escrita = reader.GetDecimalOrNull(8),
                            NotaBim2Conversacao = reader.GetDecimalOrNull(9),
                            NotaBim2Final = reader.GetDecimalOrNull(10),
                            NotaFinalSemestre = reader.GetDecimalOrNull(11),
                            FaltasSemestre = reader.GetInt32OrNull(12)
                        };
                    }
                }
            }

            return result;
        }
    }
}
