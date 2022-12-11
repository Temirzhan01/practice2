using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Classes.Singleton;
using WebApplication3.Classes.Strategy;
using WebApplication3.Classes.Adapter;
using WebApplication3.Classes.Composite;
using WebApplication3.Classes.Intermediate;
using WebApplication3.Classes.State;
using WebApplication3.Classes.Flyweight;
using WebApplication3.Classes.Command;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext context;
        IStrategy strategy;
        Context cont;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            context = new ApplicationContext();
            cont = new Context(new TCardFactory(new List<Card> { }), new Invoker());
        }
        [HttpGet]
        public async Task<IActionResult> IndexReal()
        {
            strategy = new Full();
            return View(strategy.Showing(await context.Cardjsons.Where(x => x.userId == UserInfo.Id).ToListAsync()));
        }
        [HttpGet]
        public async Task<IActionResult> IndexUnreal()
        {
            strategy = new NotFull();
            return View(strategy.Showing(await context.Cardjsons.Where(x => x.userId == UserInfo.Id).ToListAsync()));
        }
        [HttpGet]
        public async Task<IActionResult> Show(IEnumerable<Connecter> cards)
        {
            return PartialView(cards);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTextCard(string key, string a1, string a2, string a3, string a4)
        {
            if (UserInfo.Login != null && UserInfo.Password != null && UserInfo.Id != 0)
            {
                strategy = new Full();
                await strategy.Creating(true, key, a1, a2, a3, a4, cont);
                return RedirectToAction("IndexReal");
            }
            else
            {
                strategy = new NotFull();
                await strategy.Creating(true, key, a1, a2, a3, a4, cont);
                return RedirectToAction("IndexUnreal");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestionCard(string key, string a1, string a2, string a3, string a4)
        {
            if (UserInfo.Login != null && UserInfo.Password != null && UserInfo.Id != 0)
            {
                strategy = new Full();
                await strategy.Creating(false, key, a1, a2, a3, a4, cont);
                return RedirectToAction("IndexReal");
            }
            else
            {
                strategy = new NotFull();
                await strategy.Creating(false, key, a1, a2, a3, a4, cont);
                return RedirectToAction("IndexUnreal");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTC(int Id) 
        {
            if (UserInfo.Login != null && UserInfo.Password != null && UserInfo.Id != 0)
            {
                await cont.invoker.PressUndoButton(0, Id, true);
                return RedirectToAction("IndexReal");
            }
            else
            {
                await cont.invoker.PressUndoButton(0, Id, false);
                return RedirectToAction("IndexUnreal");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteQC(int Id)
        {
            if (UserInfo.Login != null && UserInfo.Password != null && UserInfo.Id != 0)
            {
                await cont.invoker.PressUndoButton(1, Id, true);
                return RedirectToAction("IndexReal");
            }
            else
            {
                await cont.invoker.PressUndoButton(1, Id, false);
                return RedirectToAction("IndexUnreal");
            }
        }
    }
}