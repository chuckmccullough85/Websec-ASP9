using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace AcmeLib
{
    public class BankService
    {
        private readonly BankContext ctx;
        public BankService(BankContext ctx)
        {
            this.ctx = ctx;
        }

        public void DbReset(string filename)
        {
            using System.IO.StreamReader stream = new(filename);
            var sql = stream.ReadToEnd();
            ctx.Database.ExecuteSqlRaw(sql);
        }

        public User? GetUser(string email)
        {
            Contract.Requires(!string.IsNullOrEmpty(email));
            return ctx.Users.SingleOrDefault(u => u.Email == email);
        }

        public IEnumerable<Account> GetAccounts(User user)
        {
            Contract.Requires(user != null);
            return user?.Accounts.ToList() ?? new List<Account>();
        }

        public void SaveProfile(User user)
        {
            Contract.Requires(user != null);
            ctx.Attach(user!);
            ctx.SaveChanges();
        }

        public Account GetAccount(User user, int id)
        {
            Contract.Requires(user != null && id >= 0);
            return user!.Accounts.Single(a => a.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int accountId)
        {
            Contract.Requires(user != null && accountId >= 0);
            return GetAccount(user!, accountId).Transactions.ToList();
        }

        public void Transfer(User user, int fromAcct, int toAcct, float amount)
        {
            Contract.Requires(user != null && fromAcct >= 0 && toAcct >= 0 && amount > 0 && fromAcct != toAcct);
            GetAccount(user!, fromAcct)
                .Transfer(GetAccount(user!, toAcct), amount);
            ctx.SaveChanges();
        }

        public void PayBill(User user, int fromAcct, string payee, float amount)
        {
            Contract.Requires(user != null && fromAcct >= 0 && !string.IsNullOrEmpty(payee) && amount > 0);
            GetAccount(user!, fromAcct).Pay(payee, amount);
            ctx.SaveChanges();
        }
    }
}