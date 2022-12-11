using Microsoft.AspNetCore.Mvc;
using WebApplication3.Classes.Singleton;
using WebApplication3.Classes.Composite;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class SignInController : Controller
    {
        ApplicationContext context = new ApplicationContext();
        public IActionResult Auth()
        {
            return View();
        }
        public IActionResult Check(string login, string password)
        {
            string logi = context.Users.Where(e => e.login == login && e.password == password).Select(x => x.login).FirstOrDefault();
            int id = context.Users.Where(e => e.login == login && e.password == password).Select(i => i.Id).FirstOrDefault();
            if (logi?.Any() ?? false)
            {
                UserInfo.Id = id;
                UserInfo.Login = login;
                UserInfo.Password = password;
                return Redirect("~/Home/Indexreal");
            }
            else
            {
                MainRoot.main.Clear();
                UserInfo.Id = 0;
                UserInfo.Login = null;
                UserInfo.Password = null;
                return Redirect("~/Home/Indexunreal");
            }
        }
    }
}
