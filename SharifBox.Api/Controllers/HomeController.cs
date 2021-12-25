using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
using SharifBox.Api.Models;
using SharifBox.Api.Models.Refit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SharifBox.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            CaruselItem carusel = new CaruselItem();
            var res = carusel.GetCaruselItems();

            var events = RestService.For<IRefitRequests>("http://localhost:5012");
            ViewBag.Events = await events.GetEvents();

            var teams = RestService.For<IRefitRequests>("http://localhost:5022");
            ViewBag.Teams = await teams.GetTeams();

            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}