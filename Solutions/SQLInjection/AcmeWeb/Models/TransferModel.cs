using AcmeLib;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecurityUtility;

namespace AcmeWebsite.Models
{
    public class TransferModel
    {
        public TransferModel()
        { }
        public TransferModel(IEnumerable<Account> accounts, AccessRefMap<int> map)
        {
            Accounts = accounts.Select(a => new SelectListItem(a.AccountName, map.AddDirectReference(a.Id))).ToList();
            FromAccount = "";
            ToAccount = "";
        }

        public IEnumerable<SelectListItem> Accounts { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public float Amount { get; set; }
    }
}
