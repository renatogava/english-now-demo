using Microsoft.AspNetCore.Mvc;

namespace EnglishNowDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
