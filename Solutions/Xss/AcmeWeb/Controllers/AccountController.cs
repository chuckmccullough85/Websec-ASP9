using AcmeLib;
using AcmeWeb;
using AcmeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using SecurityUtility;

namespace AcmeWebsite.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Landing page for authenticated bank user.  Displays user information and account summary.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = @"Account Home";
            var uid = HttpContext.Session.GetInt32("uid");
            if (uid.HasValue)
            {
				ViewBag.User = BankService.GetUser(uid.Value);
                var accts = BankService.GetAccounts(uid.Value);
                return View(accts);
            }
            return View();
        }

        /// <summary>
        /// Initializes the user profile page
        /// </summary>
        /// <returns></returns>
        public IActionResult Profile()
        {
            if (HttpContext.Session
                        .GetInt32("uid").HasValue)
            {
                var user =
                    BankService.GetUser(
                        HttpContext.Session
                            .GetInt32("uid").Value);
                ProfileModel model = new (user);
                return View(model);
            }
            else
            {
                ViewBag.Message = "Session Timeout";
                return View("../Home/Index");
            }
        }

        /// <summary>
        /// Action handler for saving the profile page.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="phonenum"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SaveProfile(ProfileModel model)
        {
            if (!ModelState.IsValid) { return View("Profile", model); }
            if (HttpContext.Session
                         .GetInt32("uid").HasValue)
            {
                var user = BankService.GetUser(HttpContext.Session
                            .GetInt32("uid").Value);
                user.Firstname = model.FirstName;
                user.Lastname = model.LastName;
                user.Email = model.Email;
                user.Phone = model.Phone;
                BankService.SaveProfile(user);
                return Redirect("~/account/Index");
            }
            else
            {
                ViewBag.Message = "Session Timeout";
                return View("../Home/Index");
            }
        }

        /// <summary>
        /// Initializes the account detail page for the specified account
        /// </summary>
        /// <param name="acctId">The id of the account to display</param>
        /// <returns></returns>
        public IActionResult AccountDetail(int acctId)
        {
            ViewBag.account = BankService.GetAccount(acctId);
            ViewBag.transactions = BankService.GetTransactions(acctId);
            return View();
        }

        /// <summary>
        /// Initializes the account transfer page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Transfer()
        {
            var uid = HttpContext.Session.GetInt32("uid");
            if (uid.HasValue)
            {
                var map = new AccessRefMap<int>();
                var model = new TransferPayModel(BankService.GetAccounts(uid.Value), map);
                HttpContext.Session.SetJsonObject("map", map);
                return View(model);
            }
            else
            {
                ViewBag.Message = "Session Timeout";
                return View("../Home/Index");
            }
        }


        /// <summary>
        /// Transfers funds from one user account to another
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DoTransfer(TransferPayModel model)
        {
            var map = HttpContext.Session.GetJsonObject<AccessRefMap<int>>("map");
            BankService.Transfer(
                map.GetDirectReference(model.FromAccount),
                map.GetDirectReference(model.ToAccount),
                model.Amount);
            return Redirect("~/account/Index");
        }


        /// <summary>
        /// Initializes the Bill pay page.
        /// </summary>
        /// <returns></returns>
		public IActionResult BillPay()
		{
			var uid = HttpContext.Session.GetInt32("uid");
			if (uid.HasValue)
			{
                var accounts = BankService.GetAccounts(uid.Value);
                var map = new AccessRefMap<int>();
                TransferPayModel model = new(accounts, map);
                HttpContext.Session.SetJsonObject("map", map);
                return View(model);
			}
			else
			{
				ViewBag.Message = "Session Timeout";
				return View("../Home/Index");
			}
		}

        /// <summary>
        /// Handler for the bill pay submit.
        /// </summary>
        /// <param name="payee">The person to pay</param>
        /// <param name="amt">The amount to pay them</param>
        /// <param name="fromAcct">The account from which to pay</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DoBillPay(TransferPayModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("BillPay");
            var map = HttpContext.Session.GetJsonObject<AccessRefMap<int>>("map");
            BankService.PayBill(map.GetDirectReference(model.FromAccount), model.Payee, model.Amount);
            return Redirect("~/account/Index");
        }
    }
}