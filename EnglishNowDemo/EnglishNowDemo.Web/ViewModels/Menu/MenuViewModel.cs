namespace EnglishNowDemo.Web.ViewModels.Menu
{
    public class MenuViewModel
    {
        public Menu Ativo { get; set; }

        public bool ProfessorVisivel { get; set; }
    }

    public enum Menu
    {
        Home,
        Professor,
        Aluno,
        Turma
    }
}
