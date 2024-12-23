using EnglishNowDemo.Web.ViewModels.Menu;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNowDemo.Web.Components
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new MenuViewModel
            {
                Ativo = ViewData["Menu"] != null ? (Menu)ViewData["Menu"] : Menu.Home
            };

            return View(model);
        }
    }
}
