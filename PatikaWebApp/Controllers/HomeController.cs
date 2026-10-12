using Microsoft.AspNetCore.Mvc;
using PatikaWebApp.Models;
using System.Diagnostics;
using PatikaWebApp.Services;

namespace patikaWebapp.Controllers
{
    public class HomeController : Controller
    {
        List<Gyogyszer> gyogyszerek = new List<Gyogyszer>
        {
            new Gyogyszer
            {
                Id = 1,
                Nev = "Név1",
                Hatoanyag = "11111111111",
                Venykoteles = true,
                Kepnev = "Aspirin"
            },
            new Gyogyszer
            {
                Id = 2,
                Nev = "Név2",
                Hatoanyag = "00000000000",
                Venykoteles = false,
                Kepnev = "Vmi más"
            }
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Sajat(int id)
        {
            List<Gyogyszer> gyogyszerek = new GyogyszerService().GetAllGyogyszer();
            return View(gyogyszerek);
        }

        public IActionResult Gyogyszer()
        {
            List<Gyogyszer> gyogyszerek =
                new GyogyszerService().GetAllGyogyszer();

            return View(gyogyszerek);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CreateGyogyszer()
        {
            Gyogyszer uresGyogyszer = new Gyogyszer();
            return View(uresGyogyszer);
        }
        [HttpPost]
        public IActionResult CreateGyogyszer(Gyogyszer gyogyszer)
        {
            string result = new GyogyszerService().PostGyogyszer(gyogyszer);
            TempData["Success message"] = result;
            return RedirectToAction(nameof(CreateGyogyszer));
        }

    }
}