using AcmeLib;
using AcmeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcmeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action for db reset link.  Executes the database initial script.
        /// </summary>
        /// <returns>Success page</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "<Pending>")]
        public IActionResult DbReset([FromServices] IWebHostEnvironment env,
            [FromServices]BankService svc)
        {
            var filename = System.IO.Path.Combine(env.WebRootPath, @"DbReset.sql");
            svc.DbReset(filename);
            return View();
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