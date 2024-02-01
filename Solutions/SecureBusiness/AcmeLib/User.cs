using System.Collections.Generic;

namespace AcmeLib
{
    /// <summary>
    /// Represents a bank customer.
    /// </summary>
    public class User
    {
        public User() { Accounts = new List<Account>(); }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
