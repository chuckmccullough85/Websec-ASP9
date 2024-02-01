namespace AcmeLib
{
    /// <summary>
    /// Represents an account that is owned by a bank user.
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public float StartBalance { get; set; }
        public int AccountType { get; set; }
        /// <summary>
        /// 1 for checking, 2 for savings.  Other types will be defined in the future
        /// </summary>
        public string AccountName
        {
            get { return AccountType == 1 ? "Checking" : "Savings"; }
        }
    }
}
