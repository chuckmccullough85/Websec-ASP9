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
        /// This method responds to the login form submission.  On successfull login, 
        /// stores the authenticated user's Id in the session under the key "uid"
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pw"></param>
        /// <returns>On successful login, redirects to the account home page</returns>
        public IActionResult DoLogin(string email, string pw)
        {
            var user = BankService.Login(email, pw);
            if (user != null)
            {
                HttpContext.Session.SetInt32("uid", user.Id);
                return Redirect("~/account/Index");
            }
            else
            {
                ViewBag.Message = "Invalid Login";
                return View("../Home/Index");
            }
        }


        /// <summary>
        /// Action for db reset link.  Executes the database initial script.
        /// </summary>
        /// <returns>Success page</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "<Pending>")]
        public IActionResult DbReset([FromServicesAttribute] IWebHostEnvironment env)
        {
            var filename = System.IO.Path.Combine(env.WebRootPath, @"DbReset.sql");
            BankService.DbReset(filename);
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