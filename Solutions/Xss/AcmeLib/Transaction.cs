using System;

namespace AcmeLib
{
    /// <summary>
    /// Represents a transaction.  This is a double entry accounting system.  One transaction object for the credit,
    /// and one transaction for the debit account.
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public int AcctId { get; set; }
        public float Amount { get; set; }
        /// <summary>
        /// This field is only used in bill pay scenarios.  Leave blank for internal account transfers.
        /// </summary>
        public string Payee { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// 0 means debit, 1 is credit
        /// </summary>
        public string TypeString
        {
            get { return Type == 0 ? "Check" : "Deposit"; }
        }
        public DateTime Date { get; set; }

    }
}
