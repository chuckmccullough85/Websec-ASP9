using AcmeLib;
using AcmeWeb;
using AcmeWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityUtility;

namespace AcmeWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public BankService Svc { get; set; }
        private AcmeLib.User BankUser => Svc.GetUser(User.Identity.Name);
        private int UserId => BankUser.Id;

        public AccountController(BankService svc)
        {
            Svc = svc;
        }
        /// <summary>
        /// Landing page for authenticated bank user.  Displays user information and account summary.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = @"Account Home";
            ViewBag.User = BankUser;
            var accts = Svc.GetAccounts(BankUser);
            return View(accts);
        }

        /// <summary>
        /// Initializes the user profile page
        /// </summary>
        /// <returns></returns>
        public IActionResult Profile()
        {
            ProfileModel model = new(BankUser);
            return View(model);
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

            var user = BankUser;
            user.Firstname = model.FirstName;
            user.Lastname = model.LastName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            Svc.SaveProfile(user);
            return Redirect("~/account/Index");
        }

        /// <summary>
        /// Initializes the account detail page for the specified account
        /// </summary>
        /// <param name="acctId">The id of the account to display</param>
        /// <returns></returns>
        public IActionResult AccountDetail(int acctId)
        {
            ViewBag.account = Svc.GetAccount(BankUser, acctId);
            ViewBag.transactions = Svc.GetTransactions(BankUser,acctId);
            return View();
        }

        /// <summary>
        /// Initializes the account transfer page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Transfer()
        {
            var map = new AccessRefMap<int>();
            var model = new TransferPayModel(Svc.GetAccounts(BankUser), map);
            HttpContext.Session.SetJsonObject("map", map);
            return View(model);
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
            Svc.Transfer(BankUser,
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
            var accounts = Svc.GetAccounts(BankUser);
            var map = new AccessRefMap<int>();
            TransferPayModel model = new(accounts, map);
            HttpContext.Session.SetJsonObject("map", map);
            return View(model);
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
            Svc.PayBill(BankUser, map.GetDirectReference(model.FromAccount), model.Payee, model.Amount);
            return Redirect("~/account/Index");
        }
    }
}