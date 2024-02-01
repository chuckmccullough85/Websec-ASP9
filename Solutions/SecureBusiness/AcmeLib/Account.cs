using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeLib
{
    public enum AccountType { Checking = 1, Savings = 2 }

    [Table("Accounts")]
    public class Account
    {
        public Account() { Transactions = new List<Transaction>(); }
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Column("balance")]
        public decimal StartBalance { get; set; }
        [Column("type")]
        public AccountType AccountType { get; set; }
        [NotMapped]
        public string AccountName
        {
            get { return AccountType.ToString(); }
        }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public void Transfer(Account toAccount, float amount)
        {
            var ct = new Transaction
            {
                Amount = (decimal)amount,
                Date = System.DateTime.Today,
                Type = TransactionType.Credit,
                Payee = ""
            };
            toAccount.Transactions.Add(ct);
            var dt = new Transaction
            {
                Amount = (decimal)amount,
                Date = ct.Date,
                Type = TransactionType.Debit,
                Payee = ""
            };
            Transactions.Add(dt);
        }


        public void Pay(string payee, float amount)
        {
            var dt = new Transaction
            {
                Amount = (decimal)amount,
                Date = System.DateTime.Today,
                Type = TransactionType.Debit,
                Payee = payee
            };
            Transactions.Add(dt);
        }
    }
}
