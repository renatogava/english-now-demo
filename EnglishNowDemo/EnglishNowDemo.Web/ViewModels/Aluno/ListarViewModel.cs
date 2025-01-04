namespace EnglishNowDemo.Web.ViewModels.Aluno
{
    public class ListarViewModel
    {
        public IList<AlunoViewModel>? Alunos { get; set; }

        public bool ExibirBotaoInserirEditar { get; set; }
    }

    public class AlunoViewModel
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }
    }
}
