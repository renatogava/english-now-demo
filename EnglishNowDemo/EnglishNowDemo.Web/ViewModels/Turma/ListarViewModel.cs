namespace EnglishNowDemo.Web.ViewModels.Turma
{
    public class ListarViewModel
    {
        public IList<TurmaViewModel>? Turmas { get; set; }

        public bool ExibirBotaoInserir { get; set; }
    }

    public class TurmaViewModel
    {
        public int Id { get; set; }

        public string? Professor { get; set; }

        public string? SemestreAno { get; set; }

        public string? Periodo { get; set; }

        public string? Nivel { get; set; }
    }
}
