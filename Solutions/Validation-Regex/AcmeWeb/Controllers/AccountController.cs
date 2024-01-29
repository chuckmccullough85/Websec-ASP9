using AcmeLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                ViewBag.user =
                    BankService.GetUser(
                        HttpContext.Session
                            .GetInt32("uid").Value);
                return View();
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
        [HttpPost]
        public IActionResult SaveProfile(string firstname, string lastname, string email, string phonenum)
        {
            if (HttpContext.Session
                         .GetInt32("uid").HasValue)
            {
                var user = BankService.GetUser(HttpContext.Session
                            .GetInt32("uid").Value);
                user.Firstname = firstname;
                user.Lastname = lastname;
                user.Email = email;
                user.Phone = phonenum;
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
				ViewBag.Accts = BankService.GetAccounts(uid.Value);
				return View();
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
        /// <param name="fromAcct">The id of the account to debit</param>
        /// <param name="toAcct">The id of the account to credit</param>
        /// <param name="amount">The amount to transfer</param>
        /// <returns></returns>
		public IActionResult DoTransfer (int fromAcct, int toAcct, float amount)
		{
            BankService.Transfer(fromAcct, toAcct, amount);
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
				ViewBag.Accts = BankService.GetAccounts(uid.Value);
				return View();
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
		public IActionResult DoBillPay(string payee, float amt, int fromAcct)
		{
            if (Regex.IsMatch(payee, @"^[a-zA-Z\s\-']{2,30}$") 
                && amt is >= 0.01f and <= 10_000.00f)
                {
			BankService.PayBill(fromAcct, payee, amt);
			return Redirect("~/account/Index");
                }
            else
            {
                ViewBag.Message = "Invalid Input";
                return View("BillPay");
            }
		}

	}
}