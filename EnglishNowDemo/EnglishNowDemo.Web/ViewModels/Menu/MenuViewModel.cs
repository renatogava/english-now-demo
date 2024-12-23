namespace EnglishNowDemo.Web.ViewModels.Menu
{
    public class MenuViewModel
    {
        public Menu PaginaAtual { get; set; }
    }

    public enum Menu
    {
        Home,
        Professor,
        Aluno
    }
}
