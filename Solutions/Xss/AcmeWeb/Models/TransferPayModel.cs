using AcmeLib;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecurityUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AcmeWeb.Models
{
    public class TransferPayModel
    {
        public TransferPayModel()
        { }
        public TransferPayModel(IEnumerable<Account> accounts, AccessRefMap<int> map)
        {
            Accounts = accounts.Select(a => new SelectListItem(a.AccountName, map.AddDirectReference(a.Id))).ToList();
            FromAccount = "";
            ToAccount = "";
        }

        public IEnumerable<SelectListItem>? Accounts { get; set; }
        [Required, RegularExpression(@"[A-Z]{6}")]
        public string FromAccount { get; set; }
        [RegularExpression(@"[A-Z]{6}")]
        public string? ToAccount { get; set; }

        [RegularExpression(@"[\s\w'-]{3,30}"), Display(Prompt = "Payee")]
        public string? Payee { get; set; }
        [Required, Range(1, 10_000), Display(Prompt = "Amount to Pay")]
        public float Amount { get; set; }
    }
}
